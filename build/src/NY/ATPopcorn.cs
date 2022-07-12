using DuckGame;
using System;
using TMGmod.Core.AmmoTypes;

namespace TMGmod.NY
{
    // ReSharper disable once InconsistentNaming
    public class ATPopcorn : BaseAmmoType
    {
        private readonly SpriteMap _sprite;
        public ATPopcorn()
        {
            bulletType = typeof(PopBullet);
            _sprite = new SpriteMap(Mod.GetPath<Core.TMGmod>("Holiday/Popcal"), 3, 3) { frame = Rando.Int(0, 8) };
            sprite = _sprite;
            bulletSpeed = 6f;
            range = 400f;
            accuracy = 0.85f;
            bulletLength = 3f;
            penetration = 0.7f;
            affectedByGravity = true;
            weight = 0f;
            barrelAngleDegrees = Rando.Float(6f, -18f);
        }
        public override Bullet FireBullet(Vec2 position, Thing owner = null, float angle = 0, Thing firedFrom = null)
        {
            barrelAngleDegrees = Rando.Float(6f, -18f);
            _sprite.frame = Rando.Int(0, 8);
            sprite = _sprite;
            return base.FireBullet(position, owner, angle, firedFrom);
        }

        public override void OnHit(bool destroyed, Bullet b)
        {
            base.OnHit(destroyed, b);
            if (!destroyed) return;
            if (!(b.firedFrom is Gun gun)) return;
            for (var index = 0; index < 20; ++index)
            {
                var num2 = (float)(index * 18.0 - 5.0) + Rando.Float(10f);
                SFX.Play("littleGun");
                var atShrapnel = new ATShrapnel { range = 20f + Rando.Float(6f) };
                var bullet = new Bullet(b.x + (float)(Math.Cos(Maths.DegToRad(num2)) * 6.0),
                    b.y - (float)(Math.Sin(Maths.DegToRad(num2)) * 6.0), atShrapnel, num2)
                { firedFrom = b.firedFrom };
                gun.firedBullets.Add(bullet);
                Level.Add(bullet);
            }
            gun.bulletFireIndex += 20;
            if (!Network.isActive) return;
            Send.Message(new NMFireGun(gun, gun.firedBullets, gun.bulletFireIndex, false), NetMessagePriority.ReliableOrdered);
            gun.firedBullets.Clear();
        }
    }
}

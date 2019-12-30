using System;
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.NY
{

    [EditorGroup("TMG|Misc|Holiday")]
    // ReSharper disable once InconsistentNaming
    public class Popcal : BaseAr
    {
        private readonly SpriteMap _sprite;
        public Popcal(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 16;
            _ammoType = new ATPopcorn();
            BaseAccuracy = 0.5f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Holiday/Popcal 9mm"), 27, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(14f, 5f);
            _collisionOffset = new Vec2(-14f, -5f);
            _collisionSize = new Vec2(27f, 10f);
            _barrelOffsetTL = new Vec2(27f, 4f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.8f;
            _kickForce = 1.45f;
            _holdOffset = new Vec2(0f, 0f);
            _editorName = "Popcal 9mm";
			_weight = 3f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.4f;
            thickness = 0f;
        }

        public override bool Hit(Bullet bullet, Vec2 hitPos)
        {
            var hit = base.Hit(bullet, hitPos);
            var hitpos = Offset(new Vec2(0, -3));
            var dp = bullet.position - hitpos;
            var v = bullet.bulletSpeed * bullet.travelDirNormalized;
            var u = new Vec2(v.y, -v.x).normalized;
            if (Math.Abs(Vec2.Dot(dp, u)) < 3f)
            {
                Destroy(new DTShot(bullet));
            }
            return hit;
        }

        public override bool Destroy(DestroyType type1 = null)
        {
            var hitpos = Offset(new Vec2(0, 0));
            for (var index = 0; index < ammo; ++index)
            {
                var num2 = (float)(index * 18.0 - 5.0) + Rando.Float(10f);
                var atPopcorn = new ATPopcorn {bulletSpeed = 2f};
                var bullet = new Bullet(hitpos.x + (float)(Math.Cos(Maths.DegToRad(num2)) * 6.0),
                        hitpos.y - (float)(Math.Sin(Maths.DegToRad(num2)) * 6.0), atPopcorn, num2)
                    { firedFrom = this };
                firedBullets.Add(bullet);
                Level.Add(bullet);
            }
            bulletFireIndex += (byte) ammo;
            ammo = 0;
            if (!Network.isActive) return base.Destroy(type1);
            Send.Message(new NMFireGun(this, firedBullets, bulletFireIndex, false),
                NetMessagePriority.ReliableOrdered);
            firedBullets.Clear();
            return base.Destroy(type1);
        }

        public override void Update()
        {
            if ((ammo > 13) & (ammo < 15)) _sprite.frame = 1;
            if ((ammo > 11) & (ammo < 13)) _sprite.frame = 2;
            if ((ammo > 9) & (ammo < 11)) _sprite.frame = 3;
            if ((ammo > 7) & (ammo < 9)) _sprite.frame = 4;
            if ((ammo > 5) & (ammo < 7)) _sprite.frame = 5;
            if ((ammo > 3) & (ammo < 5)) _sprite.frame = 6;
            if ((ammo > 1) & (ammo < 3)) _sprite.frame = 7;
            if (ammo < 1) _sprite.frame = 8;
            base.Update();
        }
    }
}

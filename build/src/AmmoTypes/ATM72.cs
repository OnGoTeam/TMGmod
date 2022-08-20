using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATM72 : BaseAmmoType, IHeavyAmmoType
    {
        public ATM72()
        {
            accuracy = 0.95f;
            penetration = 0.35f;
            bulletSpeed = 15f;
            range = 2000f;
            weight = 5f;
            barrelAngleDegrees = -2.5f;
            bulletThickness = 2f;
            affectedByGravity = true;
            bulletColor = Color.Yellow;
            bulletType = typeof(GrenadeBullet);
            immediatelyDeadly = true;
            sprite = new Sprite("launcherGrenade");
            sprite.CenterOrigin();
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shalker = new M72Shell(x, y) { hSpeed = dir * (3.5f + Rando.Float(1f)) };
            add(shalker);
        }
    }
}

using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATKSG12 : BaseAmmoType
    {
        public ATKSG12()
        {
            accuracy = 0.4f;
            penetration = 1f;
            range = 185f;
            weight = 5f;
            bulletSpeed = 25f;
            bulletThickness = 0.5f;
            DamageMean = 16f;
            DamageVariation = 0.75f;
            AlphaDamage = 0.4f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new Gauge12Shell(x, y)
            {
                hSpeed = (0.5f + Rando.Float(-0.2f, 0.2f)) * dir,
                vSpeed = 0.75f + Rando.Float(-0.5f, 0.5f),
                depth = -0.2f - Rando.Float(0.0f, 0.1f),
            };
            add(shell);
        }
    }
}

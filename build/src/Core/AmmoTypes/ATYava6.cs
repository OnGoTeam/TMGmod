using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATYava6 : BaseAmmoType
    {
        public ATYava6()
        {
            accuracy = 0.9f;
            penetration = 3f;
            bulletSpeed = 42f;
            range = 500f;
            weight = 5f;
            bulletThickness = 1.2f;
            DamageMean = 22f;
            AlphaDamage = 0.5f;
            DamageVariation = 0f;
            DistanceConvexity = 1;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new Gauge12Shell(x, y)
            {
                hSpeed = (2f + Rando.Float(-0.2f, 0.2f)) * dir,
                vSpeed = -1.2f + Rando.Float(-0.5f, 0.5f),
                depth = 0.2f - Rando.Float(0.0f, 0.1f),
            };
            add(shell);
        }
    }
}

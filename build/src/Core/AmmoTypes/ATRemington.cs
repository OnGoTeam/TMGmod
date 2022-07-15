using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATRemington : BaseAmmoType
    {
        public ATRemington()
        {
            range = 180f;
            accuracy = 0.67f;
            bulletThickness = 1.5f;
            penetration = 1f;
            bulletSpeed = 25f;
            deadly = true;
            weight = 5f;
            immediatelyDeadly = true;
            DamageMean = 26f;
            DamageVariation = 0.5f;
            AlphaDamage = 1f;
            DistanceConvexity = 1.2f;
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

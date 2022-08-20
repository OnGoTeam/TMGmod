using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class AT44DB : BaseAmmoType
    {
        public AT44DB()
        {
            accuracy = 0.1f;
            penetration = 4f;
            bulletSpeed = 125f;
            range = 100f;
            weight = 5f;
            bulletThickness = 2f;
            DamageMean = 10f;
            DamageVariation = 0.1f;
            AlphaDamage = 0.8f;
            DistanceConvexity = -0.2f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new DB44Shell(x, y)
            {
                hSpeed = (-1f + Rando.Float(-0.2f, 0.2f)) * dir,
                vSpeed = 1f + Rando.Float(-0.5f, 0.5f),
            };
            add(shell);
        }
    }
}

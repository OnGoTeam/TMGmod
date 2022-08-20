using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATVista : BaseAmmoType
    {
        public ATVista()
        {
            accuracy = .75f;
            range = 100f;
            penetration = 0.4f;
            bulletSpeed = 37f;
            bulletThickness = 0.8f;
            bulletLength = 6f;
            DamageMean = 24f;
            DamageVariation = 0.33f;
            AlphaDamage = 0.33f;
            DistanceConvexity = -2f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT9mmShell(x, y)
            {
                hSpeed = (1.5f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = -3f + Rando.Float(-0.4f, 0.4f),
            };
            add(shell);
        }
    }
}

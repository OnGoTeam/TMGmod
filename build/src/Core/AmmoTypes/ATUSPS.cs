using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATUSPS : BaseAmmoType
    {
        public ATUSPS()
        {
            range = 750f;
            accuracy = 0.9f;
            penetration = 0.4f;
            bulletSpeed = 39f;
            bulletThickness = 1.2f;
            bulletLength = 15f;
            DamageMean = 28f;
            DamageVariation = 0.2f;
            AlphaDamage = 0.6f;
            DistanceConvexity = -0.2f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT9mmShell(x, y)
            {
                hSpeed = (1.5f + Rando.Float(-0.3f, 0.3f)) * dir,
                vSpeed = -1.5f + Rando.Float(-0.2f, 0.2f),
            };
            add(shell);
        }
    }
}

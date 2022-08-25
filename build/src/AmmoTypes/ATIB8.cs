using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATIB8 : BaseAmmoType
    {
        public ATIB8()
        {
            range = 450f;
            accuracy = 0.82f;
            penetration = 0.4f;
            bulletSpeed = 30f;
            bulletThickness = 0.8f;
            bulletLength = 0f;
            DamageMean = 29f;
            DamageVariation = 0.2f;
            AlphaDamage = 0.7f;
            DistanceConvexity = -0.5f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT9mmShell(x, y)
            {
                hSpeed = Rando.Float(-0.1f, 0.1f) * dir,
                vSpeed = -2.25f + Rando.Float(-0.4f, 0.4f),
            };
            add(shell);
        }
    }
}

#if DEBUG
using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class AT9mmS : BaseAmmoType
    {
        public AT9mmS()
        {
            penetration = 0.4f;
            bulletSpeed = 31f;
            bulletThickness = 0.8f;
            bulletLength = 0f;
            DamageMean = 17f;
            DamageVariation = 0.3f;
            AlphaDamage = 0.4f;
            DistanceConvexity = -1f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT9mmShell(x, y)
            {
                hSpeed = (3f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = -2.25f + Rando.Float(-0.4f, 0.4f),
            };
            add(shell);
        }
    }
}
#endif

#if FEATURES_1_2_X
using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATBersaMagnum : BaseAmmoType
    {
        public ATBersaMagnum()
        {
            range = 150f;
            accuracy = 0.76f;
            penetration = 2.1f;
            bulletSpeed = 48f;
            bulletThickness = 2f;
            bulletLength = 64f;
            DamageMean = 59f;
            DamageVariation = 0.2f;
            AlphaDamage = 0.64f;
            DistanceConvexity = -0.2f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT545NATOShell(x, y)
            {
                hSpeed = (1f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = -2.25f + Rando.Float(-0.4f, 0.4f),
            };
            add(shell);
        }
    }
}
#endif

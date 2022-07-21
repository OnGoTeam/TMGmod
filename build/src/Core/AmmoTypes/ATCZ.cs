using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATCZ : BaseAmmoType
    {
        public ATCZ()
        {
            range = 330f;
            accuracy = 0.87f;
            penetration = 1f;
            bulletSpeed = 40f;
            bulletThickness = 1f;
            bulletLength = 50f;
            DamageMean = 28f;
            DamageVariation = 0.15f;
            AlphaDamage = 0.4f;
            DistanceConvexity = 2f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT556NATOShell(x, y)
            {
                hSpeed = (2.5f + Rando.Float(-0.2f, 0.2f)) * dir,
                vSpeed = 2f + Rando.Float(-0.3f, 0.3f),
            };
            add(shell);
        }
    }

    // ReSharper disable once InconsistentNaming
    public class ATCZ2 : ATCZ
    {
        public ATCZ2()
        {
            range = 310f;
        }
    }
}

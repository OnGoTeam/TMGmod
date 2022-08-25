using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATVintorez : BaseAmmoType
    {
        public ATVintorez()
        {
            range = 550f;
            accuracy = 0.9f;
            bulletSpeed = 17f;
            penetration = 0.4f;
            bulletThickness = 0.8f;
            bulletLength = 0f;
            DamageMean = 35f;
            DamageVariation = 0.2f;
            AlphaDamage = 0.4f;
            DistanceConvexity = 1.3f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new ATSP6Shell(x, y)
            {
                hSpeed = Rando.Float(-0.1f, 0.1f) * dir,
                vSpeed = -2.25f + Rando.Float(-0.4f, 0.4f),
            };
            add(shell);
        }
    }
}

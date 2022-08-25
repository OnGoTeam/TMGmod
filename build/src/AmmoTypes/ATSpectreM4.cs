using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATSpectreM4 : BaseAmmoType
    {
        public ATSpectreM4()
        {
            range = 145f;
            accuracy = 0.76f;
            penetration = 1f;
            bulletSpeed = 19f;
            bulletThickness = 0.8f;
            bulletLength = 15f;
            DamageMean = 31f;
            DamageVariation = 0.2f;
            AlphaDamage = 0.6f;
            DistanceConvexity = -0.2f;
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

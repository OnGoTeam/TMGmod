using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATM93R : BaseAmmoType
    {
        public ATM93R()
        {
            range = 70f;
            accuracy = 0.6f;
            penetration = 0.4f;
            bulletSpeed = 39f;
            bulletThickness = 0.8f;
            bulletLength = 15f;
            DamageMean = 31f;
            DamageVariation = 0.3f;
            AlphaDamage = 0.64f;
            DistanceConvexity = -0.2f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT9mmShell(x, y)
            {
                hSpeed = (1f + Rando.Float(-0.3f, 0.3f)) * dir,
                vSpeed = -1.5f + Rando.Float(-0.2f, 0.2f),
            };
            add(shell);
        }
    }
}

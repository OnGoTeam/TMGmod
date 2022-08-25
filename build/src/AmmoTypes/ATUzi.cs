using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATUzi : BaseAmmoType
    {
        public ATUzi()
        {
            range = 70f;
            accuracy = 0.61f;
            penetration = 0.4f;
            bulletSpeed = 35f;
            bulletThickness = 0.8f;
            bulletLength = 16f;
            DamageMean = 21f;
            DamageVariation = 0.3f;
            AlphaDamage = 0.6f;
            DistanceConvexity = -1f;
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

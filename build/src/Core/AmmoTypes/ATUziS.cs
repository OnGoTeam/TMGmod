using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATUziS : BaseAmmoType
    {
        public ATUziS()
        {
            range = 100f;
            accuracy = 0.8f;
            penetration = 0.4f;
            bulletSpeed = 32f;
            bulletThickness = 0.8f;
            bulletLength = 0f;
            DamageMean = 19f;
            DamageVariation = 0.4f;
            AlphaDamage = 0.7f;
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

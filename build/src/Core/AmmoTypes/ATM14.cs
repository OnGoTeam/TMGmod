using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATM14 : BaseAmmoType
    {
        public ATM14()
        {
            range = 666f;
            accuracy = .95f;
            penetration = 2f;
            bulletSpeed = 29f;
            bulletThickness = 1.5f;
            bulletLength = 40f;
            DamageMean = 46f;
            DamageVariation = 0.2f;
            AlphaDamage = 0.5f;
            DistanceConvexity = -1f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT762NATOShell(x, y)
            {
                hSpeed = (3f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = -2.25f + Rando.Float(-0.4f, 0.4f),
            };
            add(shell);
        }
    }
}

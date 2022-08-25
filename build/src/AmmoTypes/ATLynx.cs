using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATLynx : BaseAmmoType
    {
        public ATLynx()
        {
            range = 1200f;
            accuracy = 1f;
            penetration = 2f;
            bulletSpeed = 48f;
            bulletThickness = 2f;
            bulletLength = 58f;
            DamageMean = 103f;
            DamageVariation = 0.05f;
            AlphaDamage = 0.9f;
            DistanceConvexity = -1.5f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT762NATOShell(x, y) //spinershell
            {
                hSpeed = Rando.Float(-0.1f, 0.1f) * dir,
                vSpeed = -2.25f + Rando.Float(-0.4f, 0.4f),
            };
            add(shell);
        }
    }
}

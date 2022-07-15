using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATARS : BaseAmmoType
    {
        public ATARS()
        {
            range = 350f;
            accuracy = 0.81f;
            bulletSpeed = 40f;
            penetration = 1f;
            deadly = true;
            bulletThickness = 0.8f;
            bulletLength = 0f;
            immediatelyDeadly = true;
            DamageMean = 40f;
            DamageVariation = 0.11f;
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

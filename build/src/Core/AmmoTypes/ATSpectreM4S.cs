using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATSpectreM4S : BaseAmmoType
    {
        public ATSpectreM4S()
        {
            range = 167f;
            accuracy = 0.92f;
            penetration = 1f;
            bulletSpeed = 16f;
            deadly = true;
            bulletThickness = 0.8f;
            bulletLength = 0f;
            immediatelyDeadly = true;
            DamageMean = 30f;
            DamageVariation = 0.15f;
            AlphaDamage = 0.6f;
            DistanceConvexity = -0.2f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT9mmShell(x, y)
            {
                hSpeed = (2f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = -2.25f + Rando.Float(-0.4f, 0.4f),
            };
            add(shell);
        }
    }
}

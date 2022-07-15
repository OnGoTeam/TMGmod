using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATCalico : BaseAmmoType
    {
        public ATCalico()
        {
            accuracy = 0.4f;
            penetration = 0.4f;
            bulletSpeed = 44f;
            deadly = true;
            bulletThickness = 0.5f;
            bulletLength = 5f;
            immediatelyDeadly = true;
            DamageMean = 23f;
            DamageVariation = 0.5f;
            AlphaDamage = 0.5f;
            DistanceConvexity = -0.1f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT9mmShell(x, y)
            {
                hSpeed = 0f,
                vSpeed = 0f + Rando.Float(-0.6f, -0.2f),
            };
            add(shell);
        }
    }
}

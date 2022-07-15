using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATMP5 : BaseAmmoType
    {
        public ATMP5()
        {
            range = 215f;
            accuracy = 0.7f;
            penetration = 1f;
            bulletSpeed = 39f;
            deadly = true;
            bulletThickness = 0.8f;
            bulletLength = 0f;
            immediatelyDeadly = true;
            DamageMean = 28f;
            DamageVariation = 0.12f;
            AlphaDamage = 0.45f;
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

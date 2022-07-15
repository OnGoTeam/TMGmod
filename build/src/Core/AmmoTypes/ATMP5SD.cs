using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATMP5SD : BaseAmmoType
    {
        public ATMP5SD()
        {
            range = 240f;
            accuracy = 0.77f;
            penetration = 0.4f;
            bulletSpeed = 31f;
            deadly = true;
            bulletThickness = 0.8f;
            bulletLength = 0f;
            immediatelyDeadly = true;
            DamageMean = 26f;
            DamageVariation = 0.1f;
            AlphaDamage = 0.4f;
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

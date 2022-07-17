using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATHazeS : BaseAmmoType
    {
        public ATHazeS()
        {
            accuracy = 0.9f;
            range = 150f;
            bulletSpeed = 60f;
            penetration = 1f;
            bulletThickness = 0.8f;
            bulletLength = 15f;
            immediatelyDeadly = true;
            DamageMean = 25f;
            DamageVariation = 0.05f;
            AlphaDamage = 0.81f;
            DistanceConvexity = -0.2f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT9mmShell(x, y)
            {
                hSpeed = (1.5f + Rando.Float(-0.3f, 0.3f)) * dir,
                vSpeed = -1.5f + Rando.Float(-0.2f, 0.2f),
            };
            add(shell);
        }
    }
}

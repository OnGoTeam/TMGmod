using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATMP40 : BaseAmmoType
    {
        public ATMP40()
        {
            range = 215f;
            accuracy = 0.7f;
            penetration = 0.7f;
            bulletSpeed = 28f;
            deadly = true;
            bulletThickness = 1.2f;
            bulletLength = 10f;
            immediatelyDeadly = true;
            BulletDamage = 33f;
            DeltaDamage = 0.12f;
            AlphaDamage = 0.33f;
            DistanceConvexity = -1f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT40ACPShell(x, y)
            {
                hSpeed = (3f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = -1.5f + Rando.Float(-0.5f, 0.5f),
            };
            add(shell);
        }
    }
}

using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATMP7 : BaseAmmoType
    {
        public ATMP7()
        {
            range = 175f;
            accuracy = 0.9f;
            penetration = 0.4f;
            bulletSpeed = 18f;
            deadly = true;
            bulletThickness = 0.8f;
            bulletLength = 0f;
            immediatelyDeadly = true;
            BulletDamage = 27f;
            DeltaDamage = 0.4f;
            AlphaDamage = 0.2f;
            DistanceConvexity = -0.5f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new ATMP7Shell(x, y)
            {
                hSpeed = (1.5f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = -2.5f + Rando.Float(-0.4f, 0.4f),
            };
            add(shell);
        }
    }
}

using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATAR9SX : BaseAmmoType
    {
        public ATAR9SX()
        {
            range = 224f;
            accuracy = 0.88f;
            penetration = 0.4f;
            bulletSpeed = 44f;
            deadly = true;
            bulletThickness = 0.8f;
            bulletLength = 10f;
            immediatelyDeadly = true;
            BulletDamage = 27f;
            DeltaDamage = 0.2f;
            AlphaDamage = 0.4f;
            DistanceConvexity = -0.8f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT9mmShell(x, y)
            {
                hSpeed = (1.5f + Rando.Float(-0.2f, 0.2f)) * dir,
                vSpeed = -1.5f + Rando.Float(-0.2f, 0.2f),
            };
            add(shell);
        }
    }
}

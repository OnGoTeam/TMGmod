using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATPPK42 : BaseAmmoType
    {
        public ATPPK42()
        {
            range = 200f;
            accuracy = 0.8f;
            penetration = 0.5f;
            bulletSpeed = 44f;
            deadly = true;
            bulletThickness = 1f;
            bulletLength = 14f;
            immediatelyDeadly = true;
            BulletDamage = 28f;
            DeltaDamage = 0f;
            AlphaDamage = 0.41f;
            DistanceConvexity = -0.67f;
        }

        public override void PopShell(float x, float y, int dir)
        {
            var shell = new AT40ACPShell(x, y)
            {
                hSpeed = (3f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = -1.5f + Rando.Float(-0.5f, 0.5f),
            };
            Level.Add(shell);
        }
    }
}

using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATANP73 : BaseAmmoType
    {
        public ATANP73()
        {
            range = 195f;
            accuracy = 0.81f;
            penetration = 1f;
            bulletSpeed = 12f;
            deadly = true;
            bulletThickness = 0.8f;
            bulletLength = 15f;
            immediatelyDeadly = true;
            BulletDamage = 34f;
            DeltaDamage = 0.6f;
            AlphaDamage = 0f;
            DistanceConvexity = -1f;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var shell = new AT9mmShell(x, y)
            {
                hSpeed = (3f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = -2.25f + Rando.Float(-0.4f, 0.4f)
            };
            Level.Add(shell);
        }
    }
}
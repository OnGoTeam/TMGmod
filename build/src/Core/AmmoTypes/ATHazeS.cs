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
            combustable = true;
            bulletSpeed = 60f;
            penetration = 1f;
            deadly = true;
            bulletThickness = 0.8f;
            bulletLength = 15f;
            immediatelyDeadly = true;
            BulletDamage = 25f;
            DeltaDamage = 0.05f;
            AlphaDamage = 0.81f;
            DistanceConvexity = -0.2f;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var shell = new AT9mmShell(x, y)
            {
                hSpeed = (1.5f + Rando.Float(-0.3f, 0.3f)) * dir,
                vSpeed = -1.5f + Rando.Float(-0.2f, 0.2f)
            };
            Level.Add(shell);
        }
    }
}
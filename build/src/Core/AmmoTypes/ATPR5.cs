using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATPR5 : BaseAmmoType
    {
        public ATPR5()
        {
            range = 100f;
            accuracy = 0.8f;
            penetration = 0.5f;
            bulletSpeed = 41f;
            deadly = true;
            bulletThickness = 1f;
            bulletLength = 8f;
            immediatelyDeadly = true;
            BulletDamage = 32f;
            DeltaDamage = 0.2f;
            AlphaDamage = 0.55f;
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

using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATSR2C : BaseAmmoType
    {
        public ATSR2C()
        {
            range = 560f;
            accuracy = 0.95f;
            penetration = 2f;
            bulletSpeed = 55f;
            deadly = true;
            bulletThickness = 1.5f;
            bulletLength = 40f;
            immediatelyDeadly = true;
            BulletDamage = 57f;
            DeltaDamage = 0.2f;
            AlphaDamage = 0.6f;
            DistanceConvexity = 0.2f;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var shell = new AT762NATOShell(x, y)
            {
                hSpeed = (3f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = 2.25f + Rando.Float(-0.4f, 0.4f)
            };
            Level.Add(shell);
        }
    }
}
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATDaewooK1 : BaseAmmoType
    {
        public ATDaewooK1()
        {
            range = 245f;
            accuracy = 0.83f;
            penetration = 1f;
            bulletSpeed = 33f;
            deadly = true;
            bulletThickness = 0.8f;
            bulletLength = 10f;
            immediatelyDeadly = true;
            BulletDamage = 28f;
            DeltaDamage = 0.12f;
            AlphaDamage = 0.4f;
            DistanceConvexity = -0.2f;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var shell = new AT545NATOShell(x, y)
            {
                hSpeed = (1f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = -1.25f + Rando.Float(-0.4f, 0.4f)
            };
            Level.Add(shell);
        }
    }
}
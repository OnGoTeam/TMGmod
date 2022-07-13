using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATSV99 : BaseAmmoType
    {
        public ATSV99()
        {
            range = 800f;
            accuracy = 0.9f;
            penetration = 1.5f;
            bulletSpeed = 50f;
            deadly = true;
            bulletThickness = 0f;
            bulletLength = 20f;
            immediatelyDeadly = true;
            BulletDamage = 89f;
            DeltaDamage = 0.1f;
            AlphaDamage = 0.8f;
            DistanceConvexity = -1.5f;
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

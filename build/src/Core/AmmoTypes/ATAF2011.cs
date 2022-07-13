using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATAF2011 : BaseAmmoType
    {
        public ATAF2011()
        {
            range = 130f;
            accuracy = 0.95f;
            penetration = 1f;
            bulletSpeed = 46f;
            deadly = true;
            bulletThickness = 0.8f;
            bulletLength = 15f;
            immediatelyDeadly = true;
            BulletDamage = 26f;
            DeltaDamage = 0.25f;
            AlphaDamage = 0.46f;
            DistanceConvexity = -0.7f;
        }

        public override void PopShell(float x, float y, int dir)
        {
            var shell = new AT9mmShell(x, y)
            {
                hSpeed = (1.5f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = -2.25f + Rando.Float(-0.4f, 0.4f)
            };
            Level.Add(shell);
        }
    }
}

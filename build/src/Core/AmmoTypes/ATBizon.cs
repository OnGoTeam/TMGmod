using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATBizon : BaseAmmoType
    {
        public ATBizon()
        {
            range = 115f;
            accuracy = 0.4f;
            penetration = 1f;
            bulletSpeed = 44f;
            deadly = true;
            bulletThickness = 0.8f;
            bulletLength = 10f;
            immediatelyDeadly = true;
            BulletDamage = 30f;
            DeltaDamage = 0.20f;
            AlphaDamage = 0.25f;
            DistanceConvexity = -1.5f;
        }

        public override void PopShell(float x, float y, int dir)
        {
            var shell = new AT9mmShell(x, y)
            {
                hSpeed = (3f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = -1.5f + Rando.Float(-0.5f, 0.5f)
            };
            Level.Add(shell);
        }
    }
}

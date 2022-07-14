using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATFABARM : BaseAmmoType
    {
        public ATFABARM()
        {
            range = 125f;
            accuracy = 0.69f;
            penetration = 1f;
            bulletSpeed = 25f;
            deadly = true;
            weight = 5f;
            bulletThickness = 0.5f;
            immediatelyDeadly = true;
            BulletDamage = 30f;
            DeltaDamage = 0.3f;
            AlphaDamage = 0.33f;
            DistanceConvexity = 3;
        }

        public override void PopShell(float x, float y, int dir)
        {
            var shell = new Gauge12Shell(x, y)
            {
                hSpeed = (2f + Rando.Float(-0.2f, 0.2f)) * dir,
                vSpeed = -1.2f + Rando.Float(-0.5f, 0.5f),
                depth = 0.2f - Rando.Float(0.0f, 0.1f),
            };
            Level.Add(shell);
        }
    }
}

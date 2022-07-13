using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class AT12GaugeS : BaseAmmoType
    {
        public AT12GaugeS() //KS-23' selfAT
        {
            range = 169f;
            accuracy = 0.33f;
            penetration = 1f;
            bulletSpeed = 50f;
            deadly = true;
            weight = 5f;
            bulletThickness = 0f;
            bulletLength = 0f;
            immediatelyDeadly = true;
            BulletDamage = 22f;
            AlphaDamage = 0.67f;
            DistanceConvexity = 1;
        }

        public override void PopShell(float x, float y, int dir)
        {
            var shell = new Gauge12Shell(x, y)
            {
                hSpeed = (2f + Rando.Float(-0.2f, 0.2f)) * dir,
                vSpeed = -1.2f + Rando.Float(-0.5f, 0.5f),
                depth = 0.2f - Rando.Float(0.0f, 0.1f)
            };
            Level.Add(shell);
        }
    }
}

using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class AT545NATO : AmmoType
    {
        public AT545NATO()
        {
            penetration = 1f;
            bulletSpeed = 51f;
            deadly = true;
            bulletThickness = 1f;
            bulletLength = 50f;
            immediatelyDeadly = true;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var Shell = new AT545NATOShell(x, y)
            {
                hSpeed = (2.5f + Rando.Float(-0.2f, 0.2f)) * dir,
                vSpeed = (2f + Rando.Float(-0.3f, 0.3f)) * dir
            };
            Level.Add(Shell);
        }
    }
}
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class AT44DB : AmmoType
    {
        public AT44DB()
        {
            accuracy = 0.1f;
            penetration = 4f;
            bulletSpeed = 125f;
            range = 100f;
            deadly = true;
            weight = 5f;
            bulletThickness = 2f;
            immediatelyDeadly = true;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var shell = new DB44Shell(x, y)
            {
                hSpeed = -1f + Rando.Float(-0.2f, 0.2f),
                vSpeed = 1f + Rando.Float(-0.5f, 0.5f),
                depth = -0.2f - Rando.Float(0.0f, 0.1f)
            };
            Level.Add(shell);
        }
    }
}
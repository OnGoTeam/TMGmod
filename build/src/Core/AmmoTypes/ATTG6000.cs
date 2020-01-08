using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATTG6000 : AmmoType
    {
        public ATTG6000()
        {
            range = 120f;
            accuracy = 0.47f;
            penetration = 1f;
            bulletSpeed = 16f;
            bulletThickness = 0.6f;
            deadly = true;
            weight = 5f;
            immediatelyDeadly = true;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var shell = new Taligator6000Shell(x, y)
            {
                hSpeed = -0.5f + Rando.Float(-0.1f, 0.1f),
                vSpeed = -4.5f + Rando.Float(-0.8f, 0.8f),
                depth = 0.2f - Rando.Float(0.0f, 0.1f)
            };
            Level.Add(shell);
        }
    }
}
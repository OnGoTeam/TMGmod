using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATMG44 : AmmoType
    {
        public ATMG44()
        {
            accuracy = 0.75f;
            penetration = 1.5f;
            bulletSpeed = 36f;
            range = 460f;
            deadly = true;
            weight = 5f;
            bulletThickness = 1f;
            bulletLength = 130f;
            immediatelyDeadly = true;
        }

        public override void PopShell(float x, float y, int dir)
        {
            var shalker = new MG44Shell(x, y) { hSpeed = 0f + Rando.Float(-0.2f, 0.2f) };
            shalker.vSpeed = 1f + Rando.Float(-0.5f, 0.5f);
            shalker.depth = -0.2f - Rando.Float(0.0f, 0.1f);
            Level.Add(shalker);
        }
    }
}
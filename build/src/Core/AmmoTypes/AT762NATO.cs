using DuckGame;
//using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class AT762NATO : AmmoType
    {
        public AT762NATO()
        {
            penetration = 2f;
            bulletSpeed = 44f; //same as ATMagnum (!)
            deadly = true;
            bulletThickness = 1.5f;
            bulletLength = 40f;
            immediatelyDeadly = true;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var pistolShell = new PistolShell(x, y) { hSpeed = dir * (1.5f + Rando.Float(1f)) };
            Level.Add(pistolShell);
        }
    }
}
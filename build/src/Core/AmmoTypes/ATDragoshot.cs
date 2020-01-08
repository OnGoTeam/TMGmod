using DuckGame;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATDragoshot : AmmoType
    {
        public ATDragoshot()
        {
            range = 120f;
            accuracy = 0.7f;
            penetration = 2f;
            bulletThickness = 0.8f;
            bulletSpeed = 36f;
            deadly = true;
            immediatelyDeadly = true;
        }
    }
}
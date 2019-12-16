using DuckGame;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATSRM1 : AmmoType
    {
        public ATSRM1()
        {
            range = 120f;
            accuracy = 0.6f;
            bulletThickness = 0.7f;
            combustable = true;
            bulletSpeed = 35f;
            penetration = 1f;
            barrelAngleDegrees = 8f;
        }
    }
}

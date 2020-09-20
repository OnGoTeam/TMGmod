using DuckGame;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATSRM1 : AmmoType, IDamage
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
            Bulletdamage = 12f;
            Deltadamage = 0.3f;
        }
        public float Bulletdamage { get; }
        public float Deltadamage { get; }
    }
}

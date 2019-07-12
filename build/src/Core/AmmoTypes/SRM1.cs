using DuckGame;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// <see cref="AmmoType"/> with invisible bullets for silenced weapons
    /// </summary>
    public class ATSRM1 : AmmoType
    {
        /// <summary>
        /// sets bulletLength to 0f for invisibility
        /// </summary>
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

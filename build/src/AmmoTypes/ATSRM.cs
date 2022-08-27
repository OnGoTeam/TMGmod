using TMGmod.Core.AmmoTypes;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATSRM : BaseAmmoType
    {
        public ATSRM()
        {
            range = 120f;
            accuracy = 0.6f;
            bulletThickness = 0.7f;
            bulletSpeed = 71f;
            penetration = 1f;
            barrelAngleDegrees = -6.5f;
            DamageMean = 15f;
            DamageVariation = 0.3f;
            AlphaDamage = 0.5f;
        }
    }
}

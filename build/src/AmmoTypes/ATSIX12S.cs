using TMGmod.Core.AmmoTypes;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATSIX12S : BaseAmmoType
    {
        public ATSIX12S()
        {
            range = 180f;
            accuracy = 0.9f;
            penetration = 0.4f;
            bulletSpeed = 21f;
            weight = 5f;
            bulletThickness = 0f;
            bulletLength = 0f;
            DamageMean = 11f;
            DamageVariation = 0.2f;
            AlphaDamage = 0.5f;
            DistanceConvexity = 1;
        }
    }
}

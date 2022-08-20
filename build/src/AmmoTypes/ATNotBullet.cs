using TMGmod.Core.AmmoTypes;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATNB : BaseAmmoType
    {
        public ATNB()
        {
            bulletLength = 1f;
            bulletThickness = 0.1f;
            accuracy = 1f;
            combustable = true;
            bulletSpeed = 5f;
            range = 50f;
            penetration = 228f;
            AlphaDamage = 1f;
            DamageMean = 89.241f;
            DamageVariation = .25f;
        }
    }
}

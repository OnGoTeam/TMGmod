using TMGmod.Core.Bullets;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class AT50C : BaseAmmoType
    {
        public AT50C()
        {
            accuracy = 1f;
            range = 200f;
            penetration = 2f;
            combustable = true;
            bulletSpeed = 45f;
            bulletType = typeof(AntiProp);
            bulletThickness = 3.5f;
            DamageMean = 64f;
            DamageVariation = 0.2f;
            AlphaDamage = 0.5f;
            DistanceConvexity = -1f;
        }
    }
}

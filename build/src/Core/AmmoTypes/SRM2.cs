namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATSRM2 : BaseAmmoType
    {
        public ATSRM2()
        {
            range = 120f;
            accuracy = 0.6f;
            bulletThickness = 0.7f;
            combustable = true;
            bulletSpeed = 71f;
            penetration = 1f;
            barrelAngleDegrees = 6.5f;
            DamageMean = 15f;
            DamageVariation = 0.3f;
            AlphaDamage = 0.5f;
        }
    }
}

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public sealed class ATButcher : BaseAmmoType
    {
        public ATButcher()
        {
            barrelAngleDegrees = -5f;
            accuracy = 0.95f;
            penetration = 3f;
            bulletSpeed = 9f;
            range = 250f;
            weight = 5f;
            bulletThickness = 1f;
            bulletLength = 20f;
            DamageMean = 12f;
            DamageVariation = 0.25f;
            AlphaDamage = 0.5f;
            DistanceConvexity = -1f;
        }
    }
}

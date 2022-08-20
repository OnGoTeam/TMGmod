namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATFABARM : AT12Gauge
    {
        public ATFABARM()
        {
            range = 180f;
            accuracy = 0.67f;
            bulletThickness = 1.5f;
            penetration = 1f;
            bulletSpeed = 25f;
            weight = 5f;
            DamageMean = 26f;
            DamageVariation = 0.5f;
            AlphaDamage = 1f;
            DistanceConvexity = 1.2f;
        }
    }
}

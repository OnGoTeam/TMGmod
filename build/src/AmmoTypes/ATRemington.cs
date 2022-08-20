namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATRemington : AT12Gauge
    {
        public ATRemington()
        {
            range = 125f;
            accuracy = 0.69f;
            penetration = 1f;
            bulletSpeed = 25f;
            weight = 5f;
            bulletThickness = 0.5f;
            DamageMean = 30f;
            DamageVariation = 0.3f;
            AlphaDamage = 0.33f;
            DistanceConvexity = 3;
        }
    }
}

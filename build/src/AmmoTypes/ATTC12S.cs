namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATTC12S : ATTC12
    {
        public ATTC12S()
        {
            range = 330f;
            accuracy = 0.97f;
            penetration = 1f;
            bulletSpeed = 34f;
            bulletThickness = 1f;
            bulletLength = 50f;
            DamageMean = 36f;
            DamageVariation = 0.18f;
            AlphaDamage = 0.5f;
            DistanceConvexity = 50f;
        }
    }
}

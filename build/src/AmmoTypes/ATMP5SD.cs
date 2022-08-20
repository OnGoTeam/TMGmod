namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATMP5SD : ATMP5
    {
        public ATMP5SD()
        {
            range = 240f;
            accuracy = 0.77f;
            penetration = 0.4f;
            bulletSpeed = 31f;
            bulletThickness = 0f;
            DamageMean = 26f;
            DamageVariation = 0.1f;
            AlphaDamage = 0.4f;
        }
    }
}

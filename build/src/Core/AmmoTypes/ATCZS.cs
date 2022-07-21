namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATCZS : BaseAmmoType
    {
        public ATCZS()
        {
            range = 380f;
            accuracy = 0.95f;
            penetration = 0.4f;
            bulletSpeed = 37f;
            bulletThickness = 0f;
            DamageMean = 26f;
            DamageVariation = 0.1f;
            AlphaDamage = 0.37f;
        }
    }

    // ReSharper disable once InconsistentNaming
    public class ATCZS2 : ATCZS
    {
        public ATCZS2()
        {
            range = 350f;
        }
    }
}

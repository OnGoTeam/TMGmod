namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATSRM1 : BaseAmmoType
    {
        public ATSRM1()
        {
            range = 120f;
            accuracy = 0.6f;
            bulletThickness = 0.7f;
            combustable = true;
            bulletSpeed = 71f;
            penetration = 1f;
            barrelAngleDegrees = -6.5f;
            BulletDamage = 15f;
            DeltaDamage = 0.3f;
            AlphaDamage = 0.5f;
        }
    }
}

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
            bulletSpeed = 35f;
            penetration = 1f;
            barrelAngleDegrees = 8f;
            BulletDamage = 12f;
            DeltaDamage = 0.3f;
        }
    }
}

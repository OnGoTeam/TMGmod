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
            bulletSpeed = 35f;
            penetration = 1f;
            barrelAngleDegrees = -8f;
            BulletDamage = 12f;
            DeltaDamage = 0.3f;
        }
    }
}

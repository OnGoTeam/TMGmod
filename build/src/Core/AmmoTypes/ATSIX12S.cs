namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATSIX12S : BaseAmmoType
    {
        public ATSIX12S()
        {
            range = 180f;
            accuracy = 0.9f;
            penetration = 0.4f;
            bulletSpeed = 21f;
            deadly = true;
            weight = 5f;
            bulletThickness = 0f;
            bulletLength = 0f;
            immediatelyDeadly = true;
            BulletDamage = 11f;
            DeltaDamage = 0.2f;
            AlphaDamage = 0.5f;
            DistanceConvexity = 1;
        }
    }
}

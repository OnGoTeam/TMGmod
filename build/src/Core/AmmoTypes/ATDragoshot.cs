
namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATDragoshot : BaseAmmoType
    {
        public ATDragoshot()
        {
            range = 120f;
            accuracy = 0.7f;
            penetration = 2f;
            bulletThickness = 0.8f;
            bulletSpeed = 36f;
            deadly = true;
            immediatelyDeadly = true;
            BulletDamage = 21f;
            DeltaDamage = 0.2f;
            AlphaDamage = 0.5f;
            DistanceConvexity = 1;
        }
    }
}
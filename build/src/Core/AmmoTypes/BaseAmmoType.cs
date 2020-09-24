using DuckGame;

namespace TMGmod.Core.AmmoTypes
{
    public abstract class BaseAmmoType : AmmoType, IDamage
    {
        public float BulletDamage { get; set; }
        public float DeltaDamage { get; set; }
        public float DistanceConvexity { get; set; }
        public float AlphaDamage { get; set; }

        protected BaseAmmoType()
        {
            BulletDamage = 50f;
            DeltaDamage = 1f;
            DistanceConvexity = 0f;
            AlphaDamage = 0f;
        }
    }
}
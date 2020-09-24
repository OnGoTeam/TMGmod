using DuckGame;

namespace TMGmod.Core.AmmoTypes
{
    public abstract class BaseAmmoType : AmmoType, IDamage
    {
        public float BulletDamage { get; protected set; }
        public float DeltaDamage { get; protected set; }
        public float DistanceConvexity { get; protected set; }
        public float AlphaDamage { get; protected set; }

        protected BaseAmmoType()
        {
            BulletDamage = 50f;
            DeltaDamage = 1f;
            DistanceConvexity = 0f;
            AlphaDamage = 0f;
        }
    }
}
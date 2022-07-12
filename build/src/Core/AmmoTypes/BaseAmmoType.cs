using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.AmmoTypes
{
    public abstract class BaseAmmoType : AmmoType, IDamage
    {
        [UsedImplicitly]
        public float BulletDamage { get; set; }
        [UsedImplicitly]
        public float DeltaDamage { get; set; }
        [UsedImplicitly]
        public float DistanceConvexity { get; set; }
        [UsedImplicitly]
        public float AlphaDamage { get; set; }

        protected BaseAmmoType()
        {
            BulletDamage = 50f;
            DeltaDamage = 1f;
            DistanceConvexity = 0f;
            AlphaDamage = 0.01f;
        }
    }
}
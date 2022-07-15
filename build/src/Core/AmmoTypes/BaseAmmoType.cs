using System;
using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.AmmoTypes
{
    public abstract class BaseAmmoType : AmmoType, IDamage
    {
        protected BaseAmmoType()
        {
            BulletDamage = 50f;
            DeltaDamage = 1f;
            DistanceConvexity = 0f;
            AlphaDamage = 0.01f;
        }

        [UsedImplicitly] public float BulletDamage { get; set; }

        [UsedImplicitly] public float DeltaDamage { get; set; }

        [UsedImplicitly] public float DistanceConvexity { get; set; }

        [UsedImplicitly] public float AlphaDamage { get; set; }

        public virtual void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
        }
        public sealed override void PopShell(float x, float y, int dir) => PopShell(x, y, dir, Level.Add);
    }
}

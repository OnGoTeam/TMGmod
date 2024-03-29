using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATPPSH41 : BaseAmmoType
    {
        public ATPPSH41()
        {
            range = 160f;
            accuracy = 0.73f;
            penetration = 0.4f;
            bulletSpeed = 44f;
            bulletThickness = 0.8f;
            bulletLength = 14f;
            DamageMean = 27f;
            DamageVariation = 0f;
            AlphaDamage = 0.35f;
            DistanceConvexity = -0.75f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT40ACPShell(x, y)
            {
                hSpeed = Rando.Float(-0.1f, 0.1f) * dir,
                vSpeed = -1.5f + Rando.Float(-0.5f, 0.5f),
            };
            add(shell);
        }
    }
}

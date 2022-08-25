using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATUSP : BaseAmmoType
    {
        public ATUSP()
        {
            range = 100f;
            accuracy = 0.8f;
            penetration = 1f;
            bulletSpeed = 39f;
            bulletThickness = 1.2f;
            bulletLength = 15f;
            DamageMean = 28f;
            DamageVariation = 0.2f;
            AlphaDamage = 0.6f;
            DistanceConvexity = -0.2f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT9mmShell(x, y)
            {
                hSpeed = (3f + Rando.Float(-0.3f, 0.3f)) * dir,
                vSpeed = -1.5f + Rando.Float(-0.2f, 0.2f),
            };
            add(shell);
        }
    }
}

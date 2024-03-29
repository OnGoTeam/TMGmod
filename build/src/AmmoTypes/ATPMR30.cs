using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATPMR30 : BaseAmmoType
    {
        public ATPMR30()
        {
            range = 110f;
            accuracy = 0.7f;
            penetration = 0.45f;
            bulletSpeed = 46f;
            bulletThickness = 0.8f;
            bulletLength = 15f;
            DamageMean = 23f;
            DamageVariation = 0.4f;
            AlphaDamage = 0.51f;
            DistanceConvexity = -0.5f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT9mmShell(x, y)
            {
                hSpeed = (1f + Rando.Float(-0.3f, 0.3f)) * dir,
                vSpeed = -1.5f + Rando.Float(-0.2f, 0.2f),
            };
            add(shell);
        }
    }
}

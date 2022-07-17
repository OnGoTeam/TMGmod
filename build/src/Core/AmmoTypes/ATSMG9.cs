using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATSMG9 : BaseAmmoType
    {
        public ATSMG9()
        {
            range = 100f;
            accuracy = 0.6f;
            penetration = 0.4f;
            bulletSpeed = 34f;
            bulletThickness = 0.8f;
            bulletLength = 15f;
            DamageMean = 18f;
            DamageVariation = 0.4f;
            AlphaDamage = 0.55f;
            DistanceConvexity = -1f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT9mmShell(x, y)
            {
                hSpeed = (3f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = -2.25f + Rando.Float(-0.4f, 0.4f),
            };
            add(shell);
        }
    }
}

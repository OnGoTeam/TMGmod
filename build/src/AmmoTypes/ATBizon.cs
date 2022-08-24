using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATBizon : BaseAmmoType
    {
        public ATBizon()
        {
            range = 125f;
            accuracy = 0.6f;
            penetration = 1f;
            bulletSpeed = 44f;
            bulletThickness = 0.8f;
            bulletLength = 10f;
            DamageMean = 30f;
            DamageVariation = 0.20f;
            AlphaDamage = 0.25f;
            DistanceConvexity = -1.5f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT9mmShell(x, y)
            {
                hSpeed = Rando.Float(-0.1f, 0.1f) * dir,
                vSpeed = -1.5f + Rando.Float(-0.5f, 0.5f),
            };
            add(shell);
        }
    }
}

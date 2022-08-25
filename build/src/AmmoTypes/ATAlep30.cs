using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATAlep30 : BaseAmmoType
    {
        public ATAlep30()
        {
            range = 125f;
            accuracy = 0.6f;
            penetration = 0.4f;
            bulletSpeed = 20f;
            bulletThickness = 0.8f;
            bulletLength = 15f;
            DamageMean = 21f;
            DamageVariation = 0.1f;
            AlphaDamage = 0.44f;
            DistanceConvexity = -0.2f;
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

using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATSVU : BaseAmmoType
    {
        public ATSVU()
        {
            range = 730f;
            accuracy = 0.95f;
            penetration = 0.5f;
            bulletSpeed = 32f;
            bulletThickness = 0.8f;
            bulletLength = 0f;
            DamageMean = 49f;
            DamageVariation = 0.1f;
            AlphaDamage = 0.75f;
            DistanceConvexity = -1f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT556NATOShell(x, y) // TODO: AT762x54Shell
            {
                hSpeed = (1.9f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = -1.5f + Rando.Float(-0.4f, 0.4f),
            };
            add(shell);
        }
    }
}

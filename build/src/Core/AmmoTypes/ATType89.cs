using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATType89 : BaseAmmoType
    {
        public ATType89()
        {
            range = 366f;
            accuracy = 0.85f;
            penetration = 1f;
            bulletSpeed = 37f;
            bulletThickness = 1f;
            bulletLength = 50f;
            DamageMean = 22f;
            DamageVariation = 0.15f;
            AlphaDamage = 0.5f;
            DistanceConvexity = -1f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT545NATOShell(x, y)
            {
                hSpeed = (2.5f + Rando.Float(-0.2f, 0.2f)) * dir,
                vSpeed = 2f + Rando.Float(-0.3f, 0.3f),
            };
            add(shell);
        }
    }
}

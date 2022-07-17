using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATBersa45 : BaseAmmoType
    {
        public ATBersa45()
        {
            range = 90f;
            accuracy = 0.76f;
            penetration = 0.4f;
            bulletSpeed = 25f;
            bulletThickness = 2f;
            bulletLength = 64f;
            DamageMean = 32f;
            DamageVariation = 0.2f;
            AlphaDamage = 0.64f;
            DistanceConvexity = -0.2f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT9mmShell(x, y)
            {
                hSpeed = (3f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = 2.25f + Rando.Float(-0.4f, 0.4f),
            };
            add(shell);
        }
    }
}

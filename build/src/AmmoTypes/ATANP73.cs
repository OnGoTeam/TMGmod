using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATANP73 : BaseAmmoType
    {
        public ATANP73()
        {
            range = 153f;
            accuracy = 0.81f;
            penetration = 1f;
            bulletSpeed = 12f;
            bulletThickness = 0.8f;
            bulletLength = 15f;
            DamageMean = 34f;
            DamageVariation = 0.6f;
            AlphaDamage = 0f;
            DistanceConvexity = -1f;
        }
        
        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT9mmShell(x, y)
            {
                hSpeed = Rando.Float(-0.1f, 0.1f) * dir,
                vSpeed = -2.25f + Rando.Float(-0.4f, 0.4f),
            };
            add(shell);
        }
    }
}

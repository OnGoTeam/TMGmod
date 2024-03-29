using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATAR9SX : BaseAmmoType
    {
        public ATAR9SX()
        {
            range = 274f;
            accuracy = 0.78f;
            penetration = 0.4f;
            bulletSpeed = 44f;
            bulletThickness = 0.8f;
            bulletLength = 10f;
            DamageMean = 27f;
            DamageVariation = 0.2f;
            AlphaDamage = 0.4f;
            DistanceConvexity = -0.8f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT9mmShell(x, y)
            {
                hSpeed = Rando.Float(-0.2f, 0.2f) * dir,
                vSpeed = -1.5f + Rando.Float(-0.2f, 0.2f),
            };
            add(shell);
        }
    }
}

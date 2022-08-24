#if DEBUG
using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class AT9mmParabellum : BaseAmmoType
    {
        public AT9mmParabellum() //This is the SMG9 selfAT
        {
            accuracy = .9f;
            penetration = .4f;
            range = 300f;
            bulletSpeed = 46f;
            bulletThickness = 0.8f;
            bulletLength = 15f;
            DamageMean = 31f;
            DamageVariation = 0.3f;
            AlphaDamage = 0.64f;
            DistanceConvexity = -0.2f;
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
#endif

using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes

{
    // ReSharper disable once InconsistentNaming
    public class ATCZS : BaseAmmoType
    {
        public ATCZS()
        {
            range = 380f;
            accuracy = 0.95f;
            penetration = 0.4f;
            bulletSpeed = 37f;
            bulletThickness = 0f;
            DamageMean = 26f;
            DamageVariation = 0.1f;
            AlphaDamage = 0.37f;
        }
        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT556NATOShell(x, y)
            {
                hSpeed = (2f + Rando.Float(-0.2f, 0.2f)) * dir,
                vSpeed = -0.5f + Rando.Float(-0.3f, 0.3f),
            };
            add(shell);
        }
    }

    // ReSharper disable once InconsistentNaming
    public class ATCZS2 : ATCZS
    {
        public ATCZS2()
        {
            range = 350f;
        }
    }
}

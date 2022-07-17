using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class AT50SniperS : BaseAmmoType
    {
        public AT50SniperS()
        {
            penetration = 1f;
            bulletSpeed = 37f;
            bulletThickness = 0f;
            bulletLength = 40f;
            DamageMean = 106f;
            DamageVariation = 0.07f;
            AlphaDamage = 1f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT762NATOShell(x, y) //spinershell
            {
                hSpeed = (3f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = -2.25f + Rando.Float(-0.4f, 0.4f),
            };
            add(shell);
        }
    }
}

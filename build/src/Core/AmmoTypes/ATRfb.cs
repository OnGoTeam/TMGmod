using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATRfb : BaseAmmoType
    {
        public ATRfb()
        {
            range = 380f;
            accuracy = 0.9f;
            penetration = 1f;
            bulletSpeed = 37f;
            bulletThickness = 1f;
            bulletLength = 50f;
            DamageMean = 22f;
            DamageVariation = 0.15f;
            AlphaDamage = 0.5f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT545NATOShell(x, y)
            {
                hSpeed = Rando.Float(0.15f, 0.35f) * -dir,
                vSpeed = 0f,
            };
            add(shell);
        }
    }
}
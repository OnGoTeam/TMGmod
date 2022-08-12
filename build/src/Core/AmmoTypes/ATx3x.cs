using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public sealed class ATx3x : BaseAmmoType
    {
        public ATx3x()
        {
            bulletLength = 45f;
            combustable = true;
            bulletSpeed = 15f;
            range = 800f;
            accuracy = 1f;
            penetration = 100f;
            bulletThickness = 4f;
            DamageMean = 365f;
            DamageVariation = 0f;
            AlphaDamage = 1f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            PopShellSkin(x, y, 0, add);
        }

        public static void PopShellSkin(float x, float y, int frameid, Action<EjectedShell> add)
        {
            var flyingtoilet = new X3XShell(x, y, frameid)
            {
                hSpeed = 7f + Rando.Float(1f),
            };
            add(flyingtoilet);
        }
    }
}

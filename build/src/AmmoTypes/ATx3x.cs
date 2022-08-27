using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public sealed class ATx3x : BaseAmmoType
    {
        public ATx3x()
        {
            bulletLength = 45f;
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
            PopShellSkin(x, y, dir, 0, add);
        }

        public static void PopShellSkin(float x, float y, int dir, int frameid, Action<EjectedShell> add)
        {
            var flyingtoilet = new X3XShell(x, y)
            {
                hSpeed = Rando.Float(-.5f, .5f) * dir,
                FrameId = frameid,
            };
            add(flyingtoilet);
        }
    }
}

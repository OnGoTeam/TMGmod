using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public sealed class ATTG6000 : BaseAmmoType
    {
        public ATTG6000()
        {
            range = 120f;
            accuracy = 0.47f;
            penetration = 1f;
            bulletSpeed = 16f;
            bulletThickness = 0.6f;
            weight = 5f;
            DamageMean = 16f;
            DamageVariation = 0.2f;
            AlphaDamage = 0.5f;
        }
        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            PopShellSkin(x, y, 0, add);
        }

        public static void PopShellSkin(float x, float y, int frameid, Action<EjectedShell> add)
        {
            var shell = new Taligator6000Shell(x, y, frameid)
            {
                hSpeed = 0.5f + Rando.Float(-0.1f, 0.1f),
                vSpeed = -4.5f + Rando.Float(-0.8f, 0.8f),
                depth = 0.2f - Rando.Float(0.0f, 0.1f),
            };
            add(shell);
        }
    }
}

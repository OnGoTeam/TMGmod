using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public sealed class ATMG44 : BaseAmmoType
    {
        public ATMG44()
        {
            accuracy = 0.75f;
            penetration = 1.5f;
            bulletSpeed = 36f;
            range = 460f;
            weight = 5f;
            bulletThickness = 1f;
            bulletLength = 130f;
            DamageMean = 27f;
            DamageVariation = 0.09f;
            AlphaDamage = 0.6f;
            DistanceConvexity = -1f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            PopShellSkin(x, y, dir, 0, add);
        }

        public static void PopShellSkin(float x, float y, int dir, int frameid, Action<EjectedShell> add)
        {
            var shalker = new MG44Shell(x, y)
            {
                hSpeed = Rando.Float(-0.2f, 0.2f) * dir,
                vSpeed = 1f + Rando.Float(-0.5f, 0.5f),
                depth = -0.2f - Rando.Float(0.0f, 0.1f),
                FrameId = frameid,
            };
            add(shalker);
        }
    }
}

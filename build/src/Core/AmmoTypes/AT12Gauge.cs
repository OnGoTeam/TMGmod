using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class AT12Gauge : BaseAmmoType
    {
        public AT12Gauge()
        {
            accuracy = 0.4f;
            penetration = 1f;
            bulletSpeed = 23f;
            range = 100f;
            deadly = true;
            weight = 5f;
            bulletThickness = 2f;
            immediatelyDeadly = true;
            DamageMean = 11f;
            AlphaDamage = 0.5f;
            DistanceConvexity = 1;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new Gauge12Shell(x, y)
            {
                hSpeed = (2f + Rando.Float(-0.2f, 0.2f)) * dir,
                vSpeed = -1.2f + Rando.Float(-0.5f, 0.5f),
                depth = 0.2f - Rando.Float(0.0f, 0.1f),
            };
            add(shell);
        }
    }
}

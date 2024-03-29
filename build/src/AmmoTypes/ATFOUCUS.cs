#if FEATURES_1_2_X
using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATFOUCUS : BaseAmmoType
    {
        public ATFOUCUS()
        {
            range = 405f;
            accuracy = 0.92f;
            penetration = 2.1f;
            bulletSpeed = 38f;
            bulletThickness = 1f;
            bulletLength = 0f;
            DamageMean = 52f;
            DamageVariation = 0.05f;
            AlphaDamage = 0.9f;
            DistanceConvexity = 0f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT762NATOShell(x, y)
            {
                hSpeed = Rando.Float(-0.2f, 0.2f) * dir,
                vSpeed = 3.5f + Rando.Float(-0.3f, 0.3f),
            };
            add(shell);
        }

        public override void WriteAdditionalData(BitBuffer b)
        {
            b.Write(penetration);
            base.WriteAdditionalData(b);
        }

        public override void ReadAdditionalData(BitBuffer b)
        {
            base.ReadAdditionalData(b);
            penetration = b.ReadFloat();
        }
    }
}
#endif

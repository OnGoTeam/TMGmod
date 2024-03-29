#if FEATURES_1_2_X
using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATDR300 : BaseAmmoType
    {
        public ATDR300()
        {
            range = 567f;
            accuracy = 0.98f;
            bulletSpeed = 44f;
            penetration = 2.1f;
            bulletThickness = 1f;
            bulletLength = 38f;
            DamageMean = 36f;
            DamageVariation = 0.1f;
            AlphaDamage = 0.75f;
            DistanceConvexity = 0f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT556NATOShell(x, y)
            {
                hSpeed = Rando.Float(-0.2f, 0.2f) * dir,
                vSpeed = -2f + Rando.Float(-0.3f, 0.3f),
                depth = 1f,
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

using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATSLK8 : BaseAmmoType
    {
        public ATSLK8()
        {
            range = 600f;
            accuracy = 1f;
            penetration = 1f;
            bulletSpeed = 90f;
            bulletThickness = 1f;
            bulletLength = 50f;
            DamageMean = 56f;
            DamageVariation = 0.2f;
            AlphaDamage = 0.65f;
            DistanceConvexity = -0.8f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT762NATOShell(x, y)
            {
                hSpeed = Rando.Float(-0.2f, 0.2f) * dir,
                vSpeed = -1.5f + Rando.Float(-0.3f, 0.3f),
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

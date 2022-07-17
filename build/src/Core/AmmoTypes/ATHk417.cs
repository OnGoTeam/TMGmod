using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATHk417 : BaseAmmoType
    {
        public ATHk417()
        {
            range = 375f;
            accuracy = 0.93f;
            penetration = 2.1f;
            bulletSpeed = 34f;
            bulletThickness = 1f;
            bulletLength = 77f;
            DamageMean = 64f;
            DamageVariation = 0.2f;
            AlphaDamage = 0.67f;
            DistanceConvexity = -1f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT556NATOShell(x, y)
            {
                hSpeed = (2.5f + Rando.Float(-0.2f, 0.2f)) * dir,
                vSpeed = (2f + Rando.Float(-0.3f, 0.3f)) * dir,
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

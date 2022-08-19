using System;
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATFCAR : BaseAmmoType
    {
        public ATFCAR()
        {
            range = 530f;
            accuracy = 0.94f;
            penetration = 2.1f;
            bulletSpeed = 34f;
            bulletThickness = 1f;
            bulletLength = 50f;
            DamageMean = 52f;
            DamageVariation = 0.1f;
            AlphaDamage = 0.46f;
            DistanceConvexity = 0.2f;
        }

        public override void PopShell(float x, float y, int dir, Action<EjectedShell> add)
        {
            var shell = new AT556NATOShell(x, y)
            {
                hSpeed = (2.5f + Rando.Float(-0.2f, 0.2f)) * dir,
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

using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATMK17 : BaseAmmoType
    {
        public ATMK17()
        {
            accuracy = .84f;
            range = 275f;
            penetration = 2.1f;
            bulletSpeed = 35f;
            bulletThickness = .6f;
            bulletLength = 50f;
            DamageMean = 38f;
            DamageVariation = 0.18f;
            AlphaDamage = 0.67f;
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

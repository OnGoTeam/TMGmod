using System;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATLowQammos : BaseAmmoType
    {
        public ATLowQammos()
        {
            range = 400f;
            accuracy = 0.8f;
            penetration = 1.5f;
            bulletSpeed = 34f;
            bulletThickness = 1f;
            bulletLength = 50f;
            DamageMean = 35f;
            DamageVariation = 0.18f;
            AlphaDamage = 0.45f;
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

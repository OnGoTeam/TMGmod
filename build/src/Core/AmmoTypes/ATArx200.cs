using DuckGame;
using TMGmod.Core.Bullets;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATArx200 : BaseAmmoType
    {
        public ATArx200()
        {
            range = 450f;
            accuracy = 0.98f;
            penetration = 1.5f;
            bulletSpeed = 42f;
            deadly = true;
            bulletThickness = 1f;
            bulletLength = 50f;
            immediatelyDeadly = true;
            bulletType = typeof(Bullet556);
            BulletDamage = 47f;
            DeltaDamage = 0.1f;
            AlphaDamage = 0.69f;
            DistanceConvexity = -0.3f;
        }

        public override void PopShell(float x, float y, int dir)
        {
            var shell = new AT556NATOShell(x, y)
            {
                hSpeed = (2.5f + Rando.Float(-0.2f, 0.2f)) * dir,
                vSpeed = -2f + Rando.Float(-0.3f, 0.3f),
                depth = 1f
            };
            Level.Add(shell);
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

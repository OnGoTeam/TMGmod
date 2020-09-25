using DuckGame;
using TMGmod.Core.Bullets;
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
            deadly = true;
            bulletThickness = 1f;
            bulletLength = 77f;
            immediatelyDeadly = true;
            bulletType = typeof(Bullet556);
            BulletDamage = 64f;
            DeltaDamage = 0.2f;
            AlphaDamage = 0.67f;
            DistanceConvexity = -1f;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var shell = new AT556NATOShell(x, y)
            {
                hSpeed = (2.5f + Rando.Float(-0.2f, 0.2f)) * dir,
                vSpeed = (2f + Rando.Float(-0.3f, 0.3f)) * dir
            };
            Level.Add(shell);
        }

        public override void WriteAdditionalData(BitBuffer b)
        {
            b.Write(penetration);
        }

        public override void ReadAdditionalData(BitBuffer b)
        {
            penetration = b.ReadFloat();
        }
    }
}
using DuckGame;
using TMGmod.Core.Bullets;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class AT556NATO : AmmoType, IDamage
    {
        public AT556NATO()
        {
            penetration = 2.1f;
            bulletSpeed = 34f;
            deadly = true;
            bulletThickness = 1f;
            bulletLength = 50f;
            immediatelyDeadly = true;
            bulletType = typeof(Bullet556);
            Bulletdamage = 26f;
            Deltadamage = 0.18f;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var shell = new AT556NATOShell(x, y)
            {
                hSpeed = (2.5f + Rando.Float(-0.2f, 0.2f)) * dir,
                vSpeed = 2f + Rando.Float(-0.3f, 0.3f)
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
        public float Bulletdamage { get; }
        public float Deltadamage { get; }
    }
}
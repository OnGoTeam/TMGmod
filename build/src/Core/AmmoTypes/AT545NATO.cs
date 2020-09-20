using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class AT545NATO : AmmoType, IDamage
    {
        public AT545NATO()
        {
            penetration = 1f;
            bulletSpeed = 37f;
            deadly = true;
            bulletThickness = 1f;
            bulletLength = 50f;
            immediatelyDeadly = true;
            Bulletdamage = 22f;
            Deltadamage = 0.15f;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var shell = new AT545NATOShell(x, y)
            {
                hSpeed = (2.5f + Rando.Float(-0.2f, 0.2f)) * dir,
                vSpeed = 2f + Rando.Float(-0.3f, 0.3f)
            };
            Level.Add(shell);
        }
        public float Bulletdamage { get; }
        public float Deltadamage { get; }
    }
}
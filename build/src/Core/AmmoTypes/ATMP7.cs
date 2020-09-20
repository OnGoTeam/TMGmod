using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATMP7 : AmmoType, IDamage
    {
        public ATMP7()
        {
            range = 175f;
            accuracy = 0.9f;
            penetration = 0.4f;
            bulletSpeed = 18f;
            deadly = true;
            bulletThickness = 0.8f;
            bulletLength = 0f;
            immediatelyDeadly = true;
            Bulletdamage = 17f;
            Deltadamage = 0.5f;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var shell = new ATMP7Shell(x, y)
            {
                hSpeed = (3f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = -2.25f + Rando.Float(-0.4f, 0.4f)
            };
            Level.Add(shell);
        }
        public float Bulletdamage { get; }
        public float Deltadamage { get; }
    }
}
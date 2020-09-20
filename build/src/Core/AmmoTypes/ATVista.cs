using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATVista : AmmoType, IDamage
    {
        public ATVista()
        {
            penetration = 0.4f;
            bulletSpeed = 37f;
            deadly = true;
            bulletThickness = 0.8f;
            bulletLength = 6f;
            immediatelyDeadly = true;
            Bulletdamage = 10f;
            Deltadamage = 0.33f;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var shell = new AT762NATOShell(x, y) //AT9mmParabellumShell
            {
                hSpeed = (3f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = 2.25f + Rando.Float(-0.4f, 0.4f)
            };
            Level.Add(shell);
        }
        public float Bulletdamage { get; }
        public float Deltadamage { get; }
    }
}
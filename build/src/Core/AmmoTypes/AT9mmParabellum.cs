using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class AT9mmParabellum : AmmoType, IDamage
    {
        public AT9mmParabellum()
        {
            penetration = 0.6f;
            bulletSpeed = 24f;
            deadly = true;
            bulletThickness = 0.8f;
            bulletLength = 15f;
            immediatelyDeadly = true;
            Bulletdamage = 14f;
            Deltadamage = 0.4f;
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
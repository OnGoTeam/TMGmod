using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATUzi : BaseAmmoType
    {
        public ATUzi()
        {
            range = 70f;
            accuracy = 0.61f;
            penetration = 0.4f;
            bulletSpeed = 35f;
            deadly = true;
            bulletThickness = 0.8f;
            bulletLength = 0f;
            immediatelyDeadly = true;
            BulletDamage = 21f;
            DeltaDamage = 0.3f;
            AlphaDamage = 0.6f;
            DistanceConvexity = -1f;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var shell = new AT762NATOShell(x, y) //AT9mmParabellumShell
            {
                hSpeed = (3f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = -2.25f + Rando.Float(-0.4f, 0.4f)
            };
            Level.Add(shell);
        }
    }
}
using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATUziS : BaseAmmoType
    {
        public ATUziS()
        {
            range = 100f;
            accuracy = 0.8f;
            penetration = 0.4f;
            bulletSpeed = 32f;
            deadly = true;
            bulletThickness = 0.8f;
            bulletLength = 0f;
            immediatelyDeadly = true;
            BulletDamage = 19f;
            DeltaDamage = 0.4f;
            AlphaDamage = 0.7f;
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
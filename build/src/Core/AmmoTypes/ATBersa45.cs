using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATBersa45 : BaseAmmoType
    {
        public ATBersa45()
        {
            range = 90f;
            accuracy = 0.76f;
            penetration = 0.4f;
            bulletSpeed = 25f;
            deadly = true;
            bulletThickness = 2f;
            bulletLength = 64f;
            immediatelyDeadly = true;
            BulletDamage = 32f;
            DeltaDamage = 0.2f;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var shell = new AT762NATOShell(x, y)
            {
                hSpeed = (3f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = 2.25f + Rando.Float(-0.4f, 0.4f)
            };
            Level.Add(shell);
        }
    }
}
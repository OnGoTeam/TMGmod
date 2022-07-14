using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATAUGA1 : BaseAmmoType
    {
        public ATAUGA1()
        {
            range = 325f;
            accuracy = 0.85f;
            penetration = 1f;
            bulletSpeed = 37f;
            deadly = true;
            bulletThickness = 1f;
            bulletLength = 50f;
            immediatelyDeadly = true;
            BulletDamage = 22f;
            DeltaDamage = 0.15f;
            AlphaDamage = 0.4f;
        }

        public override void PopShell(float x, float y, int dir)
        {
            var shell = new AT556NATOShell(x, y)
            {
                hSpeed = (2.5f + Rando.Float(-0.2f, 0.2f)) * dir,
                vSpeed = 2f + Rando.Float(-0.3f, 0.3f),
            };
            Level.Add(shell);
        }
    }
}

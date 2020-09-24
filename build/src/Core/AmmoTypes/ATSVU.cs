using DuckGame;
using TMGmod.Core.Shells;

namespace TMGmod.Core.AmmoTypes
{
    // ReSharper disable once InconsistentNaming
    public class ATSVU : BaseAmmoType
    {
        public ATSVU()
        {
            range = 600f;
            accuracy = 0.95f;
            penetration = 0.5f;
            bulletSpeed = 32f;
            deadly = true;
            bulletThickness = 0.8f;
            bulletLength = 0f;
            immediatelyDeadly = true;
            BulletDamage = 49f;
            DeltaDamage = 0.1f;
        }
        public override void PopShell(float x, float y, int dir)
        {
            var shell = new AT556NATOShell(x, y) //AT762x54Shell
            {
                hSpeed = (1.9f + Rando.Float(-0.1f, 0.1f)) * dir,
                vSpeed = -1.5f + Rando.Float(-0.4f, 0.4f)
            };
            Level.Add(shell);
        }
    }
}
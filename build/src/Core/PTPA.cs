using DuckGame;
using TMGmod.Core.AmmoTypes;

namespace TMGmod.Core
{
    // ReSharper disable once InconsistentNaming
    public class PTPA : BaseAmmoType
    {
        public PTPA()
        {
            accuracy = 0.9f;
            range = 500f;
            penetration = 5f;
            combustable = true;
            bulletSpeed = 30f;
        }

        public override void PopShell(float x, float y, int dir)
        {
            Level.Add(new PistolShell(x, y)
            {
                hSpeed = dir * (1.5f + Rando.Float(1f))
            });
        }
    }
}

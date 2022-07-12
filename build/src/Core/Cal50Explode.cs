using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Shells;

namespace TMGmod.Core
{
    public class Cal50Explode : AmmoType, IHeavyAmmoType
    {
        public Cal50Explode()
        {
            accuracy = 0.9f;
            range = 700f;
            penetration = 0f;
            combustable = true;
            bulletSpeed = 55f;
            bulletType = typeof(ExplosiveBullet);
        }
        public override void PopShell(float x, float y, int dir)
        {
            var redpill = new M50Shell(x, y) { hSpeed = dir * (6f + Rando.Float(1f)) };
            Level.Add(redpill);
        }
    }
}

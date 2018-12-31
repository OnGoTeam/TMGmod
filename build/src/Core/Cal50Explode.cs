using DuckGame;

namespace TMGmod.Core
{

    public class Cal50Explode : AmmoType
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
    }
}

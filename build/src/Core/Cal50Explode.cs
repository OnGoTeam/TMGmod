using DuckGame;

namespace TMGmod.Core
{

    /// <summary>
    /// <see cref="AmmoType"/> with <see cref="ExplosiveBullet"/>
    /// </summary>
    public class Cal50Explode : AmmoType
    {
        /// <inheritdoc />
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

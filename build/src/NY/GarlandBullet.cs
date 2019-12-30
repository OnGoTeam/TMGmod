using DuckGame;

namespace TMGmod.NY
{
    /// <inheritdoc />
    public class GarlandBullet:Bullet
    {
        /// <inheritdoc />
        public GarlandBullet(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            _tracer = false;
            _bulletLength = 0f;
        }
    }
}

namespace TMGmod.NY
{
    // ReSharper disable once InconsistentNaming
    /// <inheritdoc />
    public class AT9mmParasha : AmmoType
    {
        /// <summary>
        /// bullet sprite
        /// </summary>
        public readonly SpriteMap SpriteY;


        /// <inheritdoc />
        public AT9mmParasha()
        {
            range = 1000f;
            accuracy = 1f;
            bulletSpeed = 5.5f;
            penetration = 0.4f;
            speedVariation = 0f;
            bulletType = typeof(GarlandBullet);
            combustable = true;
            SpriteY = new SpriteMap(Mod.GetPath<Core.TMGmod>("Holiday/Garland_2"), 16, 9);
            SpriteY.CenterOrigin();
            sprite = SpriteY;
        }
    }
}
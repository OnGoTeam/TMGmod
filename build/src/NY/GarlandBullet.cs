using DuckGame;
using TMGmod.NY;

namespace TMGmod.NY
{
    public class GarlandBullet:Bullet
    {
        public GarlandBullet(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            _tracer = false;
            _bulletLength = 0f;
        }
    }
}

namespace TMGmod.Core
{
    // ReSharper disable once InconsistentNaming
    public class AT9mmParasha : AmmoType
    {
        public AT9mmParasha()
        {
            bulletType = typeof(GarlandBullet);
            combustable = true;
        }
    }
}
using System;
using DuckGame;

namespace TMGmod.NY
{
    public class CandyCaneBullet:Bullet
    {
        
        public CandyCaneBullet(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            _tracer = false;
            _center = new Vec2(9f, 3.5f);
            _collisionOffset = new Vec2(-9f, -3.5f);
            _collisionSize = new Vec2(18f, 7f);
        }

        public override void Initialize()
        {
            angle = offDir > 0 ? angle : (float) Math.PI + angle;
        }

        public override void Terminate()
        {
            if (firedFrom is CandyCane c)
            {
                c.Drop(x, y, true);
            }
        }
    }
}
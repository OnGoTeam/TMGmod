using DuckGame;

namespace TMGmod.NY
{
    public class CandyCaneBullet : Bullet
    {
        public CandyCaneBullet(
            float xval, float yval, AmmoType type, float ang = -1, Thing owner = null,
            bool rbound = false, float distance = -1, bool tracer = false, bool network = true
        ) :
            base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            _tracer = false;
            _center = new Vec2(9f, 3.5f);
            _collisionOffset = new Vec2(-9f, -3.5f);
            _collisionSize = new Vec2(18f, 7f);
        }

        public override void Update()
        {
            base.Update();
            if (!(firedFrom is CandyCaneOrange)) return;
            //else
            var v = travelDirNormalized * bulletSpeed;
            v *= 0.3f;
            Level.Add(SmallFire.New(end.x, end.y, v.x, v.y, firedFrom: this));
        }

        public override void Terminate()
        {
            if (firedFrom is CandyCane c) c.Drop(end, true);
        }
    }
}

using DuckGame;

namespace TMGmod.Core.Bullets
{
    public class BulletStoppedByProps : Bullet
    {
        public BulletStoppedByProps(
            float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false,
            float distance = -1, bool tracer = false, bool network = true
        ) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
        }

        public override void OnCollide(Vec2 pos, Thing t, bool willBeStopped)
        {
            if (!(t is IPlatform) || !((t as Holdable)?.thickness > 1.1f)) return;
            doneTravelling = true;
            alpha = -1f;
            Level.Remove(this);
            OnHit(true);
        }
    }
}

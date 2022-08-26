using DuckGame;

namespace TMGmod.Core.Bullets
{
    public class AntiProp : BaseBullet
    {
        public AntiProp(
            float xval, float yval, AmmoType type, float ang = -1f, Thing owner = null, bool rbound = false,
            float distance = -1f, bool tracer = false, bool network = true
        )
            : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
        }

        public override void OnCollide(Vec2 pos, Thing t, bool willBeStopped)
        {
            base.OnCollide(pos, t, willBeStopped);
            if (t == owner) return;
            //else
            switch (t)
            {
                case MaterialThing thing and IPlatform:
                    thing._hitPoints = 1;
                    thing.Hit(this, pos);
                    break;
                case MaterialThing { _hitPoints: > 0f } thing:
                    thing._hitPoints -= 8;
                    break;
            }
        }
    }
}

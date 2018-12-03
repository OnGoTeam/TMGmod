using DuckGame;

namespace TMGmod.Core
{
    public class AntiProp : Bullet
    {
        public AntiProp(float xval, float yval, AmmoType type, float ang = -1f, Thing owner = null, bool rbound = false, float distance = -1f, bool tracer = false, bool network = true)
            : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
        }

        public override void OnCollide(Vec2 pos, Thing t, bool willBeStopped)
        {
            base.OnCollide(pos, t, willBeStopped);
            if (t != owner)
            {
                if (t is MaterialThing && t is IPlatform)
                {
                    ((MaterialThing) t)._hitPoints = 1;
                    ((MaterialThing) t).Hit(this, pos);
                }
                else if (t is MaterialThing && ((MaterialThing) t)._hitPoints > 0f)
                {
                    ((MaterialThing) t)._hitPoints -= 8;
                }
            }
        }
    }
    public class AT50C : AmmoType
    {
        public AT50C()
        {
            accuracy = 1f;
            range = 2500f;
            penetration = 1f;
            combustable = true;
            bulletSpeed = 45f;
            bulletType = typeof(AntiProp);
        }
    }
}
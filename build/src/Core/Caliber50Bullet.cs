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
            if (t == owner) return;
            //else
            switch (t)
            {
                case MaterialThing thing when thing is IPlatform:
                    thing._hitPoints = 1;
                    thing.Hit(this, pos);
                    break;
                case MaterialThing thing when thing._hitPoints > 0f:
                    thing._hitPoints -= 8;
                    break;
            }
        }
    }
    // ReSharper disable once InconsistentNaming
    public class AT50C : AmmoType
    {
        public AT50C()
        {
            accuracy = 1f;
            range = 120f;
            penetration = 2f;
            combustable = true;
            bulletSpeed = 45f;
            bulletType = typeof(AntiProp);
            bulletThickness = 3.5f;
        }
    }
}
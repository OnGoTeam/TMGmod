using DuckGame;

namespace TMGmod.src
{
    public class explode : Bullet
    {
        public explode(float xval, float yval, AmmoType type, float ang = -1f, Thing owner = null, bool rbound = false, float distance = -1f, bool tracer = false, bool network = false)
            : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            this._tracer = false;
        }
        protected override void OnHit(bool destroyed)
        {
            if (destroyed)
            {
                ExplosionPart ins = new ExplosionPart(base.x, base.y, true);
                ins.xscale *= 0.7f;
                ins.yscale *= 0.7f;
                Level.Add(ins);
                SFX.Play("magPop", 0.7f, Rando.Float(-0.5f, -0.3f), 0f, false);
                //Thing bulletOwner = this.owner;
                System.Collections.Generic.IEnumerable<MaterialThing> things = Level.CheckCircleAll<MaterialThing>(this.position, 30f);
                foreach (MaterialThing t in things)
                {
                    t.Destroy(new DTShot(this));
                }
            }
        }

        protected override void Rebound(Vec2 pos, float dir, float rng)
        {
            explode bullet = new explode(pos.x, pos.y, this.ammo, dir, null, this.rebound, rng, false, false);
            bullet._teleporter = this._teleporter;
            bullet.firedFrom = base.firedFrom;
            Level.current.AddThing(bullet);
            Level.current.AddThing(new LaserRebound(pos.x, pos.y));
        }
    }
}

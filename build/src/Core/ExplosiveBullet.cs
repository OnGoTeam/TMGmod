using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core
{
    /// <summary>
    /// <see cref="Bullet"/> which explodes on impact
    /// </summary>
    [PublicAPI]
    public class ExplosiveBullet : Bullet
    {
        /// <inheritdoc />
        public ExplosiveBullet(float xval, float yval, AmmoType type, float ang = -1f, Thing owner = null, bool rbound = false, float distance = -1f, bool tracer = false, bool network = false)
            : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
            _tracer = false;
        }

        /// <inheritdoc />
        protected override void OnHit(bool destroyed)
        {
            if (!destroyed) return;
            var ins = new ExplosionPart(x, y);
            ins.xscale *= 0.7f;
            ins.yscale *= 0.7f;
            Level.Add(ins);
            SFX.Play("magPop", 0.7f, Rando.Float(-0.5f, -0.3f));
            //Thing bulletOwner = this.owner;
            var things = Level.CheckCircleAll<MaterialThing>(position, 30f);
            foreach (var t in things)
            {
                t.Destroy(new DTShot(this));
            }
        }

        /// <inheritdoc />
        protected override void Rebound(Vec2 pos, float dir, float rng)
        {
            var bullet = new ExplosiveBullet(pos.x, pos.y, ammo, dir, null, rebound, rng)
            {
                _teleporter = _teleporter,
                firedFrom = firedFrom
            };
            Level.current.AddThing(bullet);
            Level.current.AddThing(new LaserRebound(pos.x, pos.y));
        }
    }
}

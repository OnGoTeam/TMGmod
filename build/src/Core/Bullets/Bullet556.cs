using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Core.Bullets
{
    public class Bullet556 : Bullet
    {
        [UsedImplicitly]
        public StateBinding TracerBinding = new StateBinding(nameof(Tracer));

        [UsedImplicitly]
        public bool Tracer
        {
            get => _tracer;
            set => _tracer = value;
        }

        public Bullet556(float xval, float yval, AmmoType type, float ang = -1, Thing owner = null, bool rbound = false, float distance = -1, bool tracer = false, bool network = true) : base(xval, yval, type, ang, owner, rbound, distance, tracer, network)
        {
        }

        public override void OnCollide(Vec2 pos, Thing t, bool willBeStopped)
        {
            if (t is IPlatform && (t as Holdable)?.thickness > 1.1f) _tracer = true;
            base.OnCollide(pos, t, willBeStopped);
        }
    }
}
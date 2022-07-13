using DuckGame;

namespace TMGmod.Core.Particles
{
    public class BarrBetaPar : PhysicsObject
    {
        private byte _tick;

        public BarrBetaPar(float xval, float yval) : base(xval, yval)
        {
            thickness = 2f;
            physicsMaterial = PhysicsMaterial.Wood;
            _center = new Vec2(1f, 2f);
            _collisionOffset = new Vec2(-1f, -2f);
            _collisionSize = new Vec2(2f, 4f);
            _graphic = new Sprite(GetPath("barr"));
            flammable = 0.6f;
            _weight = 1f;
        }

        public override void Update()
        {
            _tick += 1;
            if (_tick >= 32)
            {
                sleeping = false;
                _tick = 0;
            }

            base.Update();
        }
    }
}

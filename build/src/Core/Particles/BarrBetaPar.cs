using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Core.Particles
{
    public class BarrBetaPar : PhysicsObject
    {
        private byte _tick;

        public BarrBetaPar(float xval, float yval) : base(xval, yval)
        {
            thickness = 2f;
            physicsMaterial = PhysicsMaterial.Wood;
            center = new Vec2(1f, 2f);
            collisionOffset = new Vec2(-1f, -2f);
            collisionSize = new Vec2(2f, 4f);
            graphic = new Sprite(GetPath("barr"));
            flammable = 0.6f;
            weight = 1f;
        }

        public override void Update()
        {
            _tick += 1;
            if (_tick == 32)
            {
                sleeping = false;
                _tick = 0;
            }
            base.Update();
        }
    }
}
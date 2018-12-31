using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Core.Particles
{
    public class BarrBetaPar : PhysicsObject
    {
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
    }
}
using DuckGame;

namespace TMGmod.src
{
    public class Czmag : PhysicsObject
    {

        public Czmag(float xpos, float ypos)
            : base(xpos, ypos)
        {
            _dontCrush = true;
            hSpeed = 0f;
            vSpeed = 0f;
            graphic = new Sprite(GetPath("CZ75Magamed"));
            center = new Vec2(1.5f, 2f);
            depth = 0.3f + Rando.Float(0f, 0.1f);
            _collisionSize = new Vec2(3f, 4f);
            collisionOffset = new Vec2(-1.5f, -2f);
            weight = 1f;
        }
    }
}
using DuckGame;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    public class Czmag : PhysicsObject
    {

        public Czmag(float xpos, float ypos)
            : base(xpos, ypos)
        {
            _dontCrush = true;
            _hSpeed = 0f;
            _vSpeed = 0f;
            _graphic = new Sprite(GetPath("CZ75Magamed"));
            _center = new Vec2(1.5f, 2f);
            depth = 0.3f + Rando.Float(0f, 0.1f);
            _collisionSize = new Vec2(3f, 4f);
            _collisionOffset = new Vec2(-1.5f, -2f);
            _weight = 1f;
        }
    }
}
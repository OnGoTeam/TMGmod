using DuckGame;

namespace TMGmod.Core
{
    public class ArwaMag : PhysicsParticle
    {
        public ArwaMag(float xpos, float ypos) : base(xpos, ypos)
        {
            _hSpeed = 0f;
            _vSpeed = 0f;
            _graphic = new Sprite(GetPath("armag.png"));
            depth = 0.3f + Rando.Float(0f, 0.1f);
            _collisionSize = new Vec2(4, 7);
            _collisionOffset = new Vec2(-2f, -3.5f);
            _center = new Vec2(2f, 3.5f);
        }
    }
}

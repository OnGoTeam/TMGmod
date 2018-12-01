using DuckGame;

namespace TMGmod.src
{
    public class Czmag : PhysicsObject
    {

        public Czmag(float xpos, float ypos)
            : base(xpos, ypos)
        {
            base._dontCrush = true;
            this.hSpeed = 0f;
            this.vSpeed = 0f;
            this.graphic = new Sprite(GetPath("CZ75Magamed"));
            this.center = new Vec2(1.5f, 2f);
            base.depth = 0.3f + Rando.Float(0f, 0.1f);
            this._collisionSize = new Vec2(3f, 4f);
            this.collisionOffset = new Vec2(-1.5f, -2f);
            this.weight = 1f;
        }
    }
}
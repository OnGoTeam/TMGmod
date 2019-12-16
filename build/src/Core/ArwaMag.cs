using DuckGame;

namespace TMGmod.Core
{
    public class ArwaMag : PhysicsObject
    {
        public ArwaMag()
        {
            _graphic = new Sprite(GetPath("armag.png"));
            _collisionSize = new Vec2(4, 7);
            physicsMaterial = PhysicsMaterial.Plastic;
        }
    }
}
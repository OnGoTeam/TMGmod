using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Stuff
{
    [EditorGroup("TMG|Misc")]
    [UsedImplicitly]
    public class InvisiblePlatform: MaterialThing, IPlatform
    {
        public InvisiblePlatform(float x, float y) : base(x, y)
        {
            _graphic = new SpriteMap("greyBlock", 16, 4);
            _editorName = "InvPlt";
            physicsMaterial = PhysicsMaterial.Metal;
            _collisionSize = new Vec2(16f, 2f);
            thickness = 0.2f;
            _center = new Vec2(8, 8);
            _collisionOffset = new Vec2(-8f, -8f);
            depth = 0.3f;
        }

        public override void Draw()
        {
            if (!(Level.activeLevel is Editor)) return;
            base.Draw();
        }
    }
}
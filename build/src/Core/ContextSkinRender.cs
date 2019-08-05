using DuckGame;

namespace TMGmod.Core
{
    public class ContextSkinRender : ContextMenu
    {
        private SpriteMap _image;
        public ContextSkinRender(IContextListener owner, SpriteMap img, bool hasToproot = false, Vec2 topRoot = default) : base(owner, null, hasToproot, topRoot)
        {
            _image = img;
        }

        public override void Draw()
        {
            _image.depth = depth + 3;
            _image.x = x + (float) _image.width / 2;
            _image.y = y + (float) _image.height / 2;
            _image.color = Color.White;
            _image.Draw();
            base.Draw();
        }
    }
}

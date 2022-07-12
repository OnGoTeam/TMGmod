#if DEBUG
using DuckGame;

namespace TMGmod.Core
{
    public class ContextSkinRender : ContextMenu
    {
        private readonly SpriteMap _imag;
        public ContextSkinRender(IContextListener owner, SpriteMap img, bool hasToproot = false, Vec2 topRoot = default) : base(owner, null, hasToproot, topRoot)
        {
            _imag = img;
        }

        public override void Draw()
        {
            _imag.depth = depth + 3;
            _imag.x = x + (float) _imag.width / 2;
            _imag.y = y + (float) _imag.height / 2;
            _imag.color = Color.White;
            _imag.Draw();
            base.Draw();
        }
    }
}
#endif
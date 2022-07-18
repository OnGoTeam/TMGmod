#if DEBUG
using System;
using DuckGame;

namespace TMGmod.Core.SkinLogic
{
    public class ContextSkinRender : ContextMenu
    {
        private readonly EditorProperty<int> _target;
        private readonly int _skin;
        private readonly SpriteMap _imag;

        public ContextSkinRender(
            EditorProperty<int> target, int skin, Func<int, SpriteMap> img
        ) : base(null)
        {
            _target = target;
            _skin = skin;
            _imag = img(skin);
            _imag.CenterOrigin();
        }

        public override void Draw()
        {
            if (_hover)
                Graphics.DrawRect(position, position + itemSize, new Color(70, 70, 70), depth + 1);
            else if (_target.value == _skin)
                Graphics.DrawRect(position, position + itemSize, new Color(60, 60, 60), depth + 1);
            _imag.depth = depth + 3;
            _imag.x = x + itemSize.x / 2f;
            _imag.y = y + itemSize.y / 2f;
            _imag.color = Color.White;
            _imag.Draw();
            base.Draw();
        }

        public override void Selected() => _target.value = _skin;
    }
}
#endif

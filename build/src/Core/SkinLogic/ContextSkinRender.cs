#if DEBUG
using DuckGame;

namespace TMGmod.Core.SkinLogic
{
    public class ContextSkinRender : ContextMenu
    {
        private readonly IShowSkins _target;
        private readonly int _skin;
        private readonly SpriteMap _imag;

        public ContextSkinRender(
            IShowSkins target, int skin
        ) : base(null)
        {
            _target = target;
            _skin = skin;
            if (!target.AllowedSkins.Contains(skin)) return;
            // else
            _imag = _target.ShowedSkin(skin);
            _imag.CenterOrigin();
        }

        public override void Draw()
        {
            if (_hover)
                Graphics.DrawRect(position, position + itemSize, new Color(70, 70, 70), depth + 1);
            else if (_target.Skin.value == _skin)
                Graphics.DrawRect(position, position + itemSize, new Color(60, 60, 60), depth + 1);
            if (_imag != null)
            {
                _imag.depth = depth + 3;
                _imag.x = x + itemSize.x / 2f;
                _imag.y = y + itemSize.y / 2f;
                _imag.color = Color.White;
                _imag.Draw();
            }
            else
            {
                foreach (var skin in _target.AllowedSkins)
                {
                    var sprite = _target.ShowedSkin(skin);
                    sprite.CenterOrigin();
                    sprite.depth = depth + 3;
                    sprite.x = x + itemSize.x / 2f;
                    sprite.y = y + itemSize.y / 2f;
                    sprite.color = Color.White;
                    sprite.Draw(new Rectangle(0f, 0f, sprite.width, sprite.height));
                }
            }
            base.Draw();
        }

        public override void Selected() => _target.Skin.value = _skin;
    }
}
#endif

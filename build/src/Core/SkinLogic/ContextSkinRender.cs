#if DEBUG
using System.Collections.Generic;
using System.Linq;
using DuckGame;

namespace TMGmod.Core.SkinLogic
{
    public class ContextSkinRender : ContextMenu
    {
        private readonly IShowSkins _target;
        private readonly int _skin;
        private readonly SpriteMap _imag;
        private readonly IEnumerable<SpriteMap> _imags;
        private float _drift;

        public ContextSkinRender(
            IShowSkins target, int skin
        ) : base(null)
        {
            _target = target;
            _skin = skin;
            if (target.AllowedSkins.Contains(skin))
            {
                _imag = _target.ShowedSkin(skin);
                _imag.CenterOrigin();
            }
            else
            {
                _imags = _target.AllowedSkins.Select(allowed => _target.ShowedSkin(allowed));
            }
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
            else if (_imags != null)
            {
                var total = _target.AllowedSkins.Count;
                var current = 0;
                foreach (var sprite in _imags)
                {
                    sprite._imageIndex = sprite._frame;
                    sprite.CenterOrigin();
                    sprite.depth = depth + 3;
                    sprite.y = y + itemSize.y / 2f;
                    sprite.color = Color.White;
                    var step = sprite.width / (float)total;
                    var xoffset = current * step - _drift * sprite.width;
                    if (xoffset >= 0)
                    {
                        sprite.x = x + itemSize.x / 2f + xoffset;
                        sprite.Draw(new Rectangle(xoffset, 0f, step, sprite.height));
                    }
                    else if (xoffset <= -step)
                    {
                        sprite.x = x + itemSize.x / 2f + xoffset + sprite.width;
                        sprite.Draw(new Rectangle(xoffset + sprite.width, 0f, step, sprite.height));
                    }
                    else
                    {
                        sprite.x = x + itemSize.x / 2f + xoffset + sprite.width;
                        sprite.Draw(new Rectangle(xoffset + sprite.width, 0f, -xoffset, sprite.height));
                        sprite.x = x + itemSize.x / 2f;
                        sprite.Draw(new Rectangle(0f, 0f, step+xoffset, sprite.height));
                    }
                    current += 1;
                }

                _drift += .01f;
                _drift %= 1f;
            }

            base.Draw();
        }

        public override void Selected() => _target.Skin.value = _skin;
    }
}
#endif

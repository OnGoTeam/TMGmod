#if DEBUG
using System;
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
        private static readonly Dictionary<Tuple<int, int>, Tex2D> Tex = new Dictionary<Tuple<int, int>, Tex2D>();
        private static readonly Dictionary<int, Color[]> TexData = new Dictionary<int, Color[]>();
        private static readonly Dictionary<Tex2D, Color[]> Data = new Dictionary<Tex2D, Color[]>();
        private static readonly Dictionary<Tex2D, Tuple<Tex2D, int>> Rendered = new Dictionary<Tex2D, Tuple<Tex2D, int>>();

        public ContextSkinRender(
            IShowSkins target, int skin
        ) : base(null)
        {
            _target = target;
            _skin = skin;
            if (!target.AllowedSkins.Contains(skin)) return;
            // else
            _imag = _target.SpriteBase;
        }

        private static Color[] GetData(Tex2D tex)
        {
            if (!Data.ContainsKey(tex))
                Data[tex] = tex.GetData();
            return Data[tex];
        }

        private static Tex2D GetTex(int width, int height)
        {
            var tuple = new Tuple<int, int>(width, height);
            if (!Tex.ContainsKey(tuple))
                Tex[tuple] = new Tex2D(width, height);
            return Tex[tuple];
        }

        private static Color[] GetData(int size)
        {
            if (!TexData.ContainsKey(size))
                TexData[size] = new Color[size];
            return TexData[size];
        }

        private static int SkinNo(IReadOnlyList<int> skins, float x, int time)
        {
            var total = skins.Count;
            return skins[(int) Math.Floor((x + time * .0051379f) * total) % total];
        }

        private static void PutData(IReadOnlyList<int> skins, IList<Color> data, Sprite sprite, int time)
        {
            var spriteData = GetData(sprite.texture);
            for (var y = 0; y < sprite.height; y++)
            {
                for (var x = 0; x < sprite.width; x++)
                {
                    var frame = SkinNo(skins, (float) x / sprite.width, time);
                    var columns = sprite.texture.width / sprite.width;
                    var rowNo = frame / columns;
                    var colNo = frame % columns;
                    data[y * sprite.width + x] = spriteData[
                        colNo * sprite.width + (rowNo * sprite.height + y) * sprite.texture.width + x
                    ];
                }
            }
        }

        private static Color[] GetData(IReadOnlyList<int> skins, Sprite sprite, int time)
        {
            var data = GetData(sprite.width * sprite.height);
            PutData(skins, data, sprite, time);
            return data;
        }

        private static Tex2D GetTex(IReadOnlyList<int> skins, Sprite sprite, int time)
        {
            var tex = GetTex(sprite.width, sprite.height);
            tex.SetData(GetData(skins, sprite, time));
            return tex;
        }

        private static Sprite GetSprite(IShowSkins target)
        {
            return new Sprite(GetTex(target.AllowedSkins.ToArray(), target.SpriteBase, MonoMain.timeInEditor));
        }
        public static void WithRandomized(
            IShowSkins target, Action<Sprite> action
        )
        {
            action(GetSprite(target));
        }

        private void DrawRectangles()
        {
            if (_hover)
                Graphics.DrawRect(position, position + itemSize, new Color(70, 70, 70), depth + 1);
            else if (_target.Skin.value == _skin)
                Graphics.DrawRect(position, position + itemSize, new Color(60, 60, 60), depth + 1);
        }

        private void DrawStatic(Sprite sprite)
        {
            sprite.CenterOrigin();
            sprite.depth = depth + 3;
            sprite.x = x + itemSize.x / 2f;
            sprite.y = y + itemSize.y / 2f;
            sprite.color = Color.White;
            sprite.Draw();
        }

        private void DrawSkin()
        {
            if (_imag is null)
                WithRandomized(_target, DrawStatic);
            else
            {
                var old = _imag._frame;
                _imag._frame = _skin;
                DrawStatic(_imag);
                _imag._frame = old;
            }
        }

        public override void Draw()
        {
            base.Draw();
            DrawRectangles();
            DrawSkin();
        }

        public override void Selected() => _target.Skin.value = _skin;
    }
}
#endif

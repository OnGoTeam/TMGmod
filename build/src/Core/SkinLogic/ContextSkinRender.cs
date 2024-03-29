﻿using System;
using System.Collections.Generic;
using System.Linq;
using DuckGame;

namespace TMGmod.Core.SkinLogic
{
    public class ContextSkinRender : ContextMenu
    {
        private static readonly Dictionary<Tex2D, Color[]> Data = new();

        private static readonly Dictionary<Tex2D, Tuple<Tex2D, Color[], int>> Rendered = new();

        private readonly SpriteMap _imag;
        private readonly int _skin;
        private readonly IShowSkins _target;

        public ContextSkinRender(
            IShowSkins target, int skin
        ) : base(null)
        {
            _target = target;
            _skin = skin;
            itemSize.y = 4f + target.SpriteBase.height;
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
            return new Tex2D(width, height);
        }

        private static Color[] GetData(int size)
        {
            return new Color[size];
        }

        private class SkinContext
        {
            private readonly float _x;
            private readonly float _y;
            private readonly Sprite _sprite;
            private readonly int _time;
            private readonly int _total;
            private static readonly float Mode = Rando.Float(0f, 2.1f);
            public SkinContext(float x, float y, Sprite sprite, int time, int total)
            {
                _x = x;
                _y = y;
                _sprite = sprite;
                _time = time;
                _total = total;
            }

            private float Off()
            {
                return (_x + _y / 3f) / _sprite.width;
            }
            private float FloatNo()
            {
                return Mode switch
                {
                    < 1f => _x / _sprite.width / 2 + _time * .0151379f,
                    < 2f => (Off() + _time * .00951379f) * _total,
                    _ => Rando.Int(0, _total - 1),
                };
            }

            public int SkinNo()
            {
                return (int)Math.Floor(FloatNo());
            }
        }

        private static int SkinNo(IReadOnlyList<int> skins, int skin)
        {
            var total = skins.Count;
            return skins[skin.Modulo(total)];
        }

        private static void PutData(IReadOnlyList<int> skins, IList<Color> data, Sprite sprite, int time)
        {
            var spriteData = GetData(sprite.texture);
            for (var y = 0; y < sprite.height; y++)
            for (var x = 0; x < sprite.width; x++)
            {
                var frame = SkinNo(skins, new SkinContext(x, y, sprite, time, skins.Count).SkinNo());
                var columns = sprite.texture.width / sprite.width;
                var rowNo = frame / columns;
                var colNo = frame % columns;
                data[y * sprite.width + x] = spriteData[
                    colNo * sprite.width + (rowNo * sprite.height + y) * sprite.texture.width + x
                ];
            }
        }

        private static Color[] GetData(IReadOnlyList<int> skins, Sprite sprite, int time)
        {
            var data = GetData(sprite.width * sprite.height);
            PutData(skins, data, sprite, time);
            return data;
        }

        private static void UpdateContained(IReadOnlyList<int> skins, Sprite sprite, int time)
        {
            var tuple = Rendered[sprite.texture];
            if (tuple.Item3 == time) return;
            // else
            PutData(skins, tuple.Item2, sprite, time);
            tuple.Item1.SetData(GetData(skins, sprite, time));
            Rendered[sprite.texture] = new Tuple<Tex2D, Color[], int>(tuple.Item1, tuple.Item2, time);
        }

        private static void CreateContained(IReadOnlyList<int> skins, Sprite sprite, int time)
        {
            var tex = GetTex(sprite.width, sprite.height);
            var data = GetData(sprite.width * sprite.height);
            PutData(skins, data, sprite, time);
            tex.SetData(GetData(skins, sprite, time));
            Rendered[sprite.texture] = new Tuple<Tex2D, Color[], int>(tex, data, time);
        }

        private static Tex2D GetTex(IReadOnlyList<int> skins, Sprite sprite, int time)
        {
            if (Rendered.ContainsKey(sprite.texture))
                UpdateContained(skins, sprite, time);
            else
                CreateContained(skins, sprite, time);
            return Rendered[sprite.texture].Item1;
        }

        private static int Time()
        {
            return MonoMain.timeInEditor;
        }

        private static Sprite GetSprite(IShowSkins target)
        {
            return new Sprite(GetTex(target.AllowedSkins.ToArray(), target.SpriteBase, Time()));
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
            else if (_target.SkinValue == _skin)
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

        private void DrawImag()
        {
            var oldIndex = _imag._imageIndex;
            var oldFrame = _imag._frame;
            var oldFlip = _imag.flipH;
            _imag._imageIndex = _imag._frame = _skin;
            _imag.flipH = false;
            DrawStatic(_imag);
            _imag._frame = oldFrame;
            _imag._imageIndex = oldIndex;
            _imag.flipH = oldFlip;
        }

        private void DrawSkin()
        {
            if (_imag is null)
                WithRandomized(_target, DrawStatic);
            else
                DrawImag();
        }

        public override void Draw()
        {
            base.Draw();
            DrawRectangles();
            DrawSkin();
        }

        public override void Selected()
        {
            _target.SkinValue = _skin;
        }
    }
}

using System;
using DuckGame;

namespace TMGmod.Core.SkinLogic
{
    public class ContextDetailsRender : ContextMenu
    {
        private readonly IShowSkins _target;
        private readonly string _details;
        private readonly Sprite _imag;

        private float DetailsWidth()
        {
            return Graphics.GetStringWidth(_details);
        }

        private float DetailsHeight()
        {
            return Graphics.GetStringHeight(_details);
        }

        public ContextDetailsRender(
            IShowSkins target, string details
        ) : base(null)
        {
            _target = target;
            _details = details;
            itemSize.y = 4f + Math.Max(target.SpriteBase.height, DetailsHeight());
            itemSize.x = 6f + target.SpriteBase.width + DetailsWidth();
            if (target.AllowedSkins.Contains(target.SkinValue))
                _imag = target.SpriteBase;
        }

        public ContextDetailsRender(
            Thing target, string details
        ) : base(null)
        {
            _details = details;
            itemSize.y = 4f + Math.Max(target.graphic.height, DetailsHeight());
            itemSize.x = 6f + target.graphic.width + DetailsWidth();
            _imag = target.graphic;
        }

        private void DrawStatic(Sprite sprite)
        {
            sprite.CenterOrigin();
            sprite.depth = depth + 3;
            sprite.x = x + (itemSize.x - 2f - DetailsWidth()) / 2f;
            sprite.y = y + itemSize.y / 2f;
            sprite.color = Color.White;
            sprite.Draw();
        }

        private void DrawImag()
        {
            var oldFlip = _imag.flipH;
            _imag.flipH = false;
            DrawStatic(_imag);
            _imag.flipH = oldFlip;
        }

        private void DrawRandomized()
        {
            if (_target != null)
                ContextSkinRender.WithRandomized(_target, DrawStatic);
        }

        private void DrawSkin()
        {
            if (_imag is null)
                DrawRandomized();
            else
                DrawImag();
        }

        private void DrawText()
        {
            Graphics.DrawString(
                _details,
                new Vec2(x + itemSize.x - 2f - DetailsWidth(), y + (itemSize.y - DetailsHeight()) / 2),
                Color.White,
                depth:depth + 3
            );
        }

        public override void Draw()
        {
            base.Draw();
            DrawSkin();
            DrawText();
        }
    }
}

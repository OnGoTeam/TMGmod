#if DEBUG
using System;
using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Stuff
{
    [EditorGroup("TMG|DEBUG")]
    [BaggedProperty("CanSpawn", false)]
    [UsedImplicitly]
    public class Logo : Thing
    {
        private readonly ContextCheckBox _checkBox;

        public Logo(float x, float y) : base(x, y)
        {
            _collisionSize = new Vec2(48f, 36f);
            _center = _collisionSize / 2;
            _collisionOffset = -_center;
            _depth = -1f;
            _checkBox = new ContextCheckBox("square", null);
        }


        private bool Square => _checkBox.isChecked;

        public override void Draw()
        {
            _collisionSize = Square switch
            {
                true => new Vec2(48f, 48f),
                false => new Vec2(64f, 36f),
            };

            _center = _collisionSize / 2;
            _collisionOffset = -_center;

            Graphics.DrawRect(rectangle, Color.Black, depth: depth);
            {
                var gun = new DragoShot(x, y)
                {
                    SkinValue = -1,
                    position = position,
                };
                gun.center = gun.collisionOffset = gun.collisionSize / 2;
                gun.y += 8f;
                if (Square) gun.y += 4f;
                if (!Square) gun.x -= 14f;
                gun.angle = -(float)Math.Atan2(1, 3);
                gun.Draw();
            }
            {
                const string str = "TMG";
                const float strscale = 1.5f;
                var strw = Graphics.GetStringWidth(str, scale: strscale) - 1;
                var strpos = position + new Vec2(-strw / 2, -13f);
                if (Square) strpos.y -= 4f;
                Graphics.DrawStringOutline(str, strpos, Color.Black, Color.White, scale: strscale);
            }
            {
                const string str = "1.2";
                var strscale = Square ? 1f : 2f;
                var strw = Graphics.GetFancyStringWidth(str, scale: strscale);
                var strpos = position + new Vec2(-strw / 2, -1f);
                if (Square) strpos.y -= 4f;
                if (!Square) strpos.x += 14f;
                Graphics.DrawFancyString(str, strpos, Color.Gray, depth: -.5f, scale: strscale);
            }
        }

        public override ContextMenu GetContextMenu()
        {
            var menu = new EditorGroupMenu(null, true);
            menu.AddItem(_checkBox);
            return menu;
        }
    }
}
#endif

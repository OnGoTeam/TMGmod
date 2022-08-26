using System;
using System.Collections.Generic;
using System.Linq;
using DuckGame;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod.Cases
{
    public class ContextChanceRender : ContextMenu
    {
        private readonly List<(Holdable, string)> _drops;
        private readonly float _dropwidth;
        private readonly float _detailwidth;
        private readonly float _itemwidth;
        private readonly float _itemheight;
        private int _timeoffset;

        private static Tuple<Holdable, float> SpecTuple(SpawnSpec<Holdable> drop) => new(drop.Thing(), drop.Chance());

        private static bool ValidGraphic(Tuple<Holdable, float> tuple) => tuple.Item1?.graphic != null;

        public ContextChanceRender(
            IEnumerable<SpawnSpec<Holdable>> drops
        ) : base(null)
        {
            var withchance = drops.Select(SpecTuple).Where(ValidGraphic).ToList();
            var totalchance = withchance.Select(tuple => tuple.Item2).Sum();
            totalchance = Math.Max(totalchance, 1f);
            _drops = withchance.Select(
                tuple => (tuple.Item1, $"{tuple.Item2 / totalchance:P1}")
            ).ToList();
            var details = _drops.Select(drop => drop.Item2).ToList();
            var holdables = _drops.Select(drop => drop.Item1).ToList();
            _dropwidth = holdables.Select(
                holdable => (float)holdable.graphic.width
            ).Concat(
                new[] { Graphics.GetStringWidth("Drop") + 4f }
            ).Max();
            _detailwidth = details.Select(
                detail => 4f + Graphics.GetStringWidth(detail)
            ).Concat(
                new[] { Graphics.GetStringWidth("Chance") + 4f }
            ).Max();
            _itemwidth = _dropwidth + 2f + _detailwidth;
            var dropheights = holdables.Select(holdable => (float)holdable.graphic.height);
            var detailheights = details.Select(Graphics.GetStringHeight);
            _itemheight = dropheights.Concat(detailheights).Max() + 4f;
            itemSize.y = _itemheight * 5 + 4f;
            itemSize.x = _itemwidth + 4f;
        }

        private (Holdable, string) TupleNo(int ix)
        {
            ix %= _drops.Count;
            ix += _drops.Count;
            ix %= _drops.Count;
            return _drops[ix];
        }

        private float ExtraWidth() => itemSize.x - _itemwidth - 4f;


        private const int FramesPerItem = 60;

        private void DrawNo(int tupleix, int offsetix, int time)
        {
            var offsety = time / (float)FramesPerItem + offsetix;
            var x0 = x + 2f;
            var y0 = y + 3f;
            var basey = y0 + offsety * _itemheight + 1f;
            Graphics.DrawRect(
                new Rectangle(x0, basey, _dropwidth + ExtraWidth(), _itemheight - 2f),
                DuckGame.Color.Black, depth: depth + 3
            );
            Graphics.DrawRect(
                new Rectangle(x - 2f + itemSize.x - _detailwidth, basey, _detailwidth, _itemheight - 2f),
                DuckGame.Color.Black, depth: depth + 3
            );
            var tuple = TupleNo(tupleix);

            void DrawSprite(Sprite sprite)
            {
                sprite.CenterOrigin();
                sprite.x = x + 2f + (_dropwidth + ExtraWidth()) / 2f;
                sprite.y = basey + (_itemheight - 2f) / 2f;
                sprite.depth = depth + 4;
                sprite.Draw();
            }

            if (
                tuple.Item1 is IHaveAllowedSkins target
                &&
                !target.AllowedSkins.Contains(target.SkinValue)
                &&
                tuple.Item1.graphic is SpriteMap smap
            )
                ContextSkinRender.WithRandomized(new BaseGun.SkinMix(target, smap), DrawSprite);
            else
                DrawSprite(tuple.Item1.graphic);
            Graphics.DrawString(
                tuple.Item2,
                new Vec2(
                    x + itemSize.x - _detailwidth,
                    basey + (_itemheight - Graphics.GetStringHeight(tuple.Item2)) / 2f
                ),
                DuckGame.Color.White,
                depth: depth + 4
            );
        }

        private int _direction = +1;

        public override void Draw()
        {
            base.Draw();
            var totalmod = _drops.Count * FramesPerItem;
            var timeoffset = -MonoMain.timeInEditor * _direction - _timeoffset;
            timeoffset %= totalmod;
            timeoffset += totalmod;
            timeoffset %= totalmod;
            var startix = timeoffset / FramesPerItem;
            var startoffset = timeoffset - startix * FramesPerItem;
            for (var ix = 0; ix < 4; ix++)
                DrawNo(-startix + ix, ix, startoffset);
            Graphics.DrawRect(
                new Rectangle(x + 1f, y + 2f, itemSize.x - 2f, _itemheight + 2f), DuckGame.Color.Gray,
                depth: depth + 5
            );
            Graphics.DrawRect(
                new Rectangle(x + 2f, y + 3f, itemSize.x - 4f, _itemheight), DuckGame.Color.Black,
                depth: depth + 6
            );
            Graphics.DrawString(
                "Drops",
                new Vec2(
                    x + 4f,
                    y + 3f + (_itemheight - Graphics.GetStringHeight("Drops")) / 2f
                ),
                DuckGame.Color.White,
                depth: depth + 7
            );
            Graphics.DrawRect(
                new Rectangle(x + 1f, y + 2f + 4 * _itemheight, itemSize.x - 2f, _itemheight + 2f
                ),
                DuckGame.Color.Gray, depth: depth + 5);
            Graphics.DrawRect(
                new Rectangle(x + 2f, y + 3f + 4 * _itemheight, _dropwidth + ExtraWidth(), _itemheight),
                DuckGame.Color.Black, depth: depth + 6
            );
            Graphics.DrawString(
                "Drop",
                new Vec2(
                    x + 4f,
                    y + 3f + 4 * _itemheight + (_itemheight - Graphics.GetStringHeight("Drop")) / 2f
                ),
                DuckGame.Color.White,
                depth: depth + 7
            );
            Graphics.DrawRect(
                new Rectangle(x - 2f + itemSize.x - _detailwidth, y + 3f + 4 * _itemheight, _detailwidth, _itemheight),
                DuckGame.Color.Black, depth: depth + 6
            );
            Graphics.DrawString(
                "Chance",
                new Vec2(
                    x + itemSize.x - _detailwidth,
                    y + 3f + 4 * _itemheight + (_itemheight - Graphics.GetStringHeight("Chance")) / 2f
                ),
                DuckGame.Color.White,
                depth: depth + 7
            );
        }

        public override void Update()
        {
            if (Editor.inputMode == EditorInput.Mouse && Mouse.x >= x && Mouse.x <= x + itemSize.x &&
                Mouse.y >= y + 1.0 && Mouse.y <= y + itemSize.y - 1.0)
            {
                var delta = (int)(.11f * Mouse.scroll);
                _timeoffset += delta;
                if (delta * _direction < 0)
                {
                    _timeoffset += 2 * MonoMain.timeInEditor * _direction;
                    _direction *= -1;
                }
            }

            base.Update();
        }
    }
}

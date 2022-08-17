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
        private readonly List<Tuple<Holdable, string>> _drops;
        private readonly float _dropwidth;
        private readonly float _detailwidth;
        private readonly float _itemwidth;
        private readonly float _itemheight;

        private static Tuple<Holdable, float> SpecTuple(SpawnSpec<Holdable> drop) =>
            new Tuple<Holdable, float>(drop.Thing(), drop.Chance());

        private static bool ValidGraphic(Tuple<Holdable, float> tuple) => tuple.Item1?.graphic != null;

        public ContextChanceRender(
            IEnumerable<SpawnSpec<Holdable>> drops
        ) : base(null)
        {
            var withchance = drops.Select(SpecTuple).Where(ValidGraphic).ToList();
            var totalchance = withchance.Select(tuple => tuple.Item2).Sum();
            totalchance = Math.Max(totalchance, 1f);
            _drops = withchance.Select(tuple => new Tuple<Holdable, string>(tuple.Item1, $"{tuple.Item2 / totalchance}")).ToList();
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
            itemSize.y = _itemheight * 5 + 2f;
            itemSize.x = _itemwidth + 4f;
        }

        private Tuple<Holdable, string> TupleNo(int ix) => _drops[ix % _drops.Count];

        private float ExtraWidth() => itemSize.x - _itemwidth - 4f;


        private const int FramesPerItem = 60;

        private void DrawNo(int tupleix, int offsetix, int time)
        {
            var offsety = time / (float)FramesPerItem + offsetix;
            var x0 = x + 2f;
            var y0 = y + 2f;
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

        public override void Draw()
        {
            base.Draw();
            var timeoffset = int.MaxValue - MonoMain.timeInEditor;
            var startix = timeoffset / FramesPerItem;
            var startoffset = timeoffset - startix * FramesPerItem;
            for (var ix = 0; ix < 4; ix++)
                DrawNo(int.MaxValue - startix + ix, ix, startoffset);
            Graphics.DrawRect(
                new Rectangle(x + 1f, y + 1f, itemSize.x - 2f, _itemheight + 2f), DuckGame.Color.Gray,
                depth: depth + 5
            );
            Graphics.DrawRect(
                new Rectangle(x + 2f, y + 2f, itemSize.x - 4f, _itemheight), DuckGame.Color.Black,
                depth: depth + 6
            );
            Graphics.DrawString(
                "Drops",
                new Vec2(
                    x + 4f,
                    y + 2f + (_itemheight - Graphics.GetStringHeight("Drops")) / 2f
                ),
                DuckGame.Color.White,
                depth: depth + 7
            );
            Graphics.DrawRect(
                new Rectangle(x + 1f, y + 1f + 4 * _itemheight, itemSize.x - 2f, _itemheight + 2f
                ),
                DuckGame.Color.Gray, depth: depth + 5);
            Graphics.DrawRect(
                new Rectangle(x + 2f, y + 2f + 4 * _itemheight, _dropwidth + ExtraWidth(), _itemheight),
                DuckGame.Color.Black, depth: depth + 6
            );
            Graphics.DrawString(
                "Drop",
                new Vec2(
                    x + 4f,
                    y + 2f + 4 * _itemheight + (_itemheight - Graphics.GetStringHeight("Drop")) / 2f
                ),
                DuckGame.Color.White,
                depth: depth + 7
            );
            Graphics.DrawRect(
                new Rectangle(x - 2f + itemSize.x - _detailwidth, y + 2f + 4 * _itemheight, _detailwidth, _itemheight),
                DuckGame.Color.Black, depth: depth + 6
            );
            Graphics.DrawString(
                "Chance",
                new Vec2(
                    x + itemSize.x - _detailwidth,
                    y + 2f + 4 * _itemheight + (_itemheight - Graphics.GetStringHeight("Chance")) / 2f
                ),
                DuckGame.Color.White,
                depth: depth + 7
            );
        }
    }
}

using System.Collections.Generic;
using DuckGame;
using TMGmod.Cases;
using TMGmod.Cases.Color;
using TMGmod.Core;

namespace TMGmod.NY
{
    [EditorGroup("TMG|Misc|Holiday")]
    public class NewYearCase : BaseCase
    {
        public NewYearCase(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("Holiday/HolydayCase"), 14, 8);
            _graphic = sprite;
            sprite.frame = Rando.Int(0, 31);
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 4f;
            collideSounds.Add("presentLand");
            _editorName = "Holiday Case";
            ThingsDetailed = new List<SpawnSpec<Holdable>>
            {
                //x4
                B<CandyCane>(),
                B<PodarokClassic>(),
                //x3
                B<GarlandGun>().Chance(.75f),
                B<Popcal>().Chance(.75f),
                B<Icer>().Chance(.75f),
                B<SnowMgun>().Chance(.75f),
                //x2
                B<CandyCaneLime>().Chance(.5f),
                B<SkeetGun>().Chance(.5f),
                B<Helmet>().Chance(.5f),
                B<ChestPlate>().Chance(.5f),
                B<PodarokColorB>().Chance(.5f),
                B<PodarokColorC>().Chance(.5f),
                B<PodarokColorY>().Chance(.5f),
                B<PodarokColorR>().Chance(.5f),
                B<PodarokColorG>().Chance(.5f),
                B<PodarokMillitary>().Chance(.5f),
                //x1
                B<SpruceGun>().Chance(.25f),
                B<CandyCaneOrange>().Chance(.25f),
                B<PPLMG>().Chance(.25f),
            };
            CaseColor = BaseColor.Prismarine;
        }
    }
}

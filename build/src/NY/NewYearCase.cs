using System;
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
                B<CandyCane>(),
                B<CandyCane>(),
                B<CandyCane>(),
                B<PodarokClassic>(),
                B<PodarokClassic>(),
                B<PodarokClassic>(),
                B<PodarokClassic>(),
                //x3
                B<GarlandGun>(),
                B<GarlandGun>(),
                B<GarlandGun>(),
                B<Popcal>(),
                B<Popcal>(),
                B<Popcal>(),
                B<Icer>(),
                B<Icer>(),
                B<Icer>(),
                B<SnowMgun>(),
                B<SnowMgun>(),
                B<SnowMgun>(),
                //x2
                B<CandyCaneLime>(),
                B<CandyCaneLime>(),
                B<SkeetGun>(),
                B<SkeetGun>(),
                B<Helmet>(),
                B<Helmet>(),
                B<ChestPlate>(),
                B<ChestPlate>(),
                B<PodarokColorB>(),
                B<PodarokColorB>(),
                B<PodarokColorC>(),
                B<PodarokColorC>(),
                B<PodarokColorY>(),
                B<PodarokColorY>(),
                B<PodarokColorR>(),
                B<PodarokColorR>(),
                B<PodarokColorG>(),
                B<PodarokColorG>(),
                B<PodarokMillitary>(),
                B<PodarokMillitary>(),
                //x1
                B<SpruceGun>(),
                B<CandyCaneOrange>(),
                B<PPLMG>(),
            };
            CaseColor = BaseColor.Prismarine;
        }
    }
}

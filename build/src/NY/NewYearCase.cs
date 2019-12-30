using System;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Cases.Color;
using TMGmod.Cases;

namespace TMGmod.NY
{
    /// <inheritdoc />
    [EditorGroup("TMG|Misc|Holiday")]
    public class NewYearCase : BaseCase
    {
        /// <inheritdoc />
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
            Things = new List<Type>
            {
                   typeof(SkeetGun),  //x2
                typeof(GarlandGun),  //x3
                typeof(CandyCane),  //x3
                typeof(SpruceGun),  //x3
                typeof(Popcal),  //x3
                typeof(Icer),  //x3
                typeof(SnowMgun),  //x3
                   typeof(SkeetGun),  //x2
                typeof(GarlandGun),  //x3
                typeof(CandyCane),  //x3
                typeof(CandyCaneOrange),  //x1
                typeof(CandyCaneLime),  //x2
                typeof(SpruceGun),  //x3
                typeof(Popcal),  //x3
                typeof(Icer),  //x3
                typeof(SnowMgun),  //x3
                 typeof(Helmet),  //x2
                 typeof(ChestPlate),  //x2
                typeof(GarlandGun),  //x3
                typeof(CandyCane),  //x3
                typeof(CandyCaneLime),  //x2
                typeof(SpruceGun),  //x3
                typeof(Popcal),  //x3
                typeof(Icer),  //x3
                typeof(SnowMgun),  //x3
                 typeof(Helmet),  //x2
                 typeof(ChestPlate),  //x2
                   typeof(PodarokColorB),  //x1
                   typeof(PodarokColorC),  //x1
                   typeof(PodarokColorY),  //x1
                   typeof(PodarokColorR),  //x1
                   typeof(PodarokColorG),  //x1
                   typeof(PodarokMillitary)  //x1
            };
            CaseId = 6;
        }
    }
}
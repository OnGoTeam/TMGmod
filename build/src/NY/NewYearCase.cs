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
                   typeof(SkeetGun),
                typeof(GarlandGun),
                typeof(CandyCane),
                typeof(SpruceGun),
                typeof(SnowMgun),
                   typeof(SkeetGun),
                typeof(GarlandGun),
                typeof(CandyCane),
                typeof(SpruceGun),
                typeof(SnowMgun),
                 typeof(Helmet),
                 typeof(ChestPlate),
                typeof(GarlandGun),
                typeof(CandyCane),
                typeof(SpruceGun),
                typeof(SnowMgun),
                 typeof(Helmet),
                 typeof(ChestPlate),
                   typeof(PodarokColorB),
                   typeof(PodarokColorC),
                   typeof(PodarokColorY),
                   typeof(PodarokColorR),
                   typeof(PodarokColorG),
                   typeof(PodarokMillitary)
            };
            CaseId = 6;
        }
    }
}
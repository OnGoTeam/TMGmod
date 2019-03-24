using System;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;

namespace TMGmod.Cases.Color
{
    [EditorGroup("TMG|Misc|Cases")]
    public class PodarokColorC : BaseCase
    {
        public PodarokColorC(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("ColoredCases"), 14, 8);
            _graphic = sprite;
            sprite.frame = 2;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Cyan Container";
            Things = new List<Type>
            {
                typeof(SIX12S),
                typeof(DaewooK1),
                typeof(CZ805),
                typeof(MP5),
                typeof(MP5SD),
                typeof(HK417)
            };
            CaseId = 3;
        }
    }
}
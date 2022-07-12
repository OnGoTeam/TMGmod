using DuckGame;
using System;
using System.Collections.Generic;
using TMGmod.Core;

namespace TMGmod.Cases.Color
{
    [EditorGroup("TMG|Misc|Cases")]
    public class PodarokColorY : BaseCase
    {
        public PodarokColorY(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("ColoredCases"), 14, 8);
            _graphic = sprite;
            sprite.frame = 3;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Yellow Container";
            Things = new List<Type>
            {
                typeof(SIX12S),
                typeof(SIX12),
                typeof(DaewooK1),
                typeof(UziPro),
                typeof(MP5),
                typeof(MP5SD),
                typeof(AWS),
                typeof(CZ805),
                typeof(USP),
                typeof(SkeetGun),
                typeof(AKALFA),
                typeof(SMG9),
                typeof(AUGA1),
                typeof(PPSh41),
                typeof(PPK42),
                typeof(SKS),
                typeof(Glock18),
                typeof(Remington)
            };
            CaseId = 4;
        }
    }
}
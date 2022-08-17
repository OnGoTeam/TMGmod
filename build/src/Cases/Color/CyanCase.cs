using System;
using System.Collections.Generic;
using DuckGame;

namespace TMGmod.Cases.Color
{
    [EditorGroup("TMG|Misc|Cases")]
    public class PodarokColorC : BaseCase
    {
        public PodarokColorC(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("CaseColor"), 14, 8);
            _graphic = sprite;
            sprite.frame = 2;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Cyan Case";
            Things = new List<Type>
            {
                typeof(SIX12S),
                typeof(SIX12),
                typeof(DaewooK1),
                typeof(MP5),
                typeof(MP5SD),
                typeof(TC12),
                typeof(USP),
                typeof(CZ805),
                typeof(AF2011),
                typeof(MG44),
                typeof(MG44C),
                typeof(SV99),
                typeof(Lynx),
                typeof(PPSh41),
                typeof(PPK42),
                typeof(SKS),
                typeof(Glock18),
                typeof(RemingtonTac),
            };
            CaseId = CaseColor.Cyan;
        }
    }
}

using System;
using System.Collections.Generic;
using DuckGame;

namespace TMGmod.Cases.Color
{
    [EditorGroup("TMG|Misc|Cases")]
    public class PodarokColorB : BaseCase
    {
        public PodarokColorB(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("CaseColor"), 14, 8);
            _graphic = sprite;
            sprite.frame = 1;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Blue Case";
            Things = new List<Type>
            {
                typeof(SIX12S),
                typeof(SIX12),
                typeof(DaewooK1),
                typeof(UziPro),
                typeof(MP5),
                typeof(MP5SD),
                typeof(AWS),
                typeof(USP),
                typeof(BigShot),
                typeof(Vista),
                typeof(DragoShot),
                typeof(AUGA1),
                typeof(PPSh41),
                typeof(PPK42),
                typeof(SKS),
                typeof(SRM1208),
                typeof(M14),
                typeof(AR15Proto),
                typeof(Remington),
                typeof(RemingtonTac),
                typeof(DTSR44),
            };
            CaseId = CaseColor.Blue;
        }
    }
}

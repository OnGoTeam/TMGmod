#if DEBUG
using System;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;

namespace TMGmod.Cases.Color
{
    /// <inheritdoc />
    [EditorGroup("TMG|Misc|Cases")]
    public class PodarokColorG : BaseCase
    {
        /// <inheritdoc />
        public PodarokColorG(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("ColoredCases"), 14, 8);
            _graphic = sprite;
            sprite.frame = 4;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Green Container";
            Things = new List<Type>
            {
                typeof(AUGA1),
                typeof(AUGA3),
                typeof(AA12),
                typeof(DragoShot),
                typeof(MP40),
                typeof(PPSh41),
                typeof(PPK42),
                typeof(SV98),
                typeof(SV99),
                typeof(SRM1208),
                typeof(SKS)
            };
            CaseId = 8;
        }
    }
}
#endif
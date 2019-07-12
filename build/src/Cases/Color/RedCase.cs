using System;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
#if DEBUG
using TMGmod.Useless_or_deleted_Guns;
#endif

namespace TMGmod.Cases.Color
{
    /// <inheritdoc />
    [EditorGroup("TMG|Misc|Cases")]
    public class PodarokColorR : BaseCase
    {
        /// <inheritdoc />
        public PodarokColorR(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("ColoredCases"), 14, 8);
            _graphic = sprite;
            sprite.frame = 0;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Red Container";
            Things = new List<Type>
            {
                typeof(SIX12S),
                typeof(SIX12),
                typeof(DaewooK1),
                typeof(AUGA3),
                typeof(SkeetGun),
#if DEBUG
                typeof(PPSh),
                typeof(PPShC),
#endif
                typeof(PPSh41),
                typeof(PPK42),
                typeof(HK417),
                typeof(Vintorez),
                typeof(VSK94),
                typeof(PMRC),
                typeof(BigShot)
            };
            CaseId = 1;
        }
    }
}
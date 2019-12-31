using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
#if DEBUG
using TMGmod.Useless_or_deleted_Guns;
#endif

namespace TMGmod.Cases.Color
{
    [EditorGroup("TMG|Misc|Cases")]
    [UsedImplicitly]
    public class PodarokColorG : BaseCase
    {
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
#if DEBUG
                typeof(PPSh),
                typeof(PPShC),
#endif
                typeof(PPSh41),
                typeof(PPK42),
                typeof(SV98),
                typeof(SV99),
                typeof(SRM1208),
                typeof(SKS),
                typeof(AWS),
                typeof(PP19),
                typeof(G9M),
                typeof(AR15Proto),
                typeof(Vixr),
                typeof(VSK94),
                typeof(BarretM98),
                typeof(BarretM98C)
            };
            CaseId = 8;
        }
    }
}
using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Custom_Guns;
#if DEBUG
using TMGmod.Useless_or_deleted_Guns;
#endif

namespace TMGmod.Cases.Color
{
    [EditorGroup("TMG|Misc|Cases")]
    [UsedImplicitly]
    public class PodarokNitro : BaseCase
    {
        public PodarokNitro(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("NitroCase"), 14, 8);
            _graphic = sprite;
            sprite.frame = 0;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Nitro Case";
            Things = new List<Type>
            {
                typeof(SIX12S),
                typeof(SIX12),
                typeof(AWS),
#if DEBUG
                typeof(PPSh),
                typeof(PPShC),
#endif
                typeof(PPSh41),
                typeof(PPK42),
                typeof(MG44),
                typeof(SkeetGun),
                typeof(MP5),
                typeof(MP5SD),
                typeof(DaewooK1),
                typeof(USP),
                typeof(Vintorez),
                typeof(VSK94),
                typeof(BigShot),
                typeof(Arx200),
                typeof(AN94C),
                typeof(Type89),
                typeof(Rfb),
                typeof(FnFcar),
                typeof(HazeS),
                typeof(AA12),
                typeof(MP40),
                typeof(SKS)
            };
            CaseId = 7;
        }
    }
}
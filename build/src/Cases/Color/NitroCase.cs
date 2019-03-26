using System;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Custom_Guns;

namespace TMGmod.Cases.Color
{
    [EditorGroup("TMG|Misc|Cases")]
    public class PodarokNitro : BaseCase
    {
        public PodarokNitro(float xval, float yval) : base(xval, yval)
        {
            _graphic = new Sprite(GetPath("NitroCase"));
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
                typeof(PPSh),
                typeof(PPShC),
                typeof(MG44),
                typeof(SkeetGun),
                typeof(MP5),
                typeof(MP5SD),
                typeof(DaewooK1),
                typeof(USP),
                typeof(Vintorez),
                typeof(VintorezC),
                typeof(BigShot),
                typeof(Arx200),
                typeof(AN94C),
                typeof(Type89),
                typeof(Rfb),
                typeof(FnFcar),
                typeof(HazeS)
            };
            CaseId = 7;
        }
    }
}
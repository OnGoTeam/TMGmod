using System;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Custom_Guns;

namespace TMGmod.Cases.Color
{
    [EditorGroup("TMG|Misc|Cases")]
    public class PodarokPrismarine : BaseCase
    {
        public PodarokPrismarine(float xval, float yval) : base(xval, yval)
        {
            _graphic = new Sprite(GetPath("PrismarineCase"));
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Prismarine Case";
            Things = new List<Type>
            {
                typeof(SIX12S),
                typeof(SIX12),
                typeof(Arx200),
                typeof(UziPro),
                typeof(PPSh),
                typeof(PPShC),
                typeof(MG44),
                typeof(SkeetGun),
                typeof(MP5),
                typeof(MP5SD),
                typeof(AWS),
                typeof(AUGA1),
                typeof(AN94),
                typeof(Vixr),
                typeof(SpectreM4)
            };
            CaseId = 6;
        }
    }
}
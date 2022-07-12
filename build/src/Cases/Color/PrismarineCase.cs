using DuckGame;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using TMGmod.Core;
#if DEBUG
using TMGmod.Useless_or_deleted_Guns;
#endif

namespace TMGmod.Cases.Color
{
    [EditorGroup("TMG|Misc|Cases")]
    [UsedImplicitly]
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
#if DEBUG
                typeof(PPSh),
                typeof(PPShC), 
#endif
                typeof(PPSh41),
                typeof(PPK42),
                typeof(MG44),
                typeof(MG44C),
                typeof(SkeetGun),
                typeof(MP5),
                typeof(MP5SD),
                typeof(AWS),
                typeof(AUGA1),
                typeof(AN94),
                typeof(Vixr),
                typeof(SpectreM4),
                typeof(SKS)
            };
            CaseId = 6;
        }
    }
}
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;

namespace TMGmod.Cases.Color
{
    [EditorGroup("TMG|Misc|Cases")]
    [UsedImplicitly]
    public class PodarokPrismarine : BaseCase
    {
        public PodarokPrismarine(float xval, float yval) : base(xval, yval)
        {
            _graphic = new Sprite(GetPath("CasePrismarine"), 14, 8);
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Prismarine Case";
            ThingsDetailed = new List<SpawnSpec<Holdable>>
            {
                B<SIX12S>(),
                B<SIX12>(),
                B<Arx200>(),
                B<UziPro>(),
                B<PPSh41>(),
                B<PPK42>(),
                B<MG44>(),
                B<MG44C>(),
                B<SkeetGun>(),
                B<MP5>(),
                B<MP5SD>(),
                B<AWS>(),
                B<AUGA1>(),
                B<AN94>(),
                B<Vixr>(),
                B<SpectreM4>(),
                B<Bersa45>(),
                //B<SKS>(),
            };
            CaseColor = BaseColor.Prismarine;
        }
    }
}

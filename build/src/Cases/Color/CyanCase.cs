using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;

namespace TMGmod.Cases.Color
{
    [EditorGroup("TMG|Misc|Cases")]
    public class PodarokColorC : BaseCase
    {
        [UsedImplicitly]
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
            ThingsDetailed = new List<SpawnSpec<Holdable>>
            {
                B<SIX12S>(),
                B<SIX12>(),
                B<DaewooK1>(),
                B<MP5>(),
                B<MP5SD>(),
                B<TC12>(),
                B<USP>(),
                B<CZ805>(),
                B<AF2011>(),
                B<MG44>(),
                B<MG44C>(),
                B<SV99>(),
                B<Lynx>(),
                B<PPSh41>(),
                B<PPK42>(),
                B<SKS>(),
                B<Glock18>(),
                B<RemingtonTac>(),
            };
            CaseColor = BaseColor.Cyan;
        }
    }
}

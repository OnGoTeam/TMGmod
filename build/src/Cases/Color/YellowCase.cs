using System;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;

namespace TMGmod.Cases.Color
{
    [EditorGroup("TMG|Misc|Cases")]
    public class PodarokColorY : BaseCase
    {
        public PodarokColorY(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("CaseColor"), 14, 8);
            _graphic = sprite;
            sprite.frame = 3;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Yellow Case";
            ThingsDetailed = new List<SpawnSpec<Holdable>>
            {
                B<SIX12S>(),
                B<SIX12>(),
                B<DaewooK1>(),
                B<UziPro>(),
                B<MP5>(),
                B<MP5SD>(),
                B<AWS>(),
                B<CZ805>(),
                B<USP>(),
                B<SkeetGun>(),
                B<AKALFA>(),
                B<SMG9>(),
                B<AUGA1>(),
                B<PPSh41>(),
                B<PPK42>(),
                B<SKS>(),
                B<Glock18>(),
                B<Remington>(),
            };
            CaseColor = BaseColor.Yellow;
        }
    }
}

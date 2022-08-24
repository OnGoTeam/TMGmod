using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;

namespace TMGmod.Cases.Color
{
    [EditorGroup("TMG|Misc|Cases")]
    public class PodarokColorB : BaseCase
    {
        [UsedImplicitly]
        public PodarokColorB(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("CaseColor"), 14, 8);
            _graphic = sprite;
            sprite.frame = 1;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Blue Case";
            ThingsDetailed = new List<SpawnSpec<Holdable>>
            {
                B<SIX12S>(),
                B<SIX12>(),
                B<DaewooK1>(),
                B<UziPro>(),
                B<MP5>(),
                B<MP5SD>(),
                B<AWS>(),
                B<USP>(),
                B<BigShot>(),
                B<Vista>(),
                B<DragoShot>(),
                B<AF2011>(),
                B<AUGA1>(),
                B<PPSh41>(),
                B<PPK42>(),
                //B<SKS>(),
                B<SRM1208>(),
                B<M14>(),
                B<Remington>(),
                B<RemingtonTac>(),
                B<DTSR44>(),
                B<M16LMG>(),
                B<NellegaljaMk2>(),
                B<SVU>(),
                B<Taligator6000>(),
            };
            CaseColor = BaseColor.Blue;
        }
    }
}

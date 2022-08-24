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
    public class PodarokNitro : BaseCase
    {
        public PodarokNitro(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("CaseNitro"), 14, 8);
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
            ThingsDetailed = new List<SpawnSpec<Holdable>>
            {
                //B<MP5>(),
                B<MP5SD>(),
                //B<PPSh41>(),
                B<PPK42>(),
                B<DaewooK1>(),
                //B<M4A1>(),
                B<BigShot>(),
                //B<VSK94>(),
                B<AN94>(),
                //B<USP>(),
                //B<Type89>(),


                B<CZ805>(),
                B<HK417>(),
                //B<MP7>(),
                B<MG44>(),
                //B<MG44C>(),
                B<Arx200>(),
                //B<AN94C>(),
                //B<AR9SX>(),


                B<SIX12S>(),
                //B<SIX12>(),
                //B<SKS>(), return after fix
                B<Vintorez>(),
                //B<SkeetGun>(),
                //B<FnFcar>(),
                B<AA12>(),


                //B<SVU>(),
                B<Rfb>(),
                B<AWS>(),
                //B<HazeS>(),


#if FEATURES_1_2_X
                //B<Foucus>(),
#endif
            };
            CaseColor = BaseColor.Nitro;
        }
    }
}

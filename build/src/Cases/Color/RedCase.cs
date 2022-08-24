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
    public class PodarokColorR : BaseCase
    {
        [UsedImplicitly]
        public PodarokColorR(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("CaseColor"), 14, 8);
            _graphic = sprite;
            sprite.frame = 0;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Red Case";
            ThingsDetailed = new List<SpawnSpec<Holdable>>
            {
                B<SIX12S>().Chance(2f),
                B<SIX12>().Chance(2f),
                B<DaewooK1>(),
                B<AUGA1>(),
                B<AUGA3>(),
                B<SkeetGun>().Chance(2f),
                B<Bersa45>(),
#if DEBUG
                B<PPSh>(),
                B<PPShC>(),
#endif
                B<PPSh41>(),
                B<PPK42>(),
                B<HK417>(),
                B<Vintorez>(),
                B<VSK94>(),
                B<PMR>().Chance(2f),
                B<PMRC>(),
                B<BigShot>(),
                //B<SKS>(),
                B<SRM1208>().Chance(2f),
                B<PP19>(),
                B<RemingtonTac>().Chance(2f),
                B<M4A1>(),
                B<NellegaljaMk2>(),
                B<SVU>(),
                B<Taligator6000>().Chance(2f),
            };
            CaseColor = BaseColor.Red;
        }
    }
}

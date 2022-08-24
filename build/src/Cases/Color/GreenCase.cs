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
    public class PodarokColorG : BaseCase
    {
        public PodarokColorG(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("CaseColor"), 14, 8);
            _graphic = sprite;
            sprite.frame = 4;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Green Case";
            ThingsDetailed = new List<SpawnSpec<Holdable>>
            {
                B<AUGA1>(),
                B<AUGA3>(),
                B<AA12>(),
                B<DragoShot>(),
                B<MP40>(),
#if DEBUG
                B<PPSh>(),
                B<PPShC>(),
#endif
                B<PPSh41>(),
                B<PPK42>(),
                B<SV98>().Chance(2f),
                B<SV99>().Chance(2f),
                B<SRM1208>(),
                //B<SKS>().Chance(2f),
                B<AWS>().Chance(2f),
                B<PP19>(),
                B<Vixr>().Chance(2f),
                B<VSK94>().Chance(2f),
                B<BarretM98>().Chance(2f),
                B<BarretM98C>().Chance(2f),
                B<RemingtonTac>(),
                B<DTSR44>().Chance(2f),
                B<Bersa45>(),
                B<M16LMG>(),
                B<NellegaljaMk2>().Chance(2f),
                B<SVU>().Chance(2f),
            };
            CaseColor = BaseColor.Green;
        }
    }
}

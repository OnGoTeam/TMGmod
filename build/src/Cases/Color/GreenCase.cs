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
                B<SV98>(),
                B<SV99>(),
                B<SRM1208>(),
                //B<SKS>(),
                B<AWS>(),
                B<PP19>(),
                B<Vixr>(),
                B<VSK94>(),
                B<BarretM98>(),
                B<BarretM98C>(),
                B<RemingtonTac>(),
                B<DTSR44>(),
                B<Bersa45>(),
                B<M16LMG>(),
                B<NellegaljaMk2>(),
                B<SVU>(),
            };
            CaseColor = BaseColor.Green;
        }
    }
}

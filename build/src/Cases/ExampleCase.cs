#if DEBUG
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;

namespace TMGmod.Cases
{
    [UsedImplicitly]
    [BaggedProperty("canSpawn", false)]
    public class ExampleCase : BaseCase
    {
        public ExampleCase(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("CaseCivilian"), 14, 8);
            _graphic = sprite;
            sprite.frame = 1;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Example Case";
            ThingsDetailed = new List<SpawnSpec<Holdable>>
            {
                B<CZ75>(),
                B<AF2011>(),
                B<M93R>(),
                B<SMG9>(),
                B<Vista>(),
                B<BigShot>(),
                B<TC12>(),
                B<Rfb>(),
                B<Arx200>(),
                B<SV98>(),
                B<USP>(),
                B<UziPro>(),
                B<CZC2>(),
                B<DaewooK1>(),
            };
            CaseColor = BaseColor.Red;
        }
    }
}
#endif

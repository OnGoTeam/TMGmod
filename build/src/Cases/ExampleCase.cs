
using JetBrains.Annotations;
#if DEBUG
using System;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;

namespace TMGmod.Cases
{
    [UsedImplicitly]
    [BaggedProperty("canSpawn", false)]
    public class ExampleCase:BaseCase
    {
        public ExampleCase(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("CivilianCase"), 14, 8);
            _graphic = sprite;
            sprite.frame = 1;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Example Container";
            Things = new List<Type>
            {
                typeof(CZ75),
                typeof(AF2011),
                typeof(M93R),
                typeof(SMG9),
                typeof(Vista),
                typeof(BigShot),
                typeof(TC12),
                typeof(Rfb),
                typeof(Arx200),
                typeof(SV98),
                typeof(USP),
                typeof(UziPro),
                typeof(CZC2),
                typeof(DaewooK1)
            };
            CaseId = 1;
        }
    }
}
#endif
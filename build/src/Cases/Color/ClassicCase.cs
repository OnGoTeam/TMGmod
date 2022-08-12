using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;

namespace TMGmod.Cases.Color
{
    [EditorGroup("TMG|Misc|Cases")]
    [UsedImplicitly]
    public class PodarokClassic : BaseCase
    {
        public int Kostyl1 = 0;
        public int Kostyl2 = 0;
        public PodarokClassic(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("CaseClassic"), 14, 8);
            _graphic = sprite;
            sprite.frame = 0;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Classic Case";
            Things = new List<Type>
            {
                typeof(PodarokClassic)
            };
            CaseId = 0;
        }
        public override void Update()
        {
            base.Update();
            Kostyl1 += 1;
            if (Kostyl1 % 6 == 0)
            {
                Kostyl2 += 1;
                var sprite = new SpriteMap(GetPath("CaseClassic"), 14, 8);
                _graphic = sprite;
                sprite.frame = Kostyl2 % 4;
            }
        }
    }
}

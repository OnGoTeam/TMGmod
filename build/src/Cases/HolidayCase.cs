using System;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Cases
{
    [EditorGroup("TMG|Misc|Cases")]
    public class PodarokHoly : BaseCase
    {

        public PodarokHoly(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("Holiday/HolydayCase.png"), 14, 8);
            graphic = sprite;
            sprite.frame = 0;
            center = new Vec2(7f, 4f);
            collisionOffset = new Vec2(-7f, -4f);
            collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Holiday Case";
            Things = new List<Type>
            {
                typeof(Arx200),
                typeof(UziPro),
                typeof(PPSh),
                typeof(MG44),
                typeof(SkeetGun),
                typeof(MP5)
            };
            CaseId = 6;
        }
    }
}
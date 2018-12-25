using System;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Custom_Guns;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Cases.Color
{
    [EditorGroup("TMG|Misc|Cases")]
    public class PodarokColorR : BaseCase
    {

        public PodarokColorR(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("ColoredCases"), 14, 8);
            graphic = sprite;
            sprite.frame = 0;
            center = new Vec2(7f, 4f);
            collisionOffset = new Vec2(-7f, -4f);
            collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Blue Container";
            Things = new List<Type>
            {
                typeof(SIX12S),
                typeof(DaewooK1),
                typeof(AugC),
                typeof(SkeetGun),
                typeof(PPSh),
                typeof(HK417),
                typeof(Vintorez)
            };
            CaseId = 1;
        }
    }
}
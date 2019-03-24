﻿using System;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;

namespace TMGmod.Cases.Color
{
    [EditorGroup("TMG|Misc|Cases")]
    public class PodarokPrismarine : BaseCase
    {
        public PodarokPrismarine(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("PrismarineCase"), 14, 8);
            _graphic = sprite;
            sprite.frame = Rando.Int(0, 4);
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            _weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Sweet Case";
            Things = new List<Type>
            {
                typeof(Arx200),
                typeof(UziPro),
                typeof(PPSh),
                typeof(MG44),
                typeof(SkeetGun),
                typeof(MP5),
                typeof(MP5SD),
                typeof(AWS)
            };
            CaseId = 6;
        }
    }
}
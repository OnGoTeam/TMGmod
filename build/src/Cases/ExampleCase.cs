﻿using System;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Custom_Guns;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Cases
{
    [BaggedProperty("canSpawn", false)]
    public class ExampleCase:BaseCase
    {
        public ExampleCase(float xval, float yval) : base(xval, yval)
        {
            var sprite = new SpriteMap(GetPath("CivilianCase"), 14, 8);
            graphic = sprite;
            sprite.frame = 1;
            center = new Vec2(7f, 4f);
            collisionOffset = new Vec2(-7f, -4f);
            collisionSize = new Vec2(14f, 8f);
            depth = -0.5f;
            thickness = 0.0f;
            weight = 3f;
            collideSounds.Add("presentLand");
            _editorName = "Example Container";
            Things = new List<Type>
            {
                typeof(CZ75),
                typeof(AF2011),
                typeof(M93R),
                typeof(MAP),
                typeof(MPA27),
                typeof(BigShot),
                typeof(Nellegalja),
                typeof(Rfb),
                typeof(Arx200),
                typeof(SV98),
                typeof(USP),
                typeof(UziPro),
                typeof(Bren),
                typeof(DaewooK1)
            };
            CaseId = 1;
        }
    }
}
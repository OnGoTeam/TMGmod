﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Fully-Automatic")]
    [UsedImplicitly]
    public class Alep30 : BaseGun, IAmHg, IHaveAllowedSkins
    {
        public Alep30(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Alep 30";
            ammo = 18;
            SetAmmoType<ATAlep30>();
            Smap = new SpriteMap(GetPath("Alep30"), 16, 9);
            _flare = FrameUtils.TakeZis();
            _center = new Vec2(8f, 5f);
            _collisionOffset = new Vec2(-8f, -5f);
            _collisionSize = new Vec2(16f, 9f);
            _barrelOffsetTL = new Vec2(16f, 2f);
            _fireSound = GetPath("sounds/new/Alep30.wav");
            _fullAuto = true;
            _fireWait = 0.75f;
            _kickForce = 1f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.7f;
            _holdOffset = new Vec2(1f, 2f);
            ShellOffset = new Vec2(-6f, -3f);
            _weight = 2.3f;
        }
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 9 });
    }
}

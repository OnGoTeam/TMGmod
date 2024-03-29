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
    [UsedImplicitly]
    [EditorGroup("TMG|Shotgun|Break-Action|")]
    public class Deadly44 : BaseGun, IAmSg, IHaveAllowedSkins
    {
        public Deadly44(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "You Scared Ded";
            ammo = 1;
            SetAmmoType<AT44DB>(.1f);
            _numBulletsPerFire = 44;
            Smap = new SpriteMap(GetPath("44db"), 33, 10);
            _center = new Vec2(17f, 5f);
            _collisionOffset = new Vec2(-17f, -5f);
            _collisionSize = new Vec2(33f, 10f);
            _barrelOffsetTL = new Vec2(33f, 2f);
            _holdOffset = new Vec2(2f, 1f);
            _fireSound = GetPath("sounds/new/HeavyAss-1.wav");
            _flare = FrameUtils.FlareBase3();
            _fullAuto = false;
            _fireWait = 4f;
            _kickForce = 9f;
            loseAccuracy = 0.25f;
            maxAccuracyLost = 0.5f;
            _weight = 4.25f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        public override void Reload(bool shell = true)
        {
            base.Reload(false);
        }
    }
}

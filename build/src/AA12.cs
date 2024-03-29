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
    [EditorGroup("TMG|Shotgun|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class AA12 : BaseGun, IAmSg, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public AA12(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "AA-12";
            ammo = 12;
            SetAmmoType<AT12Gauge>(.6f);
            _numBulletsPerFire = 12;
            Smap = new SpriteMap(GetPath("AA12"), 34, 13);
            _center = new Vec2(17f, 7f);
            _collisionOffset = new Vec2(-17f, -7f);
            _collisionSize = new Vec2(34f, 13f);
            _barrelOffsetTL = new Vec2(34f, 5f);
            _fireSound = "shotgunFire";
            _flare = FrameUtils.FlareBase3();
            _fullAuto = true;
            _fireWait = 1.2f;
            _kickForce = 6f;
            loseAccuracy = 0.35f;
            maxAccuracyLost = 0.8f;
            _holdOffset = new Vec2(1f, 1f);
            ShellOffset = new Vec2(-1f, -3f);
            _weight = 7f;
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 125f;
            _ammoType.bulletThickness = 1.5f;
            base.OnInitialize();
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 7, 8, 9 });

        public override void Fire()
        {
            if (duck?.sliding == true) _accuracyLost = 0;
            base.Fire();
            if (duck is not { ragdoll: null, sliding: true, grounded: true }) return;
            duck.vSpeed = 0f;
        }
    }
}

﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [UsedImplicitly]
    [EditorGroup("TMG|Rifle|DMR")]
    // ReSharper disable once InconsistentNaming
    public class NellegaljaMk2 : BaseDmr, IHaveAllowedSkins
    {
        public NellegaljaMk2(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 20;
            SetAmmoType<ATSLK8>();
            MinAccuracy = 0.8f;
            RegenAccuracyDmr = 0.02f;
            DrainAccuracyDmr = 0.45f;
            Smap = new SpriteMap(GetPath("Nellegalja Mk2"), 33, 13);
            _center = new Vec2(17f, 7f);
            _collisionOffset = new Vec2(-17f, -7f);
            _collisionSize = new Vec2(33f, 13f);
            _barrelOffsetTL = new Vec2(33f, 5f);
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _holdOffset = new Vec2(2f, 1f);
            ShellOffset = new Vec2(0f, 0f);
            _fireSound = GetPath("sounds/Silenced1.wav");
            _fullAuto = false;
            _fireWait = 0.8f;
            _kickForce = 0.7f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _editorName = "Nellegalja Mk2";
            _weight = 5f;
            laserSight = true;
            _laserOffsetTL = new Vec2(18f, 2f);
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 800f;
            base.OnInitialize();
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
    }
}

﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Combined")]
    public class Rfb : BaseAr, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public Rfb(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "RFB";
            ammo = 20;
            SetAmmoType<ATRfb>(.9f);
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("RFB"), 33, 11);
            _center = new Vec2(17f, 5f);
            _collisionOffset = new Vec2(-17f, -5f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(0f, 1f);
            ShellOffset = new Vec2(10f, -2f);
            _fireSound = GetPath("sounds/new/DaewooK1.wav");
            _fullAuto = false;
            _fireWait = 0.46f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _weight = 5.5f;
            _kickForce = 0.07f;
            KforceDelta = 0.63f;
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 380f;
            base.OnInitialize();
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 7 });

        public override void Update()
        {
            if (Quacked())
            {
                if (_fullAuto)
                {
                    _fullAuto = false;
                    NonSkin = 0;
                    _fireWait = 0.46f;
                    maxAccuracyLost = 0.3f;
                    _ammoType.accuracy = 0.9f;
                }
                else
                {
                    _fullAuto = true;
                    NonSkin = 1;
                    _fireWait = 0.79f;
                    maxAccuracyLost = 0.45f;
                    _ammoType.accuracy = 0.6f;
                }

                SFX.Play(GetPath("sounds/tuduc.wav"));
            }

            base.Update();
        }
    }
}

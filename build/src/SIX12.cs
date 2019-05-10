﻿using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun|Semi-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class SIX12 : BaseGun, IHaveSkin, IAmSg
    {
        private readonly SpriteMap _sprite;
        public bool Laserino;
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        public EditorProperty<int> Skin { get; }
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 1, 2, 3, 4, 5, 6, 7 });
        public SIX12 (float xval, float yval)
          : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 6;
            _ammoType = new ATMagnum
            {
                range = 165f,
                accuracy = 0.87f,
                penetration = 1f,
                bulletThickness = 0.5f
            };
            BaseAccuracy = 0.87f;
            _numBulletsPerFire = 14;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("SIX12pattern"), 29, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(19.5f, 5f);
            _collisionOffset = new Vec2(-19.5f, -5f);
            _collisionSize = new Vec2(29f, 10f);
            _barrelOffsetTL = new Vec2(30f, 4.5f);
            _fireSound = "shotgunFire";
            _fullAuto = false;
            _fireWait = 2f;
            _kickForce = 5f;
            loseAccuracy = 0.3f;
            maxAccuracyLost = 0.4f;
            laserSight = false;
            _laserOffsetTL = new Vec2(24f, 7f);
            _holdOffset = new Vec2(2f, 0f);
            _editorName = "SIX12";
			_weight = 4f;
        }
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (Laserino)
                {
                    FrameId -= 10;
                    loseAccuracy = 0.3f;
                    maxAccuracyLost = 0.4f;
                    laserSight = false;
                    Laserino = false;
                }
                else
                {
                    FrameId += 10;
                    loseAccuracy = 0.5f;
                    maxAccuracyLost = 0.5f;
                    laserSight = true;
                    Laserino = true;
                }
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }
            base.Update();
        }
        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic))
            {
                bublic = Rando.Int(0, 9);
            }
            _sprite.frame = bublic;
        }

        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}
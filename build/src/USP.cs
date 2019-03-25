﻿using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Pistol")]
    // ReSharper disable once InconsistentNaming
    public class USP : BaseGun, IAmHg, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        public bool Silencer;
        public StateBinding SilencerBinding = new StateBinding(nameof(Silencer));
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public readonly EditorProperty<int> Skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 2, 3, 4, 6 });
        public USP(float xval, float yval)
          : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 13;
            _ammoType = new AT9mm
            {
                range = 100f,
                accuracy = 0.8f,
                penetration = 1f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("USPpattern"), 23, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(8f, 3f);
            _collisionOffset = new Vec2(-7.5f, -3.5f);
            _collisionSize = new Vec2(23f, 9f);
            _barrelOffsetTL = new Vec2(15f, 3f);
            _fireSound = GetPath("sounds/1.wav");
            _fullAuto = false;
            _fireWait = 0.75f;
            _kickForce = 0f;
            loseAccuracy = 0.05f;
            maxAccuracyLost = 0.1f;
            _editorName = "USP-S";
			_weight = 1f;
        }
        private void UpdateSkin()
        {
            var fid = Skin.value;
            while (!Allowedlst.Contains(fid))
            {
                fid = Rando.Int(0, 9);
            }
            _sprite.frame = fid;
        }
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (Silencer)
                {
                    graphic = new Sprite(GetPath("USP"));
                    _fireSound = GetPath("sounds/1.wav");
                    _ammoType = new AT9mm
                    {
                        range = 100f,
                        accuracy = 0.8f
                    };
                    _barrelOffsetTL = new Vec2(15f, 3f);
                    _sprite.frame -= 10;
                    Silencer = false;
                }
                else
                {
                    graphic = new Sprite(GetPath("USPS"));
                    _fireSound = GetPath("sounds/SilencedPistol.wav");
                    _ammoType = new AT9mmS
                    {
                        range = 130f,
                        accuracy = 0.9f
                    };
                    _barrelOffsetTL = new Vec2(23f, 3f);
                    _sprite.frame += 10;
                    Silencer = true;
                }
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }
            base.Update();
        }
        public float KforceDSmg { get; }
        public int CurrDelaySmg { get; set; }
        public int MaxDelaySmg { get; set; }
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
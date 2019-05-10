﻿using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;
namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Semi-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class PMRC : BaseGun, IAmHg, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        public EditorProperty<int> Skin { get; }
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 1, 5, 9 });
        public PMRC(float xval, float yval)
          : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 30;
            _ammoType = new AT9mm
            {
                range = 125f,
                accuracy = 0.75f,
                penetration = 1f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("PMR30pattern"), 16, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(8f, 5f);
            _collisionOffset = new Vec2(-8f, -5f);
            _collisionSize = new Vec2(16f, 10f);
            _barrelOffsetTL = new Vec2(16f, 2.5f);
            _holdOffset = new Vec2(0f, 2f);
            _fireSound = GetPath("sounds/1.wav");
            _fullAuto = false;
            _fireWait = 0.5f;
            _kickForce = 0.55f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.55f;
            _editorName = "PMR-30";
			_weight = 1f;
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
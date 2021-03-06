﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|MP")]
    // ReSharper disable once InconsistentNaming
    public class SMG9 : BaseSmg, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 4 });
        public SMG9(float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 24;
            _ammoType = new AT9mm
            {
                range = 100f,
                accuracy = 0.6f,
                penetration = 0.4f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("SMG9"), 16, 15);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(7f, 7f);
            _collisionOffset = new Vec2(-8f, -7f);
            _collisionSize = new Vec2(16f, 15f);
            _barrelOffsetTL = new Vec2(16f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel0"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _fireSound = GetPath("sounds/1.wav");
            _fullAuto = true;
            _fireWait = 0.35f;
            _kickForce = 2f;
            KforceDSmg = 3.2f;
            MaxDelaySmg = 5;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.57f;
            _holdOffset = new Vec2(-1f, 2f);
            ShellOffset = new Vec2(0f, 0f);
            _editorName = "SMG-9";
			_weight = 2.5f;
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
        [UsedImplicitly]
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
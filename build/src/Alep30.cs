﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    /// <inheritdoc cref="BaseGun"/>
    /// /// <inheritdoc cref="IAmHg"/>
    [EditorGroup("TMG|Handgun|Fully-Automatic")]
    public class Alep30 : BaseGun, IAmHg, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0 });
        public Alep30(float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 26;
            _ammoType = new AT9mm
            {
                range = 115f,
                accuracy = 0.6f,
                penetration = 0.4f,
                bulletSpeed = 30f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Alep30"), 16, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _center = new Vec2(8f, 3f);
            _collisionOffset = new Vec2(-7.5f, -3.5f);
            _collisionSize = new Vec2(16f, 11f);
            _barrelOffsetTL = new Vec2(16f, 1f);
            _fireSound = GetPath("sounds/2.wav");
            _fullAuto = true;
            _fireWait = 0.6f;
            _kickForce = 1.85f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.6f;
            _holdOffset = new Vec2(1f, 0f);
            ShellOffset = new Vec2(-3f, -1f);            
            _editorName = "Alep 30";
			_weight = 2.1f;
        }

        /// <inheritdoc />
        public override void OnHoldAction()
        {
            handAngle = Rando.Float(-0.08f, 0.08f);
            base.OnHoldAction();
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
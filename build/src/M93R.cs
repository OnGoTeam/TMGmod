﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Burst")]
    public class M93R : BaseBurst, IAmHg, IHaveAllowedSkins, I5
    {
        private const int NonSkinFrames = 1;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 5 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public M93R(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            _sprite = new SpriteMap(GetPath("M93R"), 12, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(6f, 2f);
            _collisionOffset = new Vec2(-6f, -2f);
            _collisionSize = new Vec2(12f, 9f);
            ammo = 15;
            _ammoType = new ATM93R();
            _barrelOffsetTL = new Vec2(13f, 1f);
            _flare = new SpriteMap(GetPath("FlareOnePixel0"), 12, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(-2f, 0f);
            _fireSound = GetPath("sounds/1.wav");
            _fullAuto = false;
            _fireWait = 1.5f;
            _kickForce = 0.24f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.35f;
            ShellOffset = new Vec2(0f, 0f);
            _editorName = "M93R";
            _weight = 2f;
            DeltaWait = 0.3f;
            BurstNum = 3;
        }

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}

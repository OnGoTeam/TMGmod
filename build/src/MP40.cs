﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [BaggedProperty("isInDemo", true)]
    [EditorGroup("TMG|SMG|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class MP40 : BaseSmg, IHaveAllowedSkins, I5
    {
        private const int NonSkinFrames = 1;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 5, 7, 8 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public MP40(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 32;
            _ammoType = new ATMP40();
            KforceDelta = 2f;
            KforceDelay = 20;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("MP40"), 23, 14);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(12f, 7f);
            _collisionOffset = new Vec2(-12f, -7f);
            _collisionSize = new Vec2(23f, 14f);
            _barrelOffsetTL = new Vec2(23f, 1f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.5f;
            _kickForce = 1.45f;
            _holdOffset = new Vec2(4f, 4f);
            ShellOffset = new Vec2(-1f, -6f);
            _editorName = "MP40";
            _weight = 3f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.4f;
            Compose(new FirstAccuracy(20, accuracy => accuracy - .3f));
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

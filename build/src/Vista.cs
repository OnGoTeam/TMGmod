﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Burst")]
    // ReSharper disable once InconsistentNaming
    public class Vista : BaseBurst, IFirstPrecise, IHaveSkin, I5
    {
        private const int NonSkinFrames = 1;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 2, 5 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public Vista(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(5, this, -1f, 9f, 0.5f);
            ammo = 30;
            _ammoType = new ATVista
            {
                range = 105f,
                accuracy = 0.75f,
            };
            MaxDelayFp = 30;
            BaseAccuracy = 1f;
            LowerAccuracyFp = 0.75f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Vista"), 16, 14);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(6f, 7f);
            _collisionOffset = new Vec2(-8f, -7f);
            _collisionSize = new Vec2(16f, 14f);
            _barrelOffsetTL = new Vec2(16f, 2f);
            _fireSound = GetPath("sounds/2.wav");
            _fullAuto = false;
            _fireWait = 0.36f;
            _kickForce = 1.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.44f;
            _holdOffset = new Vec2(-1f, 3f);
            ShellOffset = new Vec2(-4f, -3f);
            _editorName = "Vista";
            _weight = 2f;
            DeltaWait = 0.1f;
            BurstNum = 3;
        }

        public int CurrentDelayFp { get; set; }
        public int MaxDelayFp { get; }
        public float LowerAccuracyFp { get; }
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic)) bublic = Rando.Int(0, 9);
            _sprite.frame = bublic;
        }

        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}

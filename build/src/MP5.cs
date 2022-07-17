﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Combined")]
    // ReSharper disable once InconsistentNaming
    public class MP5 : BaseBurst, IFirstKforce, IFirstPrecise, IHaveSkin, IAmSmg
    {
        private const int NonSkinFrames = 2;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 2, 3, 4, 6, 7 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        [UsedImplicitly] public StateBinding NonAutoBinding = new StateBinding(nameof(NonAuto));

        public MP5(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 30;
            _ammoType = new ATMP5();
            MaxAccuracy = 0.9f;
            LowerAccuracyFp = 0.7f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("MP5"), 27, 12);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(13f, 6f);
            _collisionOffset = new Vec2(-13f, -6f);
            _collisionSize = new Vec2(27f, 12f);
            _barrelOffsetTL = new Vec2(27f, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = "deepMachineGun";
            _fullAuto = false;
            _fireWait = 0.5f;
            _kickForce = 0.5f;
            _holdOffset = new Vec2(-1f, 2f);
            ShellOffset = new Vec2(2f, -4f);
            _editorName = "MP5A3";
            _weight = 3f;
            KickForceDeltaSmg = 1.2f;
            MaxDelayFp = 10;
            MaxDelaySmg = 20;
            DeltaWait = 0.45f;
            BurstNum = 1;
        }

        [UsedImplicitly]
        public bool NonAuto
        {
            get => BurstNum == 1;
            set
            {
                BurstNum = value ? 1 : 3;
                _fireWait = value ? 0.5f : 1.8f;
                FrameId = FrameId % 10 + (value ? 0 : 10);
                _ammoType.accuracy = value ? 0.7f : 0.9f;
            }
        }

        public float KickForceDeltaSmg { get; }
        public int CurrentDelaySmg { get; set; }
        public int MaxDelaySmg { get; }
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

        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                NonAuto = !NonAuto;
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }

            base.Update();
        }

        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}

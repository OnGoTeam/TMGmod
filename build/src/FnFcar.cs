﻿using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Semi-Automatic")]
    public class FnFcar : BaseAr, IHaveSkin, IHaveBipods
    {
        private const int NonSkinFrames = 5;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 7 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        private float _bipodsstate;

        [UsedImplicitly] public NetSoundEffect BipOff = new NetSoundEffect(Mod.GetPath<Core.TMGmod>("sounds/beepods2"));

        [UsedImplicitly] public StateBinding BipOffBinding = new NetSoundBinding(nameof(BipOff));

        [UsedImplicitly] public NetSoundEffect BipOn = new NetSoundEffect(Mod.GetPath<Core.TMGmod>("sounds/beepods1"));

        [UsedImplicitly] public StateBinding BipOnBinding = new NetSoundBinding(nameof(BipOn));

        public FnFcar(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 14;
            _ammoType = new ATFCAR();
            _type = "gun";
            _sprite = new SpriteMap(GetPath("FCAR"), 36, 15);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(18f, 8f);
            _collisionOffset = new Vec2(-18f, -8f);
            _collisionSize = new Vec2(36f, 15f);
            _barrelOffsetTL = new Vec2(36f, 5f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _holdOffset = new Vec2(3f, 1f);
            ShellOffset = new Vec2(-3f, -3f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = false;
            _fireWait = 0.75f;
            _kickForce = 2.4f;
            KickForceSlowAr = 2.4f;
            KickForceFastAr = 0.9f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.2f;
            _editorName = "Belguria Fcar";
            laserSight = false;
            _laserOffsetTL = new Vec2(19f, 4f);
            _weight = 7f;
        }

        [UsedImplicitly]
        public float BipodsState
        {
            get => duck != null ? _bipodsstate : 0;
            set
            {
                value = Math.Max(value, 0f);
                value = Math.Min(value, 1f);
                _bipodsstate = value;
            }
        }

        [UsedImplicitly] public StateBinding BsBinding { get; } = new StateBinding(nameof(BipodsState));

        public bool Bipods
        {
            get => BipodsQ();
            set
            {
                var bipodsstate = BipodsState;
                if (isServerForObject)
                    BipodsState += 1f / 22 * (value ? 1 : -1);
                var nobipods = BipodsState < 0.01f;
                var bipods = BipodsState > 0.99f;
                _ammoType.accuracy = bipods ? 1f : 0.94f;
                _ammoType.bulletSpeed = bipods ? 72f : 36f;
                _fireWait = bipods ? 0.25f : 0.75f;
                _kickForce = bipods ? 0f : 2.4f;
                KickForceFastAr = bipods ? 0f : 0.9f;
                KickForceSlowAr = bipods ? 0f : 0.07f;
                loseAccuracy = bipods ? 0f : 0.15f;
                maxAccuracyLost = bipods ? 0f : 0.2f;
                FrameId = FrameId % 10 +
                          10 * (bipods ? 4 : nobipods ? 0 : bipodsstate < 0.33f ? 1 : bipodsstate < 0.67f ? 2 : 3);
                if (isServerForObject && bipods && bipodsstate <= 0.99f)
                    BipOn.Play();
                if (isServerForObject && nobipods && bipodsstate >= 0.01f)
                    BipOff.Play();
            }
        }

        [UsedImplicitly]
        public BitBuffer BipodsBuffer
        {
            get
            {
                var b = new BitBuffer();
                b.Write(Bipods);
                return b;
            }
            set => Bipods = value.ReadBool();
        }

        public StateBinding BipodsBinding { get; } = new StateBinding(nameof(BipodsBuffer));
        public bool BipodsDisabled { get; private set; }
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        public int FrameId
        {
            get => _sprite.frame;

            set
            {
                const int total = 10 * NonSkinFrames;
                _sprite.frame = (value % total + total) % total;
            }
        }

        public override void Update()
        {
            Bipods = Bipods;
            if (duck == null) BipodsDisabled = false;
            else if (!BipodsQ(this, true)) BipodsDisabled = false;
            else if (duck.inputProfile.Pressed("QUACK")) BipodsDisabled = !BipodsDisabled;
            base.Update();
        }

        public override void Fire()
        {
            if ((FrameId + 10) % (10 * NonSkinFrames) >= 20) return;
            base.Fire();
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

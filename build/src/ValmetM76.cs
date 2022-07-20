﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Combined")]
    public class ValmetM76 : BaseBurst, IHaveAllowedSkins
    {
        private readonly LoseAccuracy _loseAccuracy;
        private const int NonSkinFrames = 2;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
        private readonly SpriteMap _sprite;

        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public ValmetM76(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 20;
            _ammoType = new ATM76();
            MaxAccuracy = 0.89f;
            MinAccuracy = 0.5f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Valmet M76"), 33, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(17f, 5f);
            _collisionOffset = new Vec2(-17f, -5f);
            _collisionSize = new Vec2(33f, 10f);
            _barrelOffsetTL = new Vec2(33f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(4f, 1f);
            ShellOffset = new Vec2(-2f, -2f);
            _fireSound = GetPath("sounds/HeavyRifle.wav");
            _fullAuto = false;
            _fireWait = 0.7f;
            _kickForce = 3f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.25f; //0.4f
            _editorName = "Valmet M76";
            _weight = 4f;
            DeltaWait = 0.15f;
            BurstNum = 1;
            _loseAccuracy = new LoseAccuracy(0.15f, 0.02f, 1f);
            Compose(_loseAccuracy);
        }

        public bool NonAuto
        {
            get => BurstNum == 1;
            set
            {
                BurstNum = value ? 1 : 2;
                _fireWait = value ? 0.7f : 1.4f;
                FrameId = FrameId % 10 + (value ? 0 : 10);
                loseAccuracy = value ? 0.15f : 0f;
                _kickForce = value ? 3f : 6.5f;
                MaxAccuracy = value ? 0.89f : 1f;
                _loseAccuracy.Regen = value ? 0.02f : 0f;
                _loseAccuracy.Drain = value ? 0.15f : 0f;
                MaxAccuracy = value ? 0.89f : 1f;
            }
        }

        [UsedImplicitly] public StateBinding NonAutoBinding { get; } = new StateBinding(nameof(NonAuto));

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
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
    }
}

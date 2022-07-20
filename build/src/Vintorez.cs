﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.BipodsLogic;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Fully-Automatic")]
    public class Vintorez : BaseAr, IHaveAllowedSkins, IHaveBipods
    {
        private const int NonSkinFrames = 1;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 7 });

        private readonly SpriteMap _sprite;

        // ReSharper disable once InconsistentNaming
        [UsedImplicitly] private readonly EditorProperty<int> skin;

        public Vintorez(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 16;
            _ammoType = new ATVintorez();
            MinAccuracy = 0f;
            MaxAccuracy = 0.9f;
            _kickForce = 0.4f;
            KforceDelta = 0.45f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Vintorez"), 33, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(16f, 6f);
            _collisionOffset = new Vec2(-16f, -6f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 5f);
            _holdOffset = new Vec2(2f, 0f);
            ShellOffset = new Vec2(0f, 0f);
            _fireSound = GetPath("sounds/Silenced1.wav");
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _fullAuto = true;
            _fireWait = 0.7f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            _editorName = "Vintorez";
            _weight = 4.7f;
            Compose(new SpeedAccuracy(this, 1f, 1f, 0.5f));
        }

        protected override bool DynamicKforce() => false;

        public bool Bipods
        {
            get => HandleQ();
            set => _kickForce = value ? Rando.Float(0.5f, 1f) : 2.85f;
        }

        public BitBuffer BipodsBuffer
        {
            get => this.GetBipodBuffer();
            set => this.SetBipodBuffer(value);
        }

        public StateBinding BipodsBinding { get; } = new StateBinding(nameof(BipodsBuffer));
        public bool BipodsDisabled => false;

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}

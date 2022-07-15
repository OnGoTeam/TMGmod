﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Bolt-Action")]
    public class BarretM98 : BaseBolt, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 8 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public BarretM98(float xval, float yval) : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            _sprite = new SpriteMap(GetPath("BarretM98"), 50, 13);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(25f, 7f);
            _collisionOffset = new Vec2(-25f, -7f);
            _collisionSize = new Vec2(50f, 13f);
            _barrelOffsetTL = new Vec2(50f, 5f);
            _flare = new SpriteMap(GetPath("FlareOnePixel3"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            ammo = 8;
            _fireSound = GetPath("sounds/HeavySniper.wav");
            _kickForce = 6f;
            _holdOffset = new Vec2(7f, 0f);
            _editorName = "Barrett M98";
            _weight = 7f;
            ShellOffset = new Vec2(-6f, -3f);
            _ammoType = new ATBoltAction();
        }

        protected override void OnInitialize()
        {
            _ammoType.penetration = 4f;
            _ammoType.range = 850f;
            base.OnInitialize();
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

        protected override bool HasLaser() => false;
        protected override float MaxAngle() => 0.1f;
        protected override float MaxOffset() => 4.0f;
        protected override float ReloadSpeed() => .5f;
    }
}

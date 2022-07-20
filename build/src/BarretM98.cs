﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Bolt-Action")]
    public class BarretM98 : BaseBolt, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        private readonly SpriteMap _sprite;

        public BarretM98(float xval, float yval) : base(xval, yval)
        {
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
            ammo = 7;
            _fireSound = GetPath("sounds/HeavySniper.wav");
            _kickForce = 12f;
            _holdOffset = new Vec2(7f, 0f);
            _editorName = "Barrett M98";
            _weight = 7f;
            ShellOffset = new Vec2(-6f, -3f);
            _ammoType = new ATBoltAction();
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 8 });

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        protected override void OnInitialize()
        {
            _ammoType.penetration = 4f;
            _ammoType.range = 1000f;
            base.OnInitialize();
        }

        protected override bool HasLaser()
        {
            return false;
        }

        protected override float MaxAngle()
        {
            return 0.1f;
        }

        protected override float MaxOffset()
        {
            return 4.0f;
        }

        protected override float ReloadSpeed()
        {
            return .4f;
        }
    }
}

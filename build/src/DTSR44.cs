using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Bolt-Action")]
    // ReSharper disable once InconsistentNaming
    public class DTSR44 : BaseBolt, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 8 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public DTSR44(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            _sprite = new SpriteMap(GetPath("DT SR-44"), 29, 12);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(15f, 6f);
            _collisionOffset = new Vec2(-15f, -6f);
            _collisionSize = new Vec2(29f, 12f);
            _barrelOffsetTL = new Vec2(29f, 5f);
            ammo = 6;
            _flare = new SpriteMap(GetPath("FlareBase2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = GetPath("sounds/Silenced1.wav");
            _fullAuto = false;
            _kickForce = 3.1f;
            _holdOffset = new Vec2(-2f, 0f);
            _editorName = "DT SR-44";
            _weight = 3.5f;
            laserSight = false;
            ShellOffset = new Vec2(-8f, -2f);
            MaxAccuracy = .92f;
            _ammoType = new AT50SniperS();
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 713f;
            base.OnInitialize();
        }

        [UsedImplicitly] public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        protected override bool HasLaser() => false;
        protected override float MaxAngle() => 0.1f;
        protected override float MaxOffset() => -4.0f;
        protected override float ReloadSpeed() => .5f;
    }
}

#if DEBUG
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|NOTRELEASEDYET")]
    // ReSharper disable once InconsistentNaming
    public class DTSRM44 : BaseBolt, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public DTSRM44(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            _sprite = new SpriteMap(GetPath("DT SRM-44"), 37, 12);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(19f, 6f);
            _collisionOffset = new Vec2(-19f, -6f);
            _collisionSize = new Vec2(37f, 12f);
            _barrelOffsetTL = new Vec2(37f, 5f);
            ammo = 7;
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = GetPath("sounds/Silenced1.wav");
            _kickForce = 4.6f;
            _holdOffset = new Vec2(2f, 0f);
            _editorName = "DT SRM-44";
            _weight = 4.5f;
            _laserOffsetTL = new Vec2(30f, 7.5f);
            ShellOffset = new Vec2(-12f, -2f);
            MaxAccuracy = .97f;
            _ammoType = new AT762NATO();
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 889f;
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

        protected override bool HasLaser() => true;
        protected override float MaxAngle() => 0.1f;
        protected override float MaxOffset() => 4.0f;
        protected override float ReloadSpeed() => .5f;
    }
}
#endif

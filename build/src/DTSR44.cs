using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Bolt-Action")]
    // ReSharper disable once InconsistentNaming
    public class DTSR44 : BaseBolt, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        private readonly SpriteMap _sprite;

        public DTSR44(float xval, float yval)
            : base(xval, yval)
        {
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
            _kickForce = 3.1f;
            _holdOffset = new Vec2(-2f, 0f);
            _editorName = "DT SR-44";
            _weight = 3.5f;
            ShellOffset = new Vec2(-8f, -2f);
            MaxAccuracy = .92f;
            _ammoType = new AT50SniperS();
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 8 });

        [UsedImplicitly] public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 713f;
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
            return -4.0f;
        }

        protected override float ReloadSpeed()
        {
            return 1f;
        }
    }
}

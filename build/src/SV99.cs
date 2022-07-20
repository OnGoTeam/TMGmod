using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Bolt-Action")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class SV99 : BaseBolt, I5, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 3, 5, 8 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public SV99(float xval, float yval) : base(xval, yval)
        {
            skin = new EditorProperty<int>(8, this, -1f, 9f, 0.5f);
            _sprite = new SpriteMap(GetPath("SV99"), 27, 9);
            _graphic = _sprite;
            _sprite.frame = 8;
            _center = new Vec2(13f, 5f);
            _collisionOffset = new Vec2(-13f, -5f);
            _collisionSize = new Vec2(27f, 9f);
            _barrelOffsetTL = new Vec2(27f, 4f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            ammo = 6;
            _ammoType = new ATSV99();
            _fireSound = GetPath("sounds/Silenced3.wav");
            _kickForce = 1.8f;
            loseAccuracy = 0.5f;
            maxAccuracyLost = 1.5f;
            _holdOffset = new Vec2(-1f, 0f);
            _editorName = "SV-99";
            _weight = 2f;
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
        protected override float MaxAngle() => 0.3f;
        protected override float MaxOffset() => 2.0f;
        protected override float ReloadSpeed() => 2f;
    }
}

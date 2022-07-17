using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Semi-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class PMRC : BaseGun, IAmHg, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 5, 9 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public PMRC(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 30;
            _ammoType = new ATPMR30();
            _type = "gun";
            _sprite = new SpriteMap(GetPath("PMR30"), 16, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(8f, 5f);
            _collisionOffset = new Vec2(-8f, -5f);
            _collisionSize = new Vec2(16f, 10f);
            _barrelOffsetTL = new Vec2(16f, 2f);
            _holdOffset = new Vec2(0f, 2f);
            ShellOffset = new Vec2(-2f, -3f);
            _fireSound = GetPath("sounds/1.wav");
            _fullAuto = false;
            _fireWait = 0.5f;
            _kickForce = 1.5f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.35f;
            _editorName = "PMR-30";
            _weight = 2.5f;
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
    }
}

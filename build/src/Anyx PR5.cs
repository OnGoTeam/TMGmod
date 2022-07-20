using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassImplementations;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [UsedImplicitly]
    [EditorGroup("TMG|Handgun|Burst")]
    public class Glock18 : BaseBurst, IAmHg, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 3, 4 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public Glock18(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            _sprite = new SpriteMap(GetPath("Anyx PR5"), 12, 8);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(6f, 4f);
            _collisionOffset = new Vec2(-6f, -4f);
            _collisionSize = new Vec2(12f, 8f);
            ammo = 21;
            _ammoType = new ATPR5();
            _type = "gun";
            _barrelOffsetTL = new Vec2(12f, 1f);
            _flare = new SpriteMap(GetPath("FlareOnePixel0"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = GetPath("sounds/2.wav");
            _fullAuto = false;
            _fireWait = 0.5f;
            _kickForce = 0.5f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.6f;
            ShellOffset = new Vec2(-1f, -2f);
            _holdOffset = new Vec2(-1f, 2f);
            _editorName = "Anyx PR5";
            _weight = 1.7f;
            DeltaWait = 0.6f;
            BurstNum = 3;
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

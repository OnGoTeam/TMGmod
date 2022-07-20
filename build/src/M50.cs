using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.BipodsLogic;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Semi-Automatic")]
    public class M50 : BaseGun, IAmSr, IHaveAllowedSkins, IHaveBipods
    {
        private const int NonSkinFrames = 1;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public M50(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 7;
            _ammoType = new Cal50Explode
            {
                range = 1100f,
                accuracy = 1f,
                penetration = 1f,
                bulletThickness = 2.5f,
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("M50"), 40, 13);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(20f, 7f);
            _collisionOffset = new Vec2(-20f, -7f);
            _collisionSize = new Vec2(40f, 13f);
            _barrelOffsetTL = new Vec2(40f, 5f);
            _flare = new SpriteMap(GetPath("FlareOnePixel3"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = GetPath("sounds/HeavySniper.wav");
            _fullAuto = false;
            _fireWait = 3.75f;
            _kickForce = 8f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(6f, -1f);
            ShellOffset = new Vec2(0f, 0f);
            laserSight = true;
            _laserOffsetTL = new Vec2(31f, 9f);
            _editorName = "M50";
            _weight = 6.75f;
            Compose(new SpeedAccuracy(this, 1f, 1f, 1f));
        }

        public bool Bipods
        {
            get => HandleQ();
            set
            {
                _kickForce = value ? 1f : 8f;
                loseAccuracy = value ? 0f : 0.1f;
                maxAccuracyLost = value ? 0f : 0.3f;
            }
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

        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}

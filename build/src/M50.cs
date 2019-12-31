using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Semi-Automatic")]
    public class M50 : BaseGun, ISpeedAccuracy, IAmSr, IHaveSkin, IHaveBipods
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0 });

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
                bulletThickness = 2.5f
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
                center = new Vec2(0.0f, 5f)
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
            MuAccuracySr = 1f;
            LambdaAccuracySr = 0.67f;
        }
        public override void Update()
        {
            base.Update();
            Bipods = Bipods;
        }
        public bool Bipods
        {
            get => HandleQ();
            set
            {
                _kickForce = value ? 1f : 8f;
                loseAccuracy = value ? 0f : 0.1f;
                maxAccuracyLost = value ? 0f : 0.3f;
                LambdaAccuracySr = value ? 0f : 0.5f;
            }
        }
        [UsedImplicitly]
        public BitBuffer BipodsBuffer
        {
            get
            {
                var b = new BitBuffer();
                b.Write(Bipods);
                return b;
            }
            set => Bipods = value.ReadBool();
        }

        public StateBinding BipodsBinding { get; } = new StateBinding(nameof(BipodsBuffer));
        public bool BipodsDisabled => false;

        public float MuAccuracySr { get; }
        public float LambdaAccuracySr { get; private set; }
        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic))
            {
                bublic = Rando.Int(0, 9);
            }
            _sprite.frame = bublic;
        }
        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}
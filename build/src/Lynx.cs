using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Semi-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class Lynx : BaseDmr, ISpeedAccuracy, IHaveSkin, I5, IHaveBipods
    {
        private const int NonSkinFrames = 4;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 3, 5 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        private float _bipodsstate;

        [UsedImplicitly] public NetSoundEffect BipOff = new NetSoundEffect(Mod.GetPath<Core.TMGmod>("sounds/beepods2"));

        [UsedImplicitly] public StateBinding BipOffBinding = new NetSoundBinding(nameof(BipOff));

        [UsedImplicitly] public NetSoundEffect BipOn = new NetSoundEffect(Mod.GetPath<Core.TMGmod>("sounds/beepods1"));

        [UsedImplicitly] public StateBinding BipOnBinding = new NetSoundBinding(nameof(BipOn));

        public Lynx(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 6;
            _ammoType = new ATLynx();
            BaseAccuracy = 1f;
            MinAccuracy = 0.3f;
            RegenAccuracyDmr = 0.01f;
            DrainAccuracyDmr = 0.6f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Lynx"), 31, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(14f, 5.5f);
            _collisionOffset = new Vec2(-14.5f, -5.5f);
            _collisionSize = new Vec2(31f, 11f);
            _barrelOffsetTL = new Vec2(31f, 4f);
            _flare = new SpriteMap(GetPath("FlareOnePixel3"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _fireSound = GetPath("sounds/HeavySniper.wav");
            _fullAuto = false;
            _fireWait = 2f;
            _kickForce = 5.8f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(-1f, 1f);
            ShellOffset = new Vec2(-18f, -1f);
            laserSight = true;
            _laserOffsetTL = new Vec2(22f, 3.5f);
            _editorName = "Gepard Lynx";
            _weight = 6f;
        }

        [UsedImplicitly]
        public float BipodsState
        {
            get => duck != null ? _bipodsstate : 0;
            set => _bipodsstate = Maths.Clamp(value, 0f, 1f);
        }

        [UsedImplicitly] public StateBinding BsBinding { get; } = new StateBinding(nameof(BipodsState));

        public bool Bipods
        {
            get => BipodsQ();
            set
            {
                var bipodsstate = BipodsState;
                if (isServerForObject)
                    BipodsState += 1f / 8 * (value ? 1 : -1);
                var nobipods = BipodsState < 0.01f;
                var bipods = BipodsState > 0.99f;
                _ammoType.range = bipods ? 2400f : 1200f;
                _ammoType.bulletSpeed = bipods ? 150f : 48f;
                _fireWait = bipods ? 1.5f : 4f;
                _kickForce = bipods ? 0 : 5.8f;
                loseAccuracy = bipods ? 0 : 0.1f;
                maxAccuracyLost = bipods ? 0 : 0.3f;
                FrameId = FrameId % 10 + 10 * (bipods ? 3 : nobipods ? 0 : bipodsstate < 0.5f ? 1 : 2);
                if (isServerForObject && bipods && bipodsstate <= 0.99f)
                    BipOn.Play();
                if (isServerForObject && nobipods && bipodsstate >= 0.01f)
                    BipOff.Play();
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
        public bool BipodsDisabled { get; private set; }
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public float SpeedAccuracyThreshold => 0f;
        public float SpeedAccuracyHorizontal => 1f;
        public float SpeedAccuracyVertical => 0f;

        public override void Update()
        {
            Bipods = Bipods;
            if (duck == null) BipodsDisabled = false;
            else if (!BipodsQ(this, true)) BipodsDisabled = false;
            else if (duck.inputProfile.Pressed("QUACK")) BipodsDisabled = !BipodsDisabled;
            base.Update();
        }

        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic)) bublic = Rando.Int(0, 9);
            _sprite.frame = bublic;
        }

        public override void Fire()
        {
            if ((FrameId + 10) % (10 * NonSkinFrames) >= 20) return;
            base.Fire();
        }

        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}

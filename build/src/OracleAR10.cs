using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|DMR")]
    // ReSharper disable once InconsistentNaming
    public class OracleAR10 : BaseDmr, IHaveAllowedSkins, IHaveBipods
    {
        private const int NonSkinFrames = 1;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        private float _bipodsstate;

        [UsedImplicitly] public NetSoundEffect BipOff = new NetSoundEffect(Mod.GetPath<Core.TMGmod>("sounds/beepods2"));

        [UsedImplicitly] public StateBinding BipOffBinding = new NetSoundBinding(nameof(BipOff));

        [UsedImplicitly] public NetSoundEffect BipOn = new NetSoundEffect(Mod.GetPath<Core.TMGmod>("sounds/beepods1"));

        [UsedImplicitly] public StateBinding BipOnBinding = new NetSoundBinding(nameof(BipOn));

        [UsedImplicitly] public StateBinding HandAngleOffBinding = new StateBinding(nameof(HandAngleOff));

        public OracleAR10(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 10;
            _ammoType = new AT556NATO
            {
                range = 333f,
                accuracy = 0.91f,
            };
            MaxAccuracy = 0.91f;
            MinAccuracy = 0.35f;
            RegenAccuracyDmr = 0.015f;
            DrainAccuracyDmr = 0.3f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Oracle AR-10"), 29, 12);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(15f, 6f);
            _collisionOffset = new Vec2(-15f, -6f);
            _collisionSize = new Vec2(29f, 12f);
            _barrelOffsetTL = new Vec2(27f, 4f);
            _flare = new SpriteMap(GetPath("FlareTC12"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(-1f, 0f);
            ShellOffset = new Vec2(0f, 0f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = false;
            _fireWait = 0.5f;
            _kickForce = 2f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.15f;
            laserSight = false;
            _laserOffsetTL = new Vec2(17f, 1.5f);
            _editorName = "Oracle AR-10";
            _weight = 5f;
        }

        [UsedImplicitly]
        public float HandAngleOff
        {
            get => handAngle * offDir;
            set => handAngle = value * offDir;
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
                MaxAccuracy = bipods ? 1f : 0.91f;
                MinAccuracy = bipods ? 1f : 0.35f;
                _ammoType.range = bipods ? 666f : 333f;
                _ammoType.bulletSpeed = bipods ? 69f : 37f;
                loseAccuracy = bipods ? 0 : 0.15f;
                maxAccuracyLost = bipods ? 0 : 0.15f;
                _kickForce = bipods ? 0f : 2f;
                laserSight = bipods;
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

        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void Update()
        {
            Bipods = Bipods;
            if (duck == null) BipodsDisabled = false;
            else if (!BipodsQ(true)) BipodsDisabled = false;
            base.Update();
        }
    }
}

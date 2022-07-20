using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.BipodsLogic;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|DMR")]
    // ReSharper disable once InconsistentNaming
    public class OracleAR10 : BaseDmr, IHaveAllowedSkins, IHaveBipods
    {
        private const int NonSkinFrames = 1;
        private readonly SpriteMap _sprite;

        [UsedImplicitly] public StateBinding HandAngleOffBinding = new StateBinding(nameof(HandAngleOff));

        public OracleAR10(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 10;
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
            _ammoType = new AT556NATO();
        }

        public float HandAngleOff
        {
            get => handAngle * offDir;
            set => handAngle = value * offDir;
        }

        public BitBuffer BipodsBuffer
        {
            get => this.GetBipodBuffer();
            set => this.SetBipodBuffer(value);
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public bool Bipods
        {
            get => BipodsQ();
            set
            {
                _kickForce = value ? 0f : 2f;
                MaxAccuracy = value ? 1f : 0.91f;
                MinAccuracy = value ? 1f : 0.35f;
                _ammoType.range = value ? 666f : 333f;
                _ammoType.bulletSpeed = value ? 69f : 37f;
                loseAccuracy = value ? 0 : 0.15f;
                maxAccuracyLost = value ? 0 : 0.15f;
                laserSight = value;
            }
        }

        public StateBinding BipodsBinding { get; } = new StateBinding(nameof(BipodsBuffer));
        public bool BipodsDisabled => false;

        protected override void OnInitialize()
        {
            _ammoType.range = 333f;
            base.OnInitialize();
        }
    }
}

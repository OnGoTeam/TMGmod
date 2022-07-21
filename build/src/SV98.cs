using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.BipodsLogic;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Bolt-Action")]
    // ReSharper disable once InconsistentNaming
    public class SV98 : BaseBolt, IHaveAllowedSkins, I5, ISwitchBipods
    {
        private const int NonSkinFrames = 3;

        private readonly BipodStateContainer _bipodsState = new BipodStateContainer();
        private readonly SpriteMap _sprite;

        public SV98(float xval, float yval) : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("SV98"), 33, 11);
            _graphic = _sprite;
            BipOff = GetPath("sounds/beepods2");
            BipOn = GetPath("sounds/beepods1");
            _sprite.frame = 0;
            _center = new Vec2(17f, 6f);
            _collisionOffset = new Vec2(-17f, -6f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 4f);
            _flare = new SpriteMap(GetPath("FlareOnePixel3"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            ammo = 5;
            _fireSound = "sniper";
            _kickForce = 4.25f;
            _holdOffset = new Vec2(3f, 1f);
            _editorName = "SV-98";
            _weight = 4.5f;
            _laserOffsetTL = new Vec2(22f, 4f);
            ShellOffset = new Vec2(-1f, -2f);
            _ammoType = new ATBoltAction();
        }

        protected override float BaseKforce => this.BipodsDeployed() ? 0 : 4.67f;

        [UsedImplicitly]
        public BitBuffer BipodsBuffer
        {
            get => this.GetBipodBuffer();
            set => this.SetBipodBuffer(value);
        }

        public bool Bipods
        {
            get => BipodsQ();
            set => this.SetBipods(value);
        }

        public StateBinding BipodsBinding { get; } = new StateBinding(nameof(BipodsBuffer));
        public bool BipodsDisabled { get; private set; }

        public void SetBipodsDisabled(bool disabled)
        {
            BipodsDisabled = disabled;
        }

        public string BipOn { get; }
        public string BipOff { get; }

        [UsedImplicitly]
        public float BipodsState
        {
            get => _bipodsState.Get(this);
            set => _bipodsState.Set(value);
        }

        [UsedImplicitly] public StateBinding BsBinding { get; } = new StateBinding(nameof(BipodsState));

        public void UpdateBipodsStats(float old)
        {
            UpdateStats();
            UpdateFrames();
            this.UpdateBipodsSounds(old);
        }

        public float BipodSpeed => 1f / 7f;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 5, 8 });
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 1250f;
            base.OnInitialize();
        }

        private void UpdateStats()
        {
            _ammoType.range = this.BipodsDeployed() ? 2500f : 1250f;
            _ammoType.bulletSpeed = this.BipodsDeployed() ? 150f : 50f;
        }

        private void UpdateFrames()
        {
            FrameId = FrameId % 10 + 10 * (this.BipodsDeployed() ? 2 : this.BipodsFolded() ? 0 : 1);
        }


        protected override bool HasLaser()
        {
            return true;
        }

        protected override float MaxAngle()
        {
            return Bipods ? .05f : .15f;
        }

        protected override float MaxOffset()
        {
            return 4.0f;
        }

        protected override float ReloadSpeed()
        {
            return Bipods ? 1.5f : 1f;
        }
    }
}

using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.BipodsLogic;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Bolt-Action")]
    // ReSharper disable once InconsistentNaming
    public class AWS : BaseBolt, IHaveAllowedSkins, I5, ISwitchBipods
    {
        // Amazon Web Services

        private readonly BipodStateContainer _bipodsState = new BipodStateContainer();

        [UsedImplicitly]
        public AWS(float xval, float yval)
            : base(xval, yval)

        {
            _editorName = "AWS";
            NonSkinFrames = 3;
            Smap = new SpriteMap(GetPath("AWS"), 33, 11);
            BipOff = GetPath("sounds/beepods2");
            BipOn = GetPath("sounds/beepods1");
            _center = new Vec2(17f, 6f);
            _collisionOffset = new Vec2(-17f, -6f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 4f);
            ammo = 6;
            _flare = FrameUtils.FlareSilencer();
            _fireSound = GetPath("sounds/new/AWS.wav");
            _kickForce = 3.8f;
            _holdOffset = new Vec2(2f, 1f);
            _weight = 5f;
            ShellOffset = new Vec2(-2f, -2f);
            SetAmmoType<AT50SniperS>();
        }

        protected override float BaseKforce => this.BipodsDeployed() ? 0 : 4.75f;
        protected override float BaseAccuracy => this.BipodsDeployed() ? 1f : 0.97f;

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

        public float BipodsState
        {
            get => _bipodsState.Get(this);
            set => _bipodsState.Set(value);
        }

        public StateBinding BsBinding { get; } = new StateBinding(nameof(BipodsState));

        public void UpdateBipodsStats(float old)
        {
            UpdateStats();
            UpdateFrames();
            this.UpdateBipodsSounds(old);
        }

        public float BipodSpeed => 1f / 10f;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 4, 5, 6, 7, 8, 9 });

        protected override void OnInitialize()
        {
            _ammoType.range = 550f;
            base.OnInitialize();
        }

        private void UpdateStats()
        {
            _ammoType.range = this.BipodsDeployed() ? 1100f : 550f;
            _ammoType.bulletSpeed = this.BipodsDeployed() ? 100f : 35f;
        }

        private void UpdateFrames() => NonSkin = this.BipodsDeployed() ? 2 : this.BipodsFolded() ? 0 : 1;


        protected override bool HasLaser() => false;

        protected override float MaxAngle() => Bipods ? .05f : .25f;

        protected override float MaxOffset() => 3.0f;

        protected override float ReloadSpeed() => Bipods ? 1.5f : 1f;
    }
}

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
    public class SV98 : BaseBolt, IHaveAllowedSkins, I5, ISwitchBipods
    {
        private readonly BipodStateContainer _bipodsState = new BipodStateContainer();

        public SV98(float xval, float yval) : base(xval, yval)
        {
            _editorName = "SV-98";
            NonSkinFrames = 3;
            Smap = new SpriteMap(GetPath("SV98"), 33, 11);
            BipOff = GetPath("sounds/beepods2");
            BipOn = GetPath("sounds/beepods1");
            _center = new Vec2(17f, 6f);
            _collisionOffset = new Vec2(-17f, -6f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 4.5f);
            _flare = FrameUtils.FlareOnePixel3();
            ammo = 5;
            _fireSound = GetPath("sounds/new/SV98.wav");
            _kickForce = 4.25f;
            _holdOffset = new Vec2(3f, 1f);
            _weight = 4.5f;
            _laserOffsetTL = new Vec2(21f, 4.5f);
            ShellOffset = new Vec2(-5f, -1f);
            SetAmmoType<ATBoltAction>();
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

        protected override void OnInitialize()
        {
            _ammoType.range = 950f;
            base.OnInitialize();
        }

        private void UpdateStats()
        {
            _ammoType.range = this.BipodsDeployed() ? 2500f : 1250f;
            _ammoType.bulletSpeed = this.BipodsDeployed() ? 150f : 50f;
        }

        private void UpdateFrames()
        {
            NonSkin = this.BipodsDeployed() ? 2 : this.BipodsFolded() ? 0 : 1;
        }


        protected override bool HasLaser()
        {
            return true;
        }

        protected override float MaxAngle() => Bipods ? .05f : .15f;

        protected override float MaxOffset()
        {
            return 4.0f;
        }

        protected override float ReloadSpeed() => Bipods ? 1.5f : 1f;
    }
}

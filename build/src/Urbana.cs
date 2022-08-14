using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.BipodsLogic;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Bolt-Action")]
    // ReSharper disable once InconsistentNaming
    public class Urbana : BaseBolt, IHaveAllowedSkins, ISwitchBipods
    {
        private readonly BipodStateContainer _bipodsState = new BipodStateContainer();

        public Urbana(float xval, float yval) : base(xval, yval)
        {
            _editorName = "Urbana";
            NonSkinFrames = 4;
            Smap = new SpriteMap(GetPath("Urbana"), 53, 15);
            BipOff = GetPath("sounds/beepods2");
            BipOn = GetPath("sounds/beepods1");
            _center = new Vec2(27f, 8f);
            _collisionOffset = new Vec2(-27f, -8f);
            _collisionSize = new Vec2(53f, 15f);
            _barrelOffsetTL = new Vec2(53f, 5f);
            _flare = new SpriteMap(GetPath("FlareOnePixel3"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            ammo = 6;
            _fireSound = GetPath("sounds/new/HeavySniper.wav");
            _fullAuto = false;
            _kickForce = 5.25f;
            laserSight = false;
            _laserOffsetTL = new Vec2(31f, 9f);
            _holdOffset = new Vec2(9f, 1f);
            _weight = 5.6f;
            ShellOffset = new Vec2(-9f, -2f);
            SetAmmoType<ATBoltAction>();
        }

        protected override float BaseKforce => this.BipodsDeployed() ? 0 : 5f;

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

        public float BipodSpeed => 1f / 20f;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        protected override void OnInitialize()
        {
            _ammoType.bulletSpeed = 75f;
            _ammoType.range = 1200f;
            _ammoType.penetration = 2f;
            base.OnInitialize();
        }

        private void UpdateStats()
        {
            _ammoType.range = this.BipodsDeployed() ? 1800f : 1200f;
            _ammoType.bulletSpeed = this.BipodsDeployed() ? 150f : 75f;
        }

        private void UpdateFrames()
        {
            NonSkin = this.BipodsDeployed() ? 3 : this.BipodsFolded() ? 0 : BipodsState < .5f ? 1 : 2;
        }


        protected override bool HasLaser()
        {
            return false;
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
            return Bipods ? 1f : .75f;
        }
    }
}

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
    [EditorGroup("TMG|Sniper|Semi-Automatic")]
    public class FnFcar : BaseDmr, IHaveAllowedSkins, ISwitchBipods
    {
        private readonly BipodStateContainer _bipodsState = new BipodStateContainer();

        [UsedImplicitly]
        public FnFcar(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Belguria Fcar";
            ammo = 14;
            BipOff = GetPath("sounds/beepods2");
            BipOn = GetPath("sounds/beepods1");
            SetAmmoType<ATFCAR>();
            MinAccuracy = 0.67f;
            RegenAccuracyDmr = 0.01f;
            DrainAccuracyDmr = 0.1f;
            NonSkinFrames = 5;
            Smap = new SpriteMap(GetPath("FCAR"), 36, 15);
            _center = new Vec2(18f, 8f);
            _collisionOffset = new Vec2(-18f, -8f);
            _collisionSize = new Vec2(36f, 15f);
            _barrelOffsetTL = new Vec2(36f, 5.5f);
            _flare = FrameUtils.FlareOnePixel2();
            _holdOffset = new Vec2(3f, 1f);
            ShellOffset = new Vec2(-3f, -3f);
            _fireSound = GetPath("sounds/new/scar.wav");
            _fullAuto = false;
            _fireWait = 0.75f;
            _kickForce = 2.4f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            laserSight = false;
            _laserOffsetTL = new Vec2(19f, 4f);
            _weight = 7f;
        }

        protected override float BaseKforce => this.BipodsDeployed() ? 0f : 2.4f;

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

        public float BipodSpeed => 1f / 22f;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 7 });

        private void UpdateStats()
        {
            _ammoType.bulletSpeed = this.BipodsDeployed() ? 72f : 36f;
            _fireWait = this.BipodsDeployed() ? 0.25f : 0.75f;
            MaxAccuracy = this.BipodsDeployed() ? 1f : 0.94f;
            MinAccuracy = this.BipodsDeployed() ? 0.95f : 0.67f;
        }

        private void UpdateFrames()
        {
            NonSkin = this.BipodsDeployed() ? 4 :
                this.BipodsFolded() ? 0 :
                BipodsState < 0.33f ? 1 :
                BipodsState < 0.67f ? 2 : 3;
        }
    }
}

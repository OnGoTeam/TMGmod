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
    [EditorGroup("TMG|LMG")]
    // ReSharper disable once InconsistentNaming
    public class MG3 : BaseLmg, IHaveAllowedSkins, I5, ISwitchBipods
    {
        private readonly BipodStateContainer _bipodsState = new BipodStateContainer();

        public MG3(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 80;
            BipOff = GetPath("sounds/beepods2");
            BipOn = GetPath("sounds/beepods1");
            NonSkinFrames = 6;
            Smap = new SpriteMap(GetPath("mg3"), 39, 11);
            _center = new Vec2(19.5f, 5.5f);
            _collisionOffset = new Vec2(-19.5f, -5.5f);
            _collisionSize = new Vec2(39f, 11f);
            _barrelOffsetTL = new Vec2(39f, 3f);
            _fireSound = GetPath("sounds/RifleOrMG.wav");
            _fullAuto = true;
            _fireWait = 0.5f;
            _kickForce = 2.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.25f;
            _holdOffset = new Vec2(4f, 1.5f);
            ShellOffset = new Vec2(-2f, -3f);
            _editorName = "MG3";
            _weight = 7f;
            _ammoType = new AT556NATO();
            MaxAccuracy = .8f;
        }

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

        [UsedImplicitly] public StateBinding BsBinding { get; } = new StateBinding(nameof(BipodsState));

        public void UpdateBipodsStats(float old)
        {
            UpdateStats();
            UpdateFrames();
            this.UpdateBipodsSounds(old);
        }

        public float BipodSpeed => 1f / 15f;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 5 });

        protected override void OnInitialize()
        {
            _ammoType.range = 480f;
            base.OnInitialize();
        }

        private void UpdateStats()
        {
            _ammoType.range = this.BipodsDeployed() ? 550f : 480f;
            _ammoType.bulletSpeed = this.BipodsDeployed() ? 40f : 28f;
            KickForce1Lmg = this.BipodsDeployed() ? 0 : 2.0f;
            KickForce2Lmg = this.BipodsDeployed() ? 0 : 3.0f;
            loseAccuracy = this.BipodsDeployed() ? 0 : 0.1f;
            maxAccuracyLost = this.BipodsDeployed() ? 0 : 0.25f;
        }

        private void UpdateFrames()
        {
            NonSkin = NonSkin % 2 + 2 * (this.BipodsDeployed() ? 2 : this.BipodsFolded() ? 0 : 1);
        }

        public override void Update()
        {
            if (ammo == 0 && NonSkin % 2 == 0) NonSkin += 1;
            base.Update();
        }
    }
}

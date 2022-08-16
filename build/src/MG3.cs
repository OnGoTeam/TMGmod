#if DEBUG
using System;
#endif
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
            _editorName = "MG3";
            ammo = 80;
            BipOff = GetPath("sounds/beepods2");
            BipOn = GetPath("sounds/beepods1");
            NonSkinFrames = 6;
            Smap = new SpriteMap(GetPath("mg3"), 39, 11);
            _center = new Vec2(20f, 6f);
            _collisionOffset = new Vec2(-20f, -6f);
            _collisionSize = new Vec2(39f, 11f);
            _barrelOffsetTL = new Vec2(39f, 3f);
            _fireSound = GetPath("sounds/new/HighCaliber-Impactful.wav");
            _fullAuto = true;
            _fireWait = .5f;
            _kickForce = 2.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.25f;
            _holdOffset = new Vec2(4f, 2f);
            ShellOffset = new Vec2(-1f, -3f);
            _weight = 7f;
            SetAmmoType<AT556NATO>(.8f);
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
#if DEBUG
        private float _acc;
        private const float Fw0 = .5f;
        private float _fw = Fw0;
        private const float FwC = 1.0f;
        private const float FwM = Fw0 - FwC * 2f / 3f;

        protected override void BaseOnUpdate()
        {
            _acc -= .001f;
            _acc = Maths.Clamp(_acc, 0f, 1f);
            _fireWait = _fw;
        }

        protected override void BaseOnSpent()
        {
            var waitbase = (_fw - FwM) / FwC;
            waitbase = Maths.Clamp(waitbase, .0001f, .9999f);
            var accbase = _acc;
            accbase = Maths.Clamp(accbase, 0f, 1f);
            accbase = (float) Math.Sqrt(accbase);
            accbase = Maths.Clamp(accbase, 0f, 1f);
            var bifurcation = 3 + accbase * .6785728f;
            bifurcation = Maths.Clamp(bifurcation, 1f, 4f);
            waitbase = bifurcation * waitbase * (1 - waitbase);
            waitbase = Maths.Clamp(waitbase, 0f, 1f);
            _fireWait = _fw = waitbase * FwC + FwM;
            _acc += .03f;
            _acc = Maths.Clamp(_acc, 0f, 1f);
        }
#endif
    }
}

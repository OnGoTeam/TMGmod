#if FEATURES_1_2
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core.BipodsLogic;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|NOTRELEASEDYET")]
    [UsedImplicitly]
    public class Lstk16V6 : BaseGun, IHaveAllowedSkins, ISwitchBipods
    {
        private readonly BipodStateContainer _bipodsState = new BipodStateContainer();

        public Lstk16V6(float xval, float yval) : base(xval, yval)
        {
            _editorName = "LSTK-16v6";
            ammo = 25;
            BipOff = GetPath("sounds/beepods2");
            BipOn = GetPath("sounds/beepods1");
            SetAmmoType<ATM16>();
            NonSkinFrames = 9;
            Smap = new SpriteMap(GetPath("LSTK16v6"), 33, 14);
            _center = new Vec2(17f, 7f);
            _collisionOffset = new Vec2(-17f, -7f);
            _collisionSize = new Vec2(33f, 14f);
            _barrelOffsetTL = new Vec2(34f, 5f);
            _flare = new SpriteMap(GetPath("FlareTC12"), 13, 10)
            {
                center = new Vec2(1.0f, 6f),
            };
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.9f;
            _kickForce = 5.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.45f;
            _holdOffset = new Vec2(3f, 0f);
            ShellOffset = new Vec2(-5f, -2f);
            _weight = 6.7f;
        }

        protected override float BaseKforce => this.BipodsDeployed() ? 0 : 5.5f;

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

        public float BipodSpeed => 1f / 10f;

        public void UpdateBipodsStats(float old)
        {
            UpdateFrames();
            this.UpdateBipodsSounds(old);
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        private void UpdateFrames()
        {
            NonSkin = NonSkin % 3 + 3 * (this.BipodsDeployed() ? 2 : this.BipodsFolded() ? 0 : 1);
        }
    }
}
#endif

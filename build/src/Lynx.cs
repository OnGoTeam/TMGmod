using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.BipodsLogic;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Semi-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class Lynx : BaseGun, IAmDmr, IHaveAllowedSkins, I5, ICanDisableBipods, IDeployBipods
    {
        private const int NonSkinFrames = 4;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 3, 5 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public string BipOn { get; } = Mod.GetPath<Core.TMGmod>("sounds/beepods1");
        public string BipOff { get; } = Mod.GetPath<Core.TMGmod>("sounds/beepods2");

        public Lynx(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 6;
            _ammoType = new ATLynx();
            MaxAccuracy = 1f;
            MinAccuracy = 0.3f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Lynx"), 31, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(14f, 5.5f);
            _collisionOffset = new Vec2(-14.5f, -5.5f);
            _collisionSize = new Vec2(31f, 11f);
            _barrelOffsetTL = new Vec2(31f, 4.5f);
            _flare = new SpriteMap(GetPath("FlareOnePixel3"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = GetPath("sounds/HeavySniper.wav");
            _fullAuto = false;
            _fireWait = 2f;
            _kickForce = 5.8f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(-1f, 1f);
            ShellOffset = new Vec2(-18f, -1f);
            laserSight = true;
            _laserOffsetTL = new Vec2(22f, 3.5f);
            _editorName = "Gepard Lynx";
            _weight = 6f;
            Compose(
                _loseAccuracy,
                new SpeedAccuracy(this, 0f, 1f, 0f)
            );
        }

        private readonly LoseAccuracy _loseAccuracy = new LoseAccuracy(0.6f, 0.01f, 1f);

        private readonly BipodStateContainer _bipodsState = new BipodStateContainer();

        public float BipodsState
        {
            get => _bipodsState.Get(this);
            set => _bipodsState.Set(value);
        }

        public StateBinding BsBinding { get; } = new StateBinding(nameof(BipodsState));
        protected override float BaseKforce => this.BipodsDeployed() ? 0 : 5.8f;

        private void UpdateStats()
        {
            _ammoType.range = this.BipodsDeployed() ? 2400f : 1200f;
            _ammoType.bulletSpeed = this.BipodsDeployed() ? 150f : 48f;
            _fireWait = this.BipodsDeployed() ? 1.5f : 4f;
            loseAccuracy = this.BipodsDeployed() ? 0 : 0.1f;
            maxAccuracyLost = this.BipodsDeployed() ? 0 : 0.3f;
            _loseAccuracy.Drain = this.BipodsDeployed() ? .3f : .6f;
            _loseAccuracy.Regen = this.BipodsDeployed() ? .02f : .01f;
            _loseAccuracy.Max = this.BipodsDeployed() ? .5f : 1.0f;
        }

        private void UpdateFrames() =>
            FrameId = FrameId % 10 +
                      10 * (this.BipodsDeployed() ? 3 : this.BipodsFolded() ? 0 : BipodsState < 0.5f ? 1 : 2);

        public void UpdateBipodsStats(float old)
        {
            UpdateStats();
            UpdateFrames();
            this.UpdateBipodsSounds(old);
        }

        public float BipodSpeed => 1f / 8f;

        public bool Bipods
        {
            get => BipodsQ();
            set => this.SetBipods(value);
        }

        public BitBuffer BipodsBuffer
        {
            get => this.GetBipodBuffer();
            set => this.SetBipodBuffer(value);
        }

        public StateBinding BipodsBinding { get; } = new StateBinding(nameof(BipodsBuffer));
        public bool BipodsDisabled { get; private set; }
        public void SetBipodsDisabled(bool disabled) => BipodsDisabled = disabled;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}

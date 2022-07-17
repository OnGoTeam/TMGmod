using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Bolt-Action")]
    // ReSharper disable once InconsistentNaming
    public class Urbana : BaseBolt, IHaveAllowedSkins, ISwitchBipods, IDeployBipods
    {
        private const int NonSkinFrames = 4;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        [UsedImplicitly]
        public NetSoundEffect BipOff { get; } = new NetSoundEffect(Mod.GetPath<Core.TMGmod>("sounds/beepods2"));

        [UsedImplicitly] public StateBinding BipOffBinding { get; } = new NetSoundBinding(nameof(BipOff));

        [UsedImplicitly]
        public NetSoundEffect BipOn { get; } = new NetSoundEffect(Mod.GetPath<Core.TMGmod>("sounds/beepods1"));

        [UsedImplicitly] public StateBinding BipOnBinding { get; } = new NetSoundBinding(nameof(BipOn));

        public Urbana(float xval, float yval) : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            _sprite = new SpriteMap(GetPath("Urbana"), 53, 15);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(27f, 8f);
            _collisionOffset = new Vec2(-27f, -8f);
            _collisionSize = new Vec2(53f, 15f);
            _barrelOffsetTL = new Vec2(53f, 5f);
            _flare = new SpriteMap(GetPath("FlareOnePixel3"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            ammo = 6;
            _fireSound = GetPath("sounds/RifleOrMG.wav");
            _fullAuto = false;
            _kickForce = 5.25f;
            laserSight = false;
            _laserOffsetTL = new Vec2(31f, 9f);
            _holdOffset = new Vec2(9f, 1f);
            _editorName = "Urbana";
            _weight = 5.6f;
            ShellOffset = new Vec2(-9f, -2f);
            _ammoType = new ATBoltAction();
        }

        protected override void OnInitialize()
        {
            _ammoType.bulletSpeed = 75f;
            _ammoType.range = 1200f;
            _ammoType.penetration = 2f;
            base.OnInitialize();
        }

        private readonly BipodStateContainer _bipodsState = new BipodStateContainer();

        [UsedImplicitly]
        public float BipodsState
        {
            get => _bipodsState.Get(this);
            set => _bipodsState.Set(value);
        }

        [UsedImplicitly] public StateBinding BsBinding { get; } = new StateBinding(nameof(BipodsState));
        protected override float GetBaseKforce() => this.BipodsDeployed() ? 0 : 5f;

        private void UpdateStats()
        {
            _ammoType.range = this.BipodsDeployed() ? 1800f : 1200f;
            _ammoType.bulletSpeed = this.BipodsDeployed() ? 150f : 75f;
        }

        private void UpdateFrames() =>
            FrameId = FrameId % 10 + 10 * (this.BipodsDeployed() ? 3 : this.BipodsFolded() ? 0 : BipodsState < .5f ? 1 : 2);

        public void UpdateStats(float old)
        {
            UpdateStats();
            UpdateFrames();
            this.UpdateBipodsSounds(old);
        }

        public float BipodSpeed => 1f / 20f;

        public bool Bipods
        {
            get => BipodsQ();
            set => this.SetBipods(value);
        }

        [UsedImplicitly]
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

        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public bool SwitchingBipods() => (FrameId + 10) % (10 * NonSkinFrames) >= 20;

        protected override bool HasLaser() => false;
        protected override float MaxAngle() => Bipods ? .05f : .15f;
        protected override float MaxOffset() => 4.0f;
        protected override float ReloadSpeed() => Bipods? 1f : .75f;
    }
}

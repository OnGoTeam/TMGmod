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
    public class AWS : BaseBolt, IHaveAllowedSkins, I5, IHaveBipodState, ICanDisableBipods, ISwitchBipods
    {
        // Amazon Web Services

        private const int NonSkinFrames = 3;
        public ICollection<int> AllowedSkins => new List<int>(new[] { 0, 2, 4, 5, 6, 7, 8, 9 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        private float _bipodsstate;

        [UsedImplicitly] public NetSoundEffect BipOff = new NetSoundEffect(Mod.GetPath<Core.TMGmod>("sounds/beepods2"));

        [UsedImplicitly] public StateBinding BipOffBinding = new NetSoundBinding(nameof(BipOff));

        [UsedImplicitly] public NetSoundEffect BipOn = new NetSoundEffect(Mod.GetPath<Core.TMGmod>("sounds/beepods1"));

        [UsedImplicitly] public StateBinding BipOnBinding = new NetSoundBinding(nameof(BipOn));

        public AWS(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            _sprite = new SpriteMap(GetPath("AWS"), 33, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(17f, 6f);
            _collisionOffset = new Vec2(-17f, -6f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 4f);
            ammo = 6;
            _ammoType = new AT50SniperS
            {
                range = 550f,
                accuracy = 0.97f
            };
            _flare = new SpriteMap(GetPath("FlareSilencer"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _fireSound = GetPath("sounds/Silenced1.wav");
            _fullAuto = false;
            _kickForce = 3.8f;
            _holdOffset = new Vec2(2f, 1f);
            _editorName = "AWS";
            _weight = 5f;
            _laserOffsetTL = new Vec2(18f, 3f);
            ShellOffset = new Vec2(-3f, -2f);
        }

        [UsedImplicitly]
        public float BipodsState
        {
            get => duck != null ? _bipodsstate : 0;
            set => _bipodsstate = Maths.Clamp(value, 0f, 1f);
        }

        [UsedImplicitly] public StateBinding BsBinding { get; } = new StateBinding(nameof(BipodsState));

        public void UpdateStats(float old)
        {
            var nobipods = BipodsState < 0.01f;
            var bipods = BipodsState > 0.99f;
            _kickForce = bipods ? 0 : 4.75f;
            _ammoType.range = bipods ? 1100f : 550f;
            _ammoType.bulletSpeed = bipods ? 150f : 37f;
            _ammoType.accuracy = bipods ? 1f : 0.97f;
            FrameId = FrameId % 10 + 10 * (bipods ? 2 : nobipods ? 0 : 1);
            if (isServerForObject && bipods && old <= 0.99f)
                BipOn.Play();
            if (isServerForObject && nobipods && old >= 0.01f)
                BipOff.Play();
        }

        public float BipodSpeed => 1f / 7;

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

        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public bool SwitchingBipods() => (FrameId + 10) % (10 * NonSkinFrames) >= 20;

        protected override bool HasLaser() => false;
        protected override float MaxAngle() => Bipods ? .05f : .25f;

        protected override float MaxOffset() => 3.0f;
        protected override float ReloadSpeed() => Bipods ? 2.0f : 1.5f;
    }
}

using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;


namespace TMGmod
{
    /// <inheritdoc cref="BaseGun"/>
    /// <inheritdoc cref="IHaveSkin"/>
    /// <inheritdoc cref="IAmLmg"/>
    [EditorGroup("TMG|LMG")]
    // ReSharper disable once InconsistentNaming
    public class MG44C : BaseGun, IHaveSkin, IAmLmg, IHaveBipods
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        [UsedImplicitly]
        public NetSoundEffect BipOn = new NetSoundEffect(Mod.GetPath<Core.TMGmod>("sounds/beepods1"));
        [UsedImplicitly]
        public NetSoundEffect BipOff = new NetSoundEffect(Mod.GetPath<Core.TMGmod>("sounds/beepods2"));
        [UsedImplicitly]
        public StateBinding BipOnBinding = new NetSoundBinding(nameof(BipOn));
        [UsedImplicitly]
        public StateBinding BipOffBinding = new NetSoundBinding(nameof(BipOff));
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        public float RandomaticKickforce;
        [UsedImplicitly]
        public StateBinding RandomaticKickforceBinding { get; } = new StateBinding(nameof(RandomaticKickforce));
        /// <inheritdoc />
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 3, 6, 7 });

        /// <inheritdoc />
        public MG44C(float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 30;
            _ammoType = new ATMagnum
            {
                range = 750f,
                accuracy = 0.75f,
                penetration = 1.5f
            };
            BaseAccuracy = 0.75f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("MG44 Mark2T"), 39, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(20f, 6f);
            _collisionOffset = new Vec2(-20f, -6f);
            _collisionSize = new Vec2(39f, 11f);
            _barrelOffsetTL = new Vec2(39f, 3f);
            _flare = new SpriteMap(GetPath("FlareMG44"), 13, 10)
            {
                center = new Vec2(1.0f, 6f)
            };
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.9f;
            _kickForce = 1.8f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.6f;
            _holdOffset = new Vec2(5f, 1f);
            ShellOffset = new Vec2(-5f, -3f);
            _editorName = "MG44 Mark2T";
            laserSight = true;
            _laserOffsetTL = new Vec2(29f, 1f);
            _weight = 6f;
        }
        public override void Update()
        {
            base.Update();
            Bipods = Bipods;
            RandomaticKickforce = Rando.Float(0.9f, 1.5f);
        }
        public bool Bipods
        {
            get => HandleQ();
            set
            {
                _kickForce = value ? RandomaticKickforce : 1.8f;
                loseAccuracy = value ? 0f : 0.1f;
                maxAccuracyLost = value ? 0f : 0.3f;
            }
        }
        [UsedImplicitly]
        public BitBuffer BipodsBuffer
        {
            get
            {
                var b = new BitBuffer();
                b.Write(Bipods);
                return b;
            }
            set => Bipods = value.ReadBool();
        }

        public StateBinding BipodsBinding { get; } = new StateBinding(nameof(BipodsBuffer));
        public bool BipodsDisabled { get; private set; }

        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic))
            {
                bublic = Rando.Int(0, 9);
            }
            _sprite.frame = bublic;
        }

        /// <inheritdoc />
        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        /// <inheritdoc />
        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|LMG")]
    // ReSharper disable once InconsistentNaming
    public class MG3 : BaseGun, IAmLmg, IHaveAllowedSkins, I5, IHaveBipods
    {
        private const int NonSkinFrames = 6;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 5 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        private float _bipodsstate;

        [UsedImplicitly] public NetSoundEffect BipOff = new NetSoundEffect(Mod.GetPath<Core.TMGmod>("sounds/beepods2"));

        [UsedImplicitly] public StateBinding BipOffBinding = new NetSoundBinding(nameof(BipOff));

        [UsedImplicitly] public NetSoundEffect BipOn = new NetSoundEffect(Mod.GetPath<Core.TMGmod>("sounds/beepods1"));

        [UsedImplicitly] public StateBinding BipOnBinding = new NetSoundBinding(nameof(BipOn));

        public MG3(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 80;
            _ammoType = new AT556NATO
            {
                range = 480f,
                accuracy = 0.8f,
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("mg3"), 39, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
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
        }

        [UsedImplicitly]
        public float BipodsState
        {
            get => duck != null ? _bipodsstate : 0;
            set => _bipodsstate = Maths.Clamp(value, 0f, 1f);
        }

        [UsedImplicitly] public StateBinding BsBinding { get; } = new StateBinding(nameof(BipodsState));

        public bool Bipods
        {
            get => BipodsQ();
            set
            {
                var bipodsstate = BipodsState;
                if (isServerForObject)
                    BipodsState += 1f / 15 * (value ? 1 : -1);
                var nobipods = BipodsState < 0.01f;
                var bipods = BipodsState > 0.99f;
                _ammoType.range = bipods ? 550f : 480f;
                _ammoType.bulletSpeed = bipods ? 40f : 28f;
                _kickForce = bipods ? 0 : 2.5f;
                loseAccuracy = bipods ? 0 : 0.1f;
                maxAccuracyLost = bipods ? 0 : 0.25f;
                FrameId = FrameId % 20 + 20 * (bipods ? 2 : nobipods ? 0 : 1);
                if (isServerForObject && bipods && bipodsstate <= 0.99f)
                    BipOn.Play();
                if (isServerForObject && nobipods && bipodsstate >= 0.01f)
                    BipOff.Play();
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
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void Update()
        {
            base.Update();
            Bipods = Bipods;
            if (ammo == 0 && FrameId % 20 >= 0 && FrameId % 20 < 10) FrameId += 10;
            if (duck == null) BipodsDisabled = false;
            else if (!BipodsQ(true)) BipodsDisabled = false;
            else if (duck.inputProfile.Pressed("QUACK")) BipodsDisabled = !BipodsDisabled;
        }

        public override void Fire()
        {
            if ((FrameId + 20) % (10 * NonSkinFrames) >= 40) return;
            base.Fire();
        }
    }
}

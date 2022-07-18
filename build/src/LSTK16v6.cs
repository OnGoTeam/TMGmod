#if DEBUG
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.BipodsLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|NOTRELEASEDYET")]
    [UsedImplicitly]
    public class Lstk16V6 : BaseGun, IHaveAllowedSkins, IHaveBipods
    {
        private const int NonSkinFrames = 9;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        private float _bipodsstate;

        [UsedImplicitly] public NetSoundEffect BipOff = new NetSoundEffect(Mod.GetPath<Core.TMGmod>("sounds/beepods2"));

        [UsedImplicitly] public StateBinding BipOffBinding = new NetSoundBinding(nameof(BipOff));

        [UsedImplicitly] public NetSoundEffect BipOn = new NetSoundEffect(Mod.GetPath<Core.TMGmod>("sounds/beepods1"));

        [UsedImplicitly] public StateBinding BipOnBinding = new NetSoundBinding(nameof(BipOn));

        public Lstk16V6(float xval, float yval) : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 25;
            _ammoType = new ATM16();
            MaxAccuracy = 0.91f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("LSTK16v6"), 33, 14);
            _graphic = _sprite;
            _sprite.frame = 0;
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
            _editorName = "LSTK-16v6";
            _weight = 6.7f;
        }


        [UsedImplicitly]
        public float BipodsState
        {
            get => duck != null ? _bipodsstate : 0;
            set => _bipodsstate = Maths.Clamp(value, 0f, 1f);
        }

        [UsedImplicitly] public StateBinding BsBinding { get; } = new StateBinding(nameof(BipodsState));

        //[UsedImplicitly]
        //private uint _cdstate;
        [UsedImplicitly]
        public bool Bipods
        {
            get => BipodsQ();
            set
            {
                if (isServerForObject)
                    BipodsState += 1f / 10 * (value ? 1 : -1);
                var nobipods = BipodsState < 0.01f;
                var bipods = BipodsState > 0.99f;
                _kickForce = value ? 0 : 5.5f;
                FrameId = FrameId % 10 + 30 * (bipods ? 2 : nobipods ? 0 : 1);
            }
        }

        public BitBuffer BipodsBuffer { get; set; }

        public StateBinding BipodsBinding { get; } = new StateBinding(nameof(Bipods));

        public bool BipodsDisabled { get; private set; }
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        [UsedImplicitly] public EditorProperty<int> Skin => skin;

        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void Update()
        {
            base.Update();
            Bipods = Bipods;
            if (duck == null) BipodsDisabled = false;
            else if (!BipodsQ(true)) BipodsDisabled = false;
            else if (duck.inputProfile.Pressed("QUACK")) BipodsDisabled = !BipodsDisabled;
        }

        public override void Fire()
        {
            if (29 < FrameId && FrameId < 60) return;
            base.Fire();
        }
    }
}
#endif
using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.WClasses;
using TMGmod.Core;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Semi-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class Lynx : BaseGun, IAmDmr, ISpeedAccuracy, IHaveSkin, I5, IHaveBipods
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 3;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 3, 5 });
        public Lynx (float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 6;
            _ammoType = new ATSniper
            {
                range = 1200f,
                accuracy = 1f,
                penetration = 1f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Lynx"), 31, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(14f, 5.5f);
            _collisionOffset = new Vec2(-14.5f, -5.5f);
            _collisionSize = new Vec2(31f, 11f);
            _barrelOffsetTL = new Vec2(31f, 4f);
            _flare = new SpriteMap(GetPath("FlareOnePixel3"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _fireSound = GetPath("sounds/HeavySniper.wav");
            _fullAuto = false;
            _fireWait = 4f;
            _kickForce = 5.8f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(-1f, 1f);
            ShellOffset = new Vec2(-18f, -1f);
            laserSight = true;
            _laserOffsetTL = new Vec2(22f, 3.5f);
            _editorName = "Gepard Lynx";
			_weight = 6f;
        }
        public bool Bipods
        {
            get => BipodsQ();
            set
            {
                var bipodsstate = BipodsState;
                if (isServerForObject)
                    BipodsState += 1f / 10 * (value ? 1 : -1);
                var nobipods = BipodsState < 0.01f;
                var bipods = BipodsState > 0.99f;
                _ammoType.range = bipods ? 2400f : 1200f;
                _ammoType.bulletSpeed = bipods ? 150f : 48f;
                _fireWait = bipods ? 1.5f : 4f;
                _kickForce = bipods ? 0 : 5.8f;
                loseAccuracy = bipods ? 0 : 0.1f;
                maxAccuracyLost = bipods ? 0 : 0.3f;
                FrameId = FrameId % 10 + 10 * (bipods ? 2 : nobipods ? 0 : 1);
                if (isServerForObject && bipods && bipodsstate <= 0.99f)
                    SFX.Play(GetPath("sounds/beepods1"));
                if (isServerForObject && nobipods && bipodsstate >= 0.01f)
                    SFX.Play(GetPath("sounds/beepods2"));
            }
        }
        public override void Update()
        {
            Bipods = Bipods;
            if (duck == null) BipodsDisabled = false;
            else if (!BipodsQ(this, true)) BipodsDisabled = false;
            else if (duck.inputProfile.Pressed("QUACK")) BipodsDisabled = !BipodsDisabled;
            base.Update();
        }
        public override void UpdateOnFire()
        {
            loseAccuracy += 0.15f;
            base.UpdateOnFire();
        }
        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic))
            {
                bublic = Rando.Int(0, 9);
            }
            _sprite.frame = bublic;
        }

        public override void Fire()
        {
            if (FrameId / 10 == 1) return;
            base.Fire();
        }
        [UsedImplicitly]
        public float BipodsState
        {
            get => duck != null ? _bipodsstate : 0;
            set
            {
                value = Math.Max(value, 0f);
                value = Math.Min(value, 1f);
                _bipodsstate = value;
            }
        }

        private float _bipodsstate;
        public StateBinding BipodsBinding => new StateBinding(nameof(Bipods));
        public bool BipodsDisabled { get; private set; }

        [UsedImplicitly]
        public StateBinding BsBinding => new StateBinding(nameof(BipodsState));

        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }

        public float MuAccuracySr => 0;
        public float LambdaAccuracySr => 0;
    }
}
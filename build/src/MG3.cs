using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|LMG")]
    // ReSharper disable once InconsistentNaming
    public class MG3 : BaseGun, IAmLmg, IHaveSkin, I5, IHaveBipods
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 6;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 5 });
		
		public MG3 (float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 80;
            _ammoType = new AT9mm
            {
                range = 480f,
                accuracy = 0.8f,
                penetration = 1.5f
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
            _kickForce = 1.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.25f;
            _holdOffset = new Vec2(4f, 1.5f);
            ShellOffset = new Vec2(-2f, -3f);
            _editorName = "MG3";
			_weight = 7f;
        }
        public override void Update()
        {
            base.Update();
            Bipods = Bipods;
            if (ammo == 0 && FrameId % 20 >= 0 && FrameId % 20 < 10) FrameId += 10;
            if (duck == null) BipodsDisabled = false;
            else if (!BipodsQ(this, true)) BipodsDisabled = false;
            else if (duck.inputProfile.Pressed("QUACK")) BipodsDisabled = !BipodsDisabled;
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

        public override void Fire()
        {
            if (FrameId / 20 == 1) return;
            base.Fire();
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
                _kickForce = bipods ? 0 : 3.5f;
                loseAccuracy = bipods ? 0 : 0.1f;
                maxAccuracyLost = bipods ? 0 : 0.45f;
                FrameId = FrameId % 20 + 20 * (bipods ? 2 : nobipods ? 0 : 1);
                if (isServerForObject && bipods && bipodsstate <= 0.99f)
                    SFX.Play(GetPath("sounds/beepods1"));
                if (isServerForObject && nobipods && bipodsstate >= 0.01f)
                    SFX.Play(GetPath("sounds/beepods2"));
            }
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
    }
}
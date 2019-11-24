using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class VSK94 : BaseAr, IHaveSkin, IHaveBipods
    {
        [UsedImplicitly]
        public float HandAngleOff
        {
            get => handAngle * offDir;
            set => handAngle = value * offDir;
        }
        private float _handAngleOff;
        [UsedImplicitly]
        public StateBinding HandAngleOffBinding = new StateBinding(nameof(HandAngleOff));

        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        [UsedImplicitly]
        public float Psevdotimer;

        private float _floatingKickforce;
        [UsedImplicitly]
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 1, 7, 8 });

        public VSK94(float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 11;
            _ammoType = new AT9mm
            {
                range = 560f,
                accuracy = 0.95f,
                bulletSpeed = 55f,
                penetration = 2f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Anyx SR2 Compact"), 32, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(11f, 5f);
            _collisionOffset = -_center;
            _collisionSize = new Vec2(32f, 10f);
            _barrelOffsetTL = new Vec2(32f, 3f);
            _holdOffset = new Vec2(2f, 1f);
            ShellOffset = new Vec2(-12f, -3f);
            _fireSound = GetPath("sounds/HeavySniper.wav");
            _flare = new SpriteMap(GetPath("FlareOnePixel3"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _fullAuto = true;
            _fireWait = 0.85f;
            _kickForce = 5.2f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.6f;
            _editorName = "Anyx SR2 Compact";
            _weight = 5.5f;
            MinAccuracy = 0f;
            BaseAccuracy = 0.9f;
            Kforce1Ar = 5.4f;
            Kforce2Ar = 6.85f;
        }
        public override void Update()
        {
            HandAngleOff = _handAngleOff;
            base.Update();
            Bipods = Bipods;
            _floatingKickforce = Psevdotimer < 16f ? 0.5f : 3f;
        }
        public bool Bipods
        {
            get => HandleQ();
            set
            {
                Kforce1Ar = value ? _floatingKickforce : 5.4f;
                Kforce2Ar = value ? _floatingKickforce : 6.85f;
                _kickForce = value ? _floatingKickforce : 5.2f;
                loseAccuracy = value ? 0f : 0.2f;
                maxAccuracyLost = value ? 0f : 0.6f;
            }
        }
        public bool BipodsDisabled => false;
        public StateBinding BipodsBinding => new StateBinding(nameof(Bipods));
        public override void OnHoldAction()
        {
            if (ammo > 0) HandAngleOff -= 0.01f;
            else if (ammo < 1) HandAngleOff = 0f;
            _handAngleOff = HandAngleOff;
            Psevdotimer += 1f;
            base.OnHoldAction();
        }
        public override void OnReleaseAction()
        {
            Psevdotimer = 0f;
            HandAngleOff = 0f;
            _handAngleOff = HandAngleOff;
            base.OnReleaseAction();
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
        [UsedImplicitly]
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
    }
}
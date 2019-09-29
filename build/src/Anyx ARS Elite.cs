using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    [PublicAPI]
    public class Vixr : BaseGun, IAmAr, IHaveSkin
    {
        public float HandAngleOff
        {
            get => handAngle * offDir;
            set => handAngle = value * offDir;
        }

        private float _handAngleOff;
        public StateBinding HandAngleOffBinding = new StateBinding(nameof(HandAngleOff));
        public bool Stockngrip = true;
        public StateBinding StockBinding = new StateBinding(nameof(Stockngrip));
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 6 });

        public Vixr(float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 21;
            _ammoType = new AT9mmS
            {
               range = 350f,
               accuracy = 0.81f,
               bulletSpeed = 40f,
               penetration = 1f
            };
            BaseAccuracy = 0.81f;
            _type = "gun";
            //THIS FILE HAS REBORN TREE TIMES SQUARES!! send this massage to your friends or not to friends
            _sprite = new SpriteMap(GetPath("Anyx ARS Elite"), 33, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(17f, 5f);
            _collisionOffset = -_center;
            _collisionSize = new Vec2(33f, 9f);
            _barrelOffsetTL = new Vec2(28f, 2f);
            _holdOffset = new Vec2(4f, 2f);
            ShellOffset = new Vec2(0f, 0f);
            _fireSound = GetPath("sounds/Silenced1.wav");
            _flare = new SpriteMap(GetPath("FlareAnyxARS"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _fullAuto = true;
            _fireWait = 0.75f;
            _kickForce = 4.6f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.4f;
            _editorName = "Anyx ARS Elite";
			_weight = 6f;
            handAngle = 0f;
        }
        public override void Update()
        {
            HandAngleOff = _handAngleOff;
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (Stockngrip)
                {
                    _sprite.frame += 10;
                    loseAccuracy = 0.3f;
                    maxAccuracyLost = 0.6f;
                    _fireWait = 0.5f;
                    Stockngrip = false;
                    weight = 3f;
                }
                else
                {
                    _sprite.frame -= 10;
                    loseAccuracy = 0.2f;
                    maxAccuracyLost = 0.4f;
                    _fireWait = 0.75f;
                    Stockngrip = true;
                    weight = 6f;
                }
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }
            base.Update();
        }
        public override void OnHoldAction()
        {
            if (ammo > 0) HandAngleOff -= 0.01f;
            else if (ammo < 1) HandAngleOff = 0f;
            _handAngleOff = HandAngleOff;
            base.OnHoldAction();
        }
        public override void OnReleaseAction()
        {
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
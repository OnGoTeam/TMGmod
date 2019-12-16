using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    [UsedImplicitly]
    public class Vixr : BaseGun, IAmAr, IHaveSkin, IHaveStock
    {
        [UsedImplicitly]
        public float HandAngleOff
        {
            get => handAngle * offDir;
            set => handAngle = value * offDir;
        }

        [UsedImplicitly]
        public float HandAngleOffState;
        [UsedImplicitly]
        public StateBinding HandAngleOffStateBinding = new StateBinding(nameof(HandAngleOffState));
        [UsedImplicitly]
        public StateBinding HandAngleOffBinding = new StateBinding(nameof(HandAngleOff));
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 3;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 6, 8 });

        private bool _stock = true;
        [UsedImplicitly]
        public bool Stock
        {
            get => _stock;
            set
            {
                _stock = value;
                var stockstate = StockState;
                if (isServerForObject)
                    StockState += 1f / 10 * (value ? 1 : -1);
                var nostock = StockState < 0.01f;
                var stock = StockState > 0.99f;
                _fireWait = stock ? 0.75f : 0.6f;
                loseAccuracy = stock ? 0.15f : 0.2f;
                maxAccuracyLost = stock ? 0.3f : 0.6f;
                weight = stock ? 6f : 3.5f;
                _kickForce = stock ? 3f : 4.6f;
                FrameId = FrameId % 10 + 10 * (stock ? 0 : nostock ? 2 : 1);
                if (isServerForObject && stock && stockstate <= 0.99f)
                    SFX.Play(GetPath("sounds/beepods1"));
                if (isServerForObject && nostock && stockstate >= 0.01f)
                    SFX.Play(GetPath("sounds/beepods2"));
            }
        }

        private float _stockstate = 1f;
        public float StockState
        {
            get => _stockstate;
            set
            {
                value = Math.Max(value, 0f);
                value = Math.Min(value, 1f);
                _stockstate = value;
            }
        }
        public StateBinding StockStateBinding { get; } = new StateBinding(nameof(StockState));

        [UsedImplicitly]
        public StateBinding StockBinding { get; } = new StateBinding(nameof(StockBuffer));

        public BitBuffer StockBuffer
        {
            get
            {
                var b = new BitBuffer();
                b.Write(Stock);
                return b;
            }
            set => Stock = value.ReadBool();
        }

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
            ShellOffset = new Vec2(-13f, -2f);
            _fireSound = GetPath("sounds/RifleOrMG.wav");
            _flare = new SpriteMap(GetPath("FlareAnyxARS"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _fullAuto = true;
            _fireWait = 0.75f;
            _kickForce = 3f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.4f;
            _editorName = "Anyx ARS Elite";
			_weight = 6f;
            handAngle = 0f;
        }
        public override void Update()
        {
            HandAngleOff = HandAngleOffState;
            base.Update();
            if (SwitchStockQ() && (Stock || duck.grounded) && duck.inputProfile.Pressed("QUACK"))
            {
                Stock = !Stock;
                SFX.Play("quack", -1);
            }
            else if (duck != null)
                Stock = Stock;
        }
        public override void OnHoldAction()
        {
            if (ammo > 0) HandAngleOff -= 0.01f;
            else if (ammo < 1) HandAngleOff = 0f;
            HandAngleOffState = HandAngleOff;
            base.OnHoldAction();
        }
        public override void OnReleaseAction()
        {
            HandAngleOff = 0f;
            HandAngleOffState = HandAngleOff;
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

        public override void Fire()
        {
            if (FrameId / 10 == 1) return;
            base.Fire();
        }
    }
}
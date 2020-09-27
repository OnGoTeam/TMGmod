using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;
using TMGmod.Core.AmmoTypes;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|PDW")]
    public class DaewooK1 : BaseSmg, IHaveSkin, IHaveStock
    {
        private readonly SpriteMap _sprite;
        [UsedImplicitly]
        private const int NonSkinFrames = 3;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 1, 2, 3, 4, 7 });


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
                _fireWait = stock ? 0.86f : 0.5f;
                loseAccuracy = stock ? 0.1f : 0.2f;
                maxAccuracyLost = stock ? 0.24f : 0.4f;
                weight = stock ? 4.5f : 3f;
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

        public DaewooK1 (float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 32;
            _ammoType = new ATDaewooK1();
            _type = "gun";
            _sprite = new SpriteMap(GetPath("DaewooK1"), 28, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(14f, 5f);
            _collisionOffset = new Vec2(-14f, -5f);
            _collisionSize = new Vec2(28f, 11f);
            _barrelOffsetTL = new Vec2(28f, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _holdOffset = new Vec2(-2f, 1f);
            ShellOffset = new Vec2(0f, 0f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 0.86f;
            _kickForce = 0.5f;
            KforceDSmg = 2.5f;
            MaxDelaySmg = 50;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.24f;
            _editorName = "Daewoo K1";
			_weight = 4.5f;
        }
        public override void Update()
        {
            base.Update();
            if (SwitchStockQ() && (Stock || duck.grounded) && duck.inputProfile.Pressed("QUACK"))
            {
                Stock = !Stock;
                SFX.Play("quack", -1);
            }
            else if (duck != null)
                Stock = Stock;
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
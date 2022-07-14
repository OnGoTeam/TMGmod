using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Fully-Automatic")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class PP19 : BaseGun, IFirstPrecise, IHaveSkin, IHaveStock
    {
        private const int NonSkinFrames = 3;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 1, 8 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        private bool _stock = true;

        private float _stockstate = 1f;

        public PP19(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 64;
            _ammoType = new ATBizon();
            BaseAccuracy = 0.8f;
            LowerAccuracyFp = 0.6f;
            MinAccuracy = 0.2f;
            MaxDelayFp = 25;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("PP19Bizon"), 28, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(14f, 5f);
            _collisionOffset = new Vec2(-14f, -5f);
            _collisionSize = new Vec2(28f, 9f);
            _barrelOffsetTL = new Vec2(28f, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _fireSound = GetPath("sounds/2.wav");
            _fullAuto = true;
            _fireWait = 0.75f;
            _kickForce = 1.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.35f;
            _holdOffset = new Vec2(2f, 1f);
            handOffset = new Vec2(2f, 0f);
            ShellOffset = new Vec2(-1f, -2f);
            _editorName = "PP-19 Bizon";
            _weight = 1.5f;
        }

        public int CurrentDelayFp { get; set; }
        public int MaxDelayFp { get; }
        public float LowerAccuracyFp { get; }
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

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
                _fireWait = stock ? 0.75f : 0.5f;
                loseAccuracy = stock ? 0.15f : 0.25f;
                maxAccuracyLost = stock ? 0.35f : 0.7f;
                weight = stock ? 1.5f : 1f;
                FrameId = FrameId % 10 + 10 * (stock ? 0 : nostock ? 2 : 1);
                if (isServerForObject && stock && stockstate <= 0.99f)
                    SFX.Play(GetPath("sounds/tuduc"));
                if (isServerForObject && nostock && stockstate >= 0.01f)
                    SFX.Play(GetPath("sounds/tuduc"));
            }
        }

        public float StockState
        {
            get => _stockstate;
            set => _stockstate = Maths.Clamp(value, 0f, 1f);
        }

        public StateBinding StockStateBinding { get; } = new StateBinding(nameof(StockState));

        [UsedImplicitly] public StateBinding StockBinding { get; } = new StateBinding(nameof(StockBuffer));

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

        public override void Update()
        {
            base.Update();
            if (SwitchStockQ() && (Stock || duck.grounded) && duck.inputProfile.Pressed("QUACK"))
            {
                Stock = !Stock;
                SFX.Play("quack", -1);
            }
            else if (duck != null)
            {
                Stock = Stock;
            }
        }

        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic)) bublic = Rando.Int(0, 9);
            _sprite.frame = bublic;
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

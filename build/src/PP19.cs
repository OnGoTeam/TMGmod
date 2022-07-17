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
    public class PP19 : BaseGun, IFirstPrecise, IHaveAllowedSkins, IHaveStock
    {
        private const int NonSkinFrames = 3;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 8 });
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
            MaxAccuracy = 0.8f;
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
                center = new Vec2(0.0f, 5f),
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

        public float StockSpeed => 1f / 10f;

        public bool Stock
        {
            get => _stock;
            set
            {
                _stock = value;
                this.SetStock(value);
            }
        }

        public float StockState
        {
            get => _stockstate;
            set => _stockstate = Maths.Clamp(value, 0f, 1f);
        }

        private void UpdateStats()
        {
            _fireWait = this.StockDeployed() ? 0.75f : 0.5f;
            loseAccuracy = this.StockDeployed() ? 0.15f : 0.25f;
            maxAccuracyLost = this.StockDeployed() ? 0.35f : 0.7f;
            _weight = this.StockDeployed() ? 1.5f : 1f;
        }

        private void UpdateFrames() =>
            FrameId = FrameId % 10 + 10 * (this.StockDeployed() ? 0 : this.StockFolded() ? 2 : 1);

        public void UpdateStockStats(float old)
        {
            UpdateStats();
            UpdateFrames();
            this.UpdateStockSounds(old);
        }

        public StateBinding StockStateBinding { get; } = new StateBinding(nameof(StockState));

        [UsedImplicitly] public StateBinding StockBinding { get; } = new StateBinding(nameof(StockBuffer));

        public BitBuffer StockBuffer
        {
            get => this.GetStockBuffer();
            set => this.SetStockBuffer(value);
        }

        public string StockOn => Mod.GetPath<Core.TMGmod>("sounds/tuduc");
        public string StockOff => Mod.GetPath<Core.TMGmod>("sounds/tuduc");
    }
}

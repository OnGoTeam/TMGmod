using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|PDW")]
    public class DaewooK1 : BaseSmg, IHaveAllowedSkins, IHaveStock
    {
        [UsedImplicitly] private const int NonSkinFrames = 3;

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 3, 4, 7 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;


        private bool _stock = true;

        private float _stockstate = 1f;

        public DaewooK1(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 32;
            _ammoType = new ATDaewooK1();
            MaxAccuracy = _ammoType.accuracy;
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
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(-2f, 1f);
            ShellOffset = new Vec2(0f, 0f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 0.86f;
            _kickForce = 0.5f;
            KickForceDeltaSmg = 2.5f;
            MaxDelaySmg = 50;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.24f;
            _editorName = "Daewoo K1";
            _weight = 4.5f;
        }

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

        [UsedImplicitly]
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
            _fireWait = this.StockDeployed() ? 0.86f : 0.75f;
            loseAccuracy = this.StockDeployed() ? 0.1f : 0.2f;
            maxAccuracyLost = this.StockDeployed() ? 0.24f : 0.4f;
            _weight = this.StockDeployed() ? 4.5f : 3f;
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

        public StateBinding StockBinding { get; } = new StateBinding(nameof(StockBuffer));

        public BitBuffer StockBuffer
        {
            get => this.GetStockBuffer();
            set => this.SetStockBuffer(value);
        }

        public string StockOn => Mod.GetPath<Core.TMGmod>("sounds/beepods1");
        public string StockOff => Mod.GetPath<Core.TMGmod>("sounds/beepods2");
    }
}

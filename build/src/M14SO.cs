#if FEATURES_1_2
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.StockLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class M14SO : BaseDmr, IHaveAllowedSkins, IHaveStock
    {
        private bool _stock = true;

        private float _stockstate = 1f;

        public M14SO(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 18;
            SetAmmoType<ATM14>(.9f);
            MinAccuracy = 0.3f;
            RegenAccuracyDmr = 0.01f;
            DrainAccuracyDmr = 0.12f;
            NonSkinFrames = 3;
            Smap = new SpriteMap(GetPath("Sawed-Off M14"), 31, 11);
            _center = new Vec2(16f, 6f);
            _collisionOffset = new Vec2(-16f, -6f);
            _collisionSize = new Vec2(31f, 11f);
            _barrelOffsetTL = new Vec2(31f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 1.25f;
            _kickForce = 2.5f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            _holdOffset = new Vec2(0f, 1f);
            ShellOffset = new Vec2(-9f, -4f);
            _editorName = "M14 Sawed-Off";
            _weight = 2.5f;
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 333f;
            base.OnInitialize();
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        public float StockSpeed => 1f / 17f;

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

        private void UpdateStats()
        {
            _fireWait = this.StockDeployed() ? 1.25f : 1f;
            loseAccuracy = this.StockDeployed() ? 0f : 0.2f;
            maxAccuracyLost = this.StockDeployed() ? 0f : 0.25f;
            _weight = this.StockDeployed() ? 2.5f : 2f;
        }

        private void UpdateFrames()
        {
            NonSkin = this.StockDeployed() ? 0 : this.StockFolded() ? 2 : 1;
        }
    }
}
#endif

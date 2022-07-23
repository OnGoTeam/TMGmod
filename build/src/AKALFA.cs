using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.StockLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class AKALFA : BaseAr, IHaveAllowedSkins, IHaveStock
    {
        private bool _stock = true;

        private float _stockstate = 1f;

        public AKALFA(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 20;
            SetAmmoType<AT545NATO>();
            NonSkinFrames = 3;
            Smap = new SpriteMap(GetPath("ALFA"), 38, 9);
            _center = new Vec2(19f, 5f);
            _collisionOffset = new Vec2(-19f, -5f);
            _collisionSize = new Vec2(38f, 9f);
            _barrelOffsetTL = new Vec2(38f, 2f);
            _holdOffset = new Vec2(5f, 1f);
            ShellOffset = new Vec2(-3f, -2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.75f;
            _kickForce = 0.05f;
            KforceDelta = 0.70f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0.25f;
            _editorName = "Alfa";
            _weight = 5.5f;
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 360f;
            _ammoType.bulletSpeed = 60f;
            _ammoType.bulletThickness = .87f;
            base.OnInitialize();
        }

        protected override float BaseKforce => this.StockDeployed() ? 0.65f : 1.2f;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 4, 5 });

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

        public string StockOn => Mod.GetPath<Core.TMGmod>("sounds/tuduc");
        public string StockOff => Mod.GetPath<Core.TMGmod>("sounds/tuduc");

        private void UpdateStats()
        {
            loseAccuracy = this.StockDeployed() ? 0f : 0.1f;
            _weight = this.StockDeployed() ? 5.5f : 3.5f;
        }

        private void UpdateFrames()
        {
            NonSkin = this.StockDeployed() ? 0 : this.StockFolded() ? 2 : 1;
        }
    }
}

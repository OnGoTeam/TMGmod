using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.StockLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Fully-Automatic")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class PP19 : BaseGun, IHaveAllowedSkins, IHaveStock
    {
        private bool _stock = true;

        private float _stockstate = 1f;

        public PP19(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "PP-19 Bizon";
            ammo = 64;
            SetAmmoType<ATBizon>(.8f);
            ComposeFirstAccuracy(25);
            MinAccuracy = 0.2f;
            NonSkinFrames = 3;
            Smap = new SpriteMap(GetPath("PP19Bizon"), 28, 9);
            _center = new Vec2(14f, 5f);
            _collisionOffset = new Vec2(-14f, -5f);
            _collisionSize = new Vec2(28f, 9f);
            _barrelOffsetTL = new Vec2(28f, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = GetPath("sounds/new/SMG-2.wav");
            _fullAuto = true;
            _fireWait = 0.75f;
            _kickForce = 1.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.35f;
            _holdOffset = new Vec2(2f, 1f);
            handOffset = new Vec2(2f, 0f);
            ShellOffset = new Vec2(-1f, -2f);
            _weight = 1.5f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 8 });

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
            _fireWait = this.StockDeployed() ? 0.75f : 0.5f;
            loseAccuracy = this.StockDeployed() ? 0.15f : 0.25f;
            maxAccuracyLost = this.StockDeployed() ? 0.35f : 0.7f;
            _weight = this.StockDeployed() ? 1.5f : 1f;
        }

        private void UpdateFrames()
        {
            NonSkin = this.StockDeployed() ? 0 : this.StockFolded() ? 2 : 1;
        }
    }
}

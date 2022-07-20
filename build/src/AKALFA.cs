using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.StockLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class AKALFA : BaseAr, IHaveAllowedSkins, IHaveStock
    {
        private const int NonSkinFrames = 3;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 4, 5 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        private bool _stock = true;

        private float _stockstate = 1f;

        public AKALFA(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 20;
            _ammoType = new AT545NATO
            {
                range = 360f,
                accuracy = 1f,
                bulletSpeed = 60f,
                bulletThickness = 0.87f,
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("ALFA"), 38, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _graphic = _sprite;
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
            _kickForce = 0.65f;
            KickForceSlowAr = 0.05f;
            KickForceFastAr = 0.75f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0.25f;
            _editorName = "Alfa";
            _weight = 5.5f;
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
            loseAccuracy = this.StockDeployed() ? 0f : 0.1f;
            _weight = this.StockDeployed() ? 5.5f : 3.5f;
        }

        protected override float BaseKforce => this.StockDeployed() ? 0.65f : 1.2f;

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

        public string StockOn => Mod.GetPath<Core.TMGmod>("sounds/tuduc");
        public string StockOff => Mod.GetPath<Core.TMGmod>("sounds/tuduc");
    }
}

using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [UsedImplicitly]
    [EditorGroup("TMG|Shotgun|Pump-Action")]
    public class RemingtonTac : BasePumpAction, IHaveAllowedSkins, IHaveStock
    {
        private const int NonSkinFrames = 3;
        private static float Rmax => 3.506401f;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 3, 5, 8, 9 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        private bool _stock;

        private float _stockstate;

        public RemingtonTac(float xval, float yval) : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 4;
            _ammoType = new ATFABARM();
            IntrinsicAccuracy = true;
            _numBulletsPerFire = 6;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Remington 870 Raid"), 26, 8);
            _graphic = _sprite;
            _center = new Vec2(13f, 4f);
            _collisionOffset = new Vec2(-13f, -4f);
            _collisionSize = new Vec2(26f, 8f);
            _barrelOffsetTL = new Vec2(26f, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(0f, 1f);
            _fireSound = "shotgunFire2";
            _kickForce = 2.8f;
            _manualLoad = true;
            _fireWait = 1.5f;
            _laserOffsetTL = new Vec2(22f, 1f);
            laserSight = true;
            _editorName = "Remington 870 Raid";
            LoaderSprite = new SpriteMap(GetPath("Remington 870 RaidPump"), 5, 2)
            {
                center = new Vec2(3f, 1f),
            };
            FrameId = 20;
            ShellOffset = new Vec2(2f, -2f);
            LoaderVec2 = new Vec2(8f, -0.5f);
            Loaddx = 3f;
            Stock = false;
        }

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set
            {
                SetSpriteMapFrameId(_sprite, value, 10 * NonSkinFrames);
                SetSpriteMapFrameId(LoaderSprite, value, 10);
            }
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

        [UsedImplicitly]
        public float StockState
        {
            get => _stockstate;
            set => _stockstate = Maths.Clamp(value, 0f, 1f);
        }

        private void UpdateStats()
        {
            _fireWait = this.StockDeployed() ? 0f : 2.75f;
            LoadSpeed = (sbyte)(this.StockDeployed() ? 20 : 10);
        }

        protected override float GetBaseKforce() => this.StockDeployed() ? 1.1f : 2.8f;

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

        protected override void OnInitialize()
        {
            Stock = false;
            base.OnInitialize();
        }

        private void GottaGoFast()
        {
            var runMax = duck.runMax;
            duck.runMax = Rmax;
            duck.UpdateMove();
            duck.runMax = runMax;
        }
        public override void Update()
        {
            base.Update();
            if (!Stock && duck != null) GottaGoFast();
        }
    }
}

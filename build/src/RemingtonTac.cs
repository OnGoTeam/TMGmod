using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.StockLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [UsedImplicitly]
    [EditorGroup("TMG|Shotgun|Pump-Action")]
    public class RemingtonTac : BasePumpAction, IHaveAllowedSkins, IHaveStock
    {
        private bool _stock = true;

        private float _stockstate = 1f;

        public RemingtonTac(float xval, float yval) : base(xval, yval)
        {
            _editorName = "Remington 870 Raid";
            ammo = 4;
            SetAmmoType<ATRemington>();
            _numBulletsPerFire = 6;
            NonSkinFrames = 3;
            Smap = new SpriteMap(GetPath("Remington 870 Raid"), 26, 8);
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
            _laserOffsetTL = new Vec2(22f, 1.5f);
            laserSight = true;
            LoaderSprite = new SpriteMap(GetPath("Remington 870 RaidPump"), 5, 2)
            {
                center = new Vec2(3f, 1f),
            };
            FrameId = 20;
            ShellOffset = new Vec2(2f, -2f);
            LoaderVec2 = new Vec2(8f, 0f);
            Loaddx = 3f;
        }

        private static float Rmax => 3.506401f;

        protected override float BaseKforce => this.StockDeployed() ? 1.1f : 2.8f;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 3, 5, 8, 9 });


        protected override void UpdateFrameId(int frameId)
        {
            SetSpriteMapFrameId(LoaderSprite, frameId, SkinFrames);
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

        private void UpdateStats()
        {
            _fireWait = this.StockDeployed() ? 0f : 2.75f;
            LoadSpeed = (sbyte)(this.StockDeployed() ? 20 : 10);
        }

        private void UpdateFrames()
        {
            NonSkin = this.StockDeployed() ? 0 : this.StockFolded() ? 2 : 1;
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
            if (!Stock && duck != null && ammo > 0 && (ammo != 1 || loaded)) GottaGoFast();
        }
    }
}

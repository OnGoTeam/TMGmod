using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.StockLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    [UsedImplicitly]
    public class Vixr : BaseGun, IAmAr, IHaveAllowedSkins, IHaveStock
    {
        private const int NonSkinFrames = 3;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 6, 8 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        private bool _stock = true;

        private float _stockstate = 1f;

        [UsedImplicitly] public StateBinding HandAngleOffBinding = new StateBinding(nameof(HandAngleOff));

        [UsedImplicitly] public float HandAngleOffState;

        [UsedImplicitly] public StateBinding HandAngleOffStateBinding = new StateBinding(nameof(HandAngleOffState));

        public Vixr(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 21;
            _ammoType = new ATARS();
            MaxAccuracy = 0.81f;
            _type = "gun";
            //THIS FILE HAS REBORN TREE TIMES SQUARES!! send this massage to your friends or not to friends
            _sprite = new SpriteMap(GetPath("Anyx ARS Elite"), 33, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(17f, 5f);
            _collisionOffset = -_center;
            _collisionSize = new Vec2(33f, 9f);
            _barrelOffsetTL = new Vec2(28f, 2f);
            _holdOffset = new Vec2(4f, 2f);
            ShellOffset = new Vec2(-13f, -2f);
            _fireSound = GetPath("sounds/RifleOrMG.wav");
            _flare = new SpriteMap(GetPath("FlareAnyxARS"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fullAuto = true;
            _fireWait = 0.75f;
            _kickForce = 3f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.4f;
            _editorName = "Anyx ARS Elite";
            _weight = 6f;
            handAngle = 0f;
        }

        [UsedImplicitly]
        public float HandAngleOff
        {
            get => handAngle * offDir;
            set => handAngle = value * offDir;
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
            _fireWait = this.StockDeployed() ? 0.75f : 0.6f;
            loseAccuracy = this.StockDeployed() ? 0.15f : 0.2f;
            maxAccuracyLost = this.StockDeployed() ? 0.3f : 0.6f;
            _weight = this.StockDeployed() ? 6f : 3.5f;
        }

        protected override float BaseKforce => this.StockDeployed() ? 3f : 4.6f;

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

        public override void Update()
        {
            HandAngleOff = HandAngleOffState;
            base.Update();
        }

        public override void OnHoldAction()
        {
            if (ammo > 0) HandAngleOff -= 0.01f;
            else if (ammo < 1) HandAngleOff = 0f;
            HandAngleOffState = HandAngleOff;
            base.OnHoldAction();
        }

        public override void OnReleaseAction()
        {
            HandAngleOff = 0f;
            HandAngleOffState = HandAngleOff;
            base.OnReleaseAction();
        }
    }
}

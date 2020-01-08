using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [UsedImplicitly]
    [EditorGroup("TMG|Shotgun|Pump-Action")]
    public class RemingtonTac : BasePumpAction, IHaveSkin, IHaveStock
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 3;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0 });
        private bool _stock;
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
                _fireWait = stock ? 0f : 2.75f;
                _kickForce = stock ? 1.1f : 2.8f;
                LoadSpeed = (sbyte)(stock ? 20 : 10);
                FrameId = FrameId % 10 + 10 * (stock ? 0 : nostock ? 2 : 1);
                if (isServerForObject && stock && stockstate <= 0.99f)
                    SFX.Play(GetPath("sounds/beepods1"));
                if (isServerForObject && nostock && stockstate >= 0.01f)
                    SFX.Play(GetPath("sounds/beepods2"));
            }
        }

        private float _stockstate;
        public float StockState
        {
            get => _stockstate;
            set
            {
                value = Math.Max(value, 0f);
                value = Math.Min(value, 1f);
                _stockstate = value;
            }
        }
        public StateBinding StockStateBinding { get; } = new StateBinding(nameof(StockState));

        [UsedImplicitly]
        public StateBinding StockBinding { get; } = new StateBinding(nameof(StockBuffer));

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
        public RemingtonTac(float xval, float yval) : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 4;
            _ammoType = new AT9mm
            {
                range = 125f,
                accuracy = 0.69f,
                penetration = 1f,
                bulletSpeed = 25f,
                bulletThickness = 0.5f
            };
            _numBulletsPerFire = 6;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Remington 870 Raid"), 26, 8);
            _graphic = _sprite;
            _sprite.frame = 20;
            _center = new Vec2(13f, 4f);
            _collisionOffset = new Vec2(-13f, -4f);
            _collisionSize = new Vec2(26f, 8f);
            _barrelOffsetTL = new Vec2(26f, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _holdOffset = new Vec2(0f, 1f);
            _fireSound = "shotgunFire2";
            _kickForce = 2.8f;
            _manualLoad = true;
            _fireWait = 1.5f;
            _laserOffsetTL = new Vec2(22f, 1f);
            laserSight = true;
            _editorName = "Remington 870 Raid";
            LoaderSprite = new SpriteMap(GetPath("Remington 870 RaidPump"), 6, 8)
            {
                center = new Vec2(3f, 4f)
            };
            LoaderVec2 = new Vec2(7f, -0.5f);
            Loaddx = 3f;
            Stock = false;
        }

        public override void Initialize()
        {
            Stock = false;
            base.Initialize();
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
                Stock = Stock;
        }
        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic))
            {
                bublic = Rando.Int(0, 9);
            }
            _sprite.frame = bublic;
        }
        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set
            {
                _sprite.frame = value % (10 * NonSkinFrames);
                LoaderSprite.frame = (value % 10 + 10) % 10;
            }
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
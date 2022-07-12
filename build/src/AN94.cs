using DuckGame;
using JetBrains.Annotations;
using System.Collections.Generic;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Burst")]
    // ReSharper disable once InconsistentNaming
    public class AN94 : BaseBurst, IHspeedKforce, IAmAr, IHaveSkin
    {
        [UsedImplicitly]
        public bool Laserrod
        {
            get => _sprite.frame < 10;
            set
            {
                if (value)
                {
                    loseAccuracy = 0.15f;
                    _fireWait = 1.5f;
                    maxAccuracyLost = 0.45f;
                    _sprite.frame %= 10;
                    laserSight = false;
                }
                else
                {
                    loseAccuracy = 0.1f;
                    _fireWait = 2.5f;
                    maxAccuracyLost = 0.1f;
                    _sprite.frame %= 10;
                    _sprite.frame += 10;
                    laserSight = true;
                }
            }
        }
        [UsedImplicitly]
        public StateBinding StockBinding = new StateBinding(nameof(Laserrod));
        // ReSharper disable once MemberCanBePrivate.Global
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 6, 7 });
        public AN94(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            _sprite = new SpriteMap(GetPath("AN94"), 33, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(16f, 5f);
            _collisionOffset = new Vec2(-15f, -5f);
            _collisionSize = new Vec2(33f, 9f);
            _barrelOffsetTL = new Vec2(33f, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _holdOffset = new Vec2(1f, 2f);
            ShellOffset = new Vec2(0f, -3f);
            ammo = 30;
            _ammoType = new AT545NATO { range = 260f, bulletSpeed = 60f, accuracy = 0.87f };
            _fireSound = "deepMachineGun2";
            _fullAuto = false;
            _fireWait = 1.5f;
            KickForceSlowAr = 0.5f;
            _kickForce = 1.6f;
            KickForceFastAr = 2f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.45f;
            _editorName = "AN94";
            _weight = 4.5f;
            laserSight = false;
            _laserOffsetTL = new Vec2(30f, 2.5f);
            DeltaWait = 0.07f;
            BurstNum = 2;
        }

        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                Laserrod = !Laserrod;
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }
            base.Update();
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
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
        public float KickForceSlowAr { get; }
        public float KickForceFastAr { get; }
    }
}
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun|Burst")]
    // ReSharper disable once InconsistentNaming
    public class SRM1208 : BaseBurst, IAmSg, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 2;
        private bool Loaded = true;
        private int _yee = 20;
        private bool _yeeenabled = false;
        private bool _shootwasyes = false;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 1, 2, 8 });

        public SRM1208(float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 8;
            _ammoType = new ATSRM1();
            BaseAccuracy = 0.6f;
            _numBulletsPerFire = 5;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("SRM1206"), 29, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(15f, 5f);
            _collisionOffset = new Vec2(-15f, -5f);
            _collisionSize = new Vec2(29f, 10f);
            _barrelOffsetTL = new Vec2(29f, 2f);
            _fireSound = "shotgunFire";
            _fullAuto = false;
            _fireWait = 2f;
            _kickForce = 6f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.25f;
            _holdOffset = new Vec2(0f, 1f);
            _editorName = "SRM 1208";
            _weight = 4.5f;
            DeltaWait = 1f;
            BurstNum = 2;
        }
        public override void Update()
        {
            if (ammo % 2 == 0) _ammoType = new ATSRM1(); else _ammoType = new ATSRM2();
            if (ammo <= 0)
            {
                Loaded = false;
                _sprite.frame %= 10;
            }
            if (_yeeenabled) _yee -= 1;
            if ((!_yeeenabled) && (_yee < 20)) _yee = 20;
            if (_yee <= 0)
            {
                SFX.Play(GetPath("sounds/tuduc.wav"));
                _sprite.frame %= 10;
                _yeeenabled = false;
                Loaded = true;
            }
            base.Update();
        }
        public override void OnPressAction()
        {
            if ((!Loaded) && (ammo > 0) && (!_yeeenabled))
            {
                if (_sprite.frame < 10) SFX.Play(GetPath("sounds/tuduc.wav"));
                _yeeenabled = true;
                _sprite.frame = (_sprite.frame % 10) + 10;
            }
            else if (Loaded) Fire();
        }
        public override void OnReleaseAction()
        {
            if (_shootwasyes)
            {
                _shootwasyes = false;
                Loaded = false;
            }
            base.OnReleaseAction();
        }
        public override void Fire()
        {
            _shootwasyes = true;
            base.Fire();
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
    }
}
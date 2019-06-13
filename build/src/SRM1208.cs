using System.Collections.Generic;
using DuckGame;
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
        private int _yee = 30;
        private bool _yeeenabled = false;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        public EditorProperty<int> Skin { get; }
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 1, 2, 8 });

        public SRM1208(float xval, float yval)
          : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 8;
            _ammoType = new ATMagnum
            {
                range = 120f,
                accuracy = 0.4f,
                penetration = 1f
            };
            BaseAccuracy = 0.4f;
            _numBulletsPerFire = 6;
            _barrelAngleOffset = -2f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("SRM1206"), 29, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(15f, 5f);
            _collisionOffset = new Vec2(-15f, -5f);
            _collisionSize = new Vec2(29f, 10f);
            _barrelOffsetTL = new Vec2(29f, 4f);
            _fireSound = "shotgunFire";
            _fullAuto = false;
            _fireWait = 2f;
            _kickForce = 4f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.25f;
            _holdOffset = new Vec2(-5f, 2f);
            _editorName = "Crocodile 12 ammo";
            _weight = 5.5f;
            DeltaWait = 0.5f;
            BurstNum = 2;
        }
        public override void Update()
        {
            if (ammo % 2 == 0) _barrelAngleOffset = 25f; else _barrelAngleOffset = -25f;
            if (ammo <= 0)
            {
                Loaded = false;
                _sprite.frame %= 10;
            }
            if (_yeeenabled) _yee -= 1;
            if ((!_yeeenabled) && (_yee != 30)) _yee = 30;
            if (_yee <= 0)
            {
                SFX.Play(GetPath("sounds/tuduc.wav"));
                _sprite.frame %= 10;
                _yeeenabled = false;
                Loaded = true;
            }
            base.Update();
        }
        public override void Fire()
        {
            if (Loaded) Fire();
            base.Fire();
        }
        public override void OnPressAction()
        {
            if (!Loaded)
            {
                _sprite.frame %= 10;
                _sprite.frame += 10;
                SFX.Play(GetPath("sounds/tuduc.wav"));
                _yeeenabled = true;
            }
            else Fire();
            base.OnPressAction();
        }
        public override void OnReleaseAction()
        {
            if ((Loaded) && (ammo > 0)) Loaded = false;
            base.OnReleaseAction();
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
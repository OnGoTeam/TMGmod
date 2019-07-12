﻿#if DEBUG
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Buddies
{
    [EditorGroup("TMG|DEBUG")]
    // ReSharper disable once InconsistentNaming
    public class AKwARv2: BaseGun, IAmAr, MagBuddy.ISupportReload
    {
        private readonly SpriteMap _sprite;
        private readonly MagBuddy _magBuddy;
        private bool _onemoreclick = true;
        private byte _mags = 1;

        public AKwARv2(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new ATMagnum
            {
                range = 500f,
                accuracy = 0.85f,
                penetration = 2f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("ARW-A"), 27, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(14f, 4f);
            _collisionOffset = new Vec2(-14f, -4f);
            _collisionSize = new Vec2(27f, 9f);
            _barrelOffsetTL = new Vec2(27f, 4f);
            _holdOffset = new Vec2(1f, -1f);
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.85f;
            _kickForce = 1f;
            loseAccuracy = 0.05f;
            maxAccuracyLost = 0.2f;
            _editorName = "AKwARv2";
            _weight = 5f;
            _magBuddy = new MagBuddy(this, typeof(UziPro));
        }


        public override void OnPressAction()
        {
            if (ammo <= 0) _magBuddy.Doload();
            base.OnPressAction();
        }

        public override void Update()
        {
            if (ammo <= 0) _magBuddy.Disload();
            if ((ammo <= 0) && (_mags <= 0)) _sprite.frame = 2;
            base.Update();
        }

        public bool SetMag()
        {
            if (_mags <= 0) return false;
            if (_wait > 1f) return false;
            if (_onemoreclick)
            {
                SFX.Play(GetPath("sounds/tuduc.wav"));
                _sprite.frame = 3;
                _wait += 5f;
                return _onemoreclick = false;
            }
            _onemoreclick = true;
            ammo = 30;
            _mags -= 1;
            return true;
        }

        public bool DropMag(Thing mag)
        {
            SFX.Play(GetPath("sounds/tuduc.wav"));
            _sprite.frame = 1;
            _wait += 7f;
            return true;
        }

        public Vec2 SpawnPos { get; set; }
    }
}
#endif
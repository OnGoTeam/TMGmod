#if DEBUG
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Buddies
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class AKwARv2: BaseGun, IAmDmr, MagBuddy.ISupportReload
    {
        private readonly MagBuddy _magBuddy;
        private bool _onemoreclick = true;
        private byte _mags = 3;

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
            _graphic = new Sprite(GetPath("AKwAR1"));
            _center = new Vec2(16f, 5f);
            _collisionOffset = new Vec2(-15.5f, -5f);
            _collisionSize = new Vec2(31f, 11f);
            _barrelOffsetTL = new Vec2(31f, 4f);
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

            base.Update();
        }

        public bool SetMag()
        {
            if (_mags <= 0) return false;
            if (_wait > 1f) return false;
            if (_onemoreclick)
            {
                SFX.Play(GetPath("sounds/tuduc.wav"));
                _graphic = new Sprite(GetPath("AKwAR1"));
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
            graphic = new Sprite(GetPath("AKwAR"));
            _wait += 7f;
            return true;
        }

        public Vec2 SpawnPos { get; set; }
    }
}
#endif
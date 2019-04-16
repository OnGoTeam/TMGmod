#if DEBUG
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Buddies
{
    [EditorGroup("TMG|Sniper")]
    // ReSharper disable once InconsistentNaming
    public class SVUE: BaseGun, IAmDmr, MagBuddy.ISupportReload
    {
        private readonly MagBuddy _magBuddy;
        private bool _onemoreclick = true;
        private byte _mags = 3;

        public SVUE (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 10;
            _ammoType = new ATMagnum
            {
                range = 580f,
                accuracy = 0.91f,
                penetration = 1.5f
            };
            _type = "gun";
            _graphic = new Sprite(GetPath("SVU"));
            _center = new Vec2(20f, 8f);
            _collisionOffset = new Vec2(-14.5f, -8f);
            _collisionSize = new Vec2(31f, 11f);
            _barrelOffsetTL = new Vec2(31f, 5f);
            _fireSound = GetPath("sounds/HeavyRifle.wav");
            _fullAuto = true;
            _fireWait = 0.75f;
            _kickForce = 1.5f;
            loseAccuracy = 0.05f;
            maxAccuracyLost = 0.45f;
            _holdOffset = new Vec2(1f, 2f);
            _editorName = "SVUE";
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
                _wait += 5f;
                return _onemoreclick = false;
            }
            _onemoreclick = true;
            ammo = 10;
            _mags -= 1;
            return true;
        }

        public bool DropMag(Thing mag)
        {
            SFX.Play(GetPath("sounds/tuduc.wav"));
            _wait += 7f;
            return true;
        }

        public Vec2 SpawnPos { get; set; }
    }
}
#endif
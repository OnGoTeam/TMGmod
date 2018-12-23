using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    [BaggedProperty("canSpawn", false)]
    // ReSharper disable once InconsistentNaming
    public class AKwAR : Gun
    {
        private int _aammo;
        private readonly int _ammo;
        private int _rs;
        private bool _isroundsin = true; 


        public AKwAR(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 30;
            _ammo = 30;
            _aammo = 30;
            _ammoType = new ATMagnum
            {
                range = 500f,
                accuracy = 0.85f,
                penetration = 2f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("AKwAR1"));
            center = new Vec2(16f, 5f);
            collisionOffset = new Vec2(-15.5f, -5f);
            collisionSize = new Vec2(31f, 11f);
            _barrelOffsetTL = new Vec2(31f, 4f);
            _holdOffset = new Vec2(1f, -1f);
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.85f;
            _kickForce = 1f;
            loseAccuracy = 0.05f;
            maxAccuracyLost = 0.2f;
            _editorName = "AK with Ammo Reload";
			weight = 5f;
            _bio = "Deprecated, bugs with sprite";
        }


        public override void OnPressAction()
        {
            if (ammo > 0)
            {
                Fire();
                return;
            }
            if (!_isroundsin || ammo != 0) return;
            //else
            SFX.Play("Click");
            graphic = new Sprite(GetPath("AKwAR"));
            _isroundsin = false;

        }

        public override void Update()
        {
            base.Update();
            if (_isroundsin) return;
            //else
            _rs = _rs + 1;
            if (_rs != 10) return;
            //else
            SFX.Play("Click");
            graphic = new Sprite(GetPath("AKwAR1"));
            _isroundsin = true;
            if (_aammo > _ammo)
            {
                ammo = _ammo;
                _aammo = _aammo - _ammo;
            }
            else
            {
                ammo = _aammo;
                _aammo = 0;
            }
            _rs = 0;
        }
        /*public override void OnHoldAction()
        {
            this._fireSound = "Click";
            this.PlayFireSound();
            this._fireSound = "deepMachineGun2";
            this.graphic = new Sprite(GetPath("AKALFA"));
            this._isroundsin = true;
            if (this._aammo + this.ammo > this._ammo)
            {
                this._aammo = this._aammo - (this._ammo - this.ammo);
                this.ammo = this._ammo;
            }
            else
            {
                this.ammo = this._aammo + this.ammo;
                this._aammo = 0;
            }
        }*/
        

    }
}

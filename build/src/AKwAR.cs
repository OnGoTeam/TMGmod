using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Machinegun")]
    public class AKwAR : Gun
    {
        int _aammo;
        int _ammo;
        int _rs;
        bool _isroundsin = true; 


        public AKwAR(float xval, float yval)
            : base(xval, yval)
        {
            this.ammo = 30;
            this._ammo = 30;
            this._aammo = 30;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 500f;
            this._ammoType.accuracy = 0.85f;
            this._ammoType.penetration = 2f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("AKwAR1"));
            this.center = new Vec2(16f, 5f);
            this.collisionOffset = new Vec2(-15.5f, -5f);
            this.collisionSize = new Vec2(31f, 11f);
            this._barrelOffsetTL = new Vec2(31f, 4f);
            this._holdOffset = new Vec2(1f, -1f);
            this._fireSound = "deepMachineGun2";
            this._fullAuto = true;
            this._fireWait = 0.85f;
            this._kickForce = 1f;
            this.loseAccuracy = 0.05f;
            this.maxAccuracyLost = 0.2f;
            this._editorName = "AK with Ammo Reload";
			this.weight = 5f;
            this._bio = "Deprecated, bugs with sprite";
        }
        

        public override void Initialize()
        {
            base.Initialize();
        }
        public override void OnPressAction()
        {
            if (this.ammo > 0)
            {
                base.Fire();
            }
            else
            {

                /*if (this._isroundsin == false)
                {
                    SFX.Play("Click");
                    this.graphic = new Sprite(GetPath("AKwAR1"));
                    this._isroundsin = true;
                    if (this._aammo > this._ammo)
                    {
                        this.ammo = this._ammo;
                        this._aammo = this._aammo - this._ammo;
                    }
                    else
                    {
                        this.ammo = this._aammo;
                        this._aammo = 0;
                    }
                }*/


                if (this._isroundsin == true && this.ammo == 0)
                {
                    SFX.Play("Click");
                    this.graphic = new Sprite(GetPath("AKwAR"));
                    this._isroundsin = false;
                }
                

                
            }
            
        }

        public override void Update()
        {
            if (this._isroundsin == false)
            {
                _rs = _rs + 1;
                if (_rs == 10)
                {
                    SFX.Play("Click");
                    this.graphic = new Sprite(GetPath("AKwAR1"));
                    this._isroundsin = true;
                    if (this._aammo > this._ammo)
                    {
                        this.ammo = this._ammo;
                        this._aammo = this._aammo - this._ammo;
                    }
                    else
                    {
                        this.ammo = this._aammo;
                        this._aammo = 0;
                    }
                    _rs = 0;
                }
            }
            
            base.Update();
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

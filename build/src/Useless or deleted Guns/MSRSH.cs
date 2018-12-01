using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using DuckGame;


namespace TMGmod.src
{
    [BaggedProperty("isInDemo", true), BaggedProperty("canSpawn", false)]
    public class MSRC : Sniper
    {
        int ammo2;
        AmmoType _ammoType2;
        Sprite graphic1;
        Sprite graphic2;
        Vec2 _barrelOffsetTL2;
		string _fireSound2;
        float loseAccuracy2;
        float maxAccuracyLost2;
        int _numBulletsPerFire2;
		bool switched = false;
		bool _fullAuto2 = false;
		bool drobovik = false;
		bool snuper = true;
	//Are you see that? This is a sniper rifle with underbarrel shotgun. But it does not work. R.I.P.
    //Wait.. what are you doing here?!	
        public MSRC(float xval, float yval) : base(xval, yval)
        {
            this.graphic = new Sprite(GetPath("MSRSH"));
            this.graphic1 = new Sprite(GetPath("MSRSH1"));
            this.graphic2 = new Sprite(GetPath("MSRSH2"));
            this.center = new Vec2(28.5f, 6f);
            this.collisionOffset = new Vec2(-28.5f, -6f);
            this.collisionSize = new Vec2(47f, 12f);
            this._barrelOffsetTL = new Vec2(48f, 5f);
            this._barrelOffsetTL2 = new Vec2(14f, 6f);
            this.ammo = 5;
			this.ammo2 = 4;
            this._ammoType = new ATSniper();
            this._ammoType.bulletSpeed = 85f;
			this._ammoType2 = new AT9mm();
            this._ammoType2.range = 130f;
            this._ammoType2.accuracy = 1f;
            this._ammoType2.penetration = 1f;
			this._ammoType2.bulletSpeed = 40f;
            this._numBulletsPerFire = 1;
            this._numBulletsPerFire2 = 7;
            this._fireSound = "sniper";
            this._fireSound2 = "littleGun";
            this._fullAuto = false;
            this._fullAuto2 = false;
            this._kickForce = 2f;
            this._fireWait = 2f;
            this.laserSight = false;
            this.loseAccuracy = 0f;
            this.loseAccuracy2 = 0f;
            this.maxAccuracyLost = 0f;
            this.maxAccuracyLost2 = 0f;
            this._laserOffsetTL = new Vec2(31f, 9f);
            this._holdOffset = new Vec2(14f, -1f);
            this._editorName = "MSR With SH";
			this.weight = 5.45f;
			

        }

        public override void Draw()
        {
            float ang = this.angle;
            if (this.offDir <= 0)
            {
                this.angle = this.angle + this._angleOffset;
            }
            else
            {
                this.angle = this.angle - this._angleOffset;
            }
            base.Draw();
            this.angle = ang;
            this.laserSight = false;
        }

        public override void OnPressAction()
        {
            if (this.loaded)
            {
                base.OnPressAction();
                return;
            }
            if (this.ammo > 0 && this._loadState == -1 && this.drobovik == false && this.snuper == true)
            {
                this._loadState = 0;
                this._loadAnimation = 0;
            }
        }

        public override void Update()
        {
            base.Update();
            if (this._loadState > -1 && this.drobovik == false && this.snuper == true)
            {
                if (this.owner == null)
                {
                    if (this._loadState == 3)
                    {
                        this.loaded = true;
                    }
                    this._loadState = -1;
                    this._angleOffset = 0f;
                    this.handOffset = Vec2.Zero;
                }
                if (this._loadState == 0)
                {
                    if (!Network.isActive)
                    {
                        SFX.Play("loadSniper", 1f, 0f, 0f, false);
                    }
                    else if (base.isServerForObject)
                    {
                        this._netLoad.Play(1f, 0f);
                    }
                    Sniper sniper = this;
                    sniper._loadState = sniper._loadState + 1;
                }
                else if (this._loadState == 1)
                {
                    if (this._angleOffset >= 0.1f)
                    {
                        Sniper sniper1 = this;
                        sniper1._loadState = sniper1._loadState + 1;
                    }
                    else
                    {
                        this._angleOffset = this._angleOffset + 0.003f;
                    }
                }
                else if (this._loadState == 2)
                {
                    this.handOffset.x = this.handOffset.x - 0.2f;
                    if (this.handOffset.x > 4f)
                    {
                        Sniper sniper2 = this;
                        sniper2._loadState = sniper2._loadState + 1;
                        this.Reload(true);
                        this.loaded = false;
                    }
                }
                else if (this._loadState == 3)
                {
                    this.handOffset.x = this.handOffset.x + 0.2f;
                    if (this.handOffset.x <= 0f)
                    {
                        Sniper sniper3 = this;
                        sniper3._loadState = sniper3._loadState + 1;
                        this.handOffset.x = 0f;
                    }
                }
                else if (this._loadState == 4)
                {
                    if (this._angleOffset <= 0.03f)
                    {
                        this._loadState = -1;
                        this.loaded = true;
                        this._angleOffset = 0f;
                    }
                    else
                    {
                        this._angleOffset = MathHelper.Lerp(this._angleOffset, 0f, 0.15f);
                    }
                }
            }
            this.laserSight = false;
			            if (this.owner != null)
            {
                if (base.isServerForObject)
                {
                    if (base.duck.inputProfile.Pressed("QUACK", false))
                    {
						if (!switched)
						{
							switched = true;
                            this.graphic = new Sprite(GetPath("MSRSH1"));
                        }
    			        Sprite g2 = this.graphic2;
                        this.graphic2 = this.graphic;
                        this.graphic = g2;
                        float la2 = this.loseAccuracy2;
                        this.loseAccuracy2 = this.loseAccuracy;
                        this.loseAccuracy = la2;
    			        float mal2 = this.maxAccuracyLost2;
                        this.maxAccuracyLost2 = this.maxAccuracyLost;
                        this.maxAccuracyLost = mal2;
    			        int fak = this._numBulletsPerFire2;
                        this._numBulletsPerFire2 = this._numBulletsPerFire;
                        this._numBulletsPerFire = fak;
                        Vec2 botl2 = this._barrelOffsetTL2;
                        this._barrelOffsetTL2 = this._barrelOffsetTL;
                        this._barrelOffsetTL = botl2;
                        int a2 = this.ammo2;
                        this.ammo2 = this.ammo;
                        this.ammo = a2;
                        AmmoType at2 = this._ammoType2;
                        this._ammoType2 = this._ammoType;
                        this._ammoType = at2;
						string s2 = this._fireSound2;
						this._fireSound2 = this._fireSound;
						this._fireSound = s2;
                        bool autonik = this._fullAuto2;
                        this._fullAuto2 = this._fullAuto;
                        this._fullAuto = autonik;
                        bool stupidshit = this.snuper;
                        this.snuper = this.drobovik;
                        this.drobovik = stupidshit;
					}
				}
			}
		    base.Update();
		}
        public override void Thrown()
        {
            if (this.ammo == 0)
            {
                Sprite g2 = this.graphic2;
                this.graphic2 = this.graphic;
                this.graphic = g2;
                float la2 = this.loseAccuracy2;
                this.loseAccuracy2 = this.loseAccuracy;
                this.loseAccuracy = la2;
                float mal2 = this.maxAccuracyLost2;
                this.maxAccuracyLost2 = this.maxAccuracyLost;
                this.maxAccuracyLost = mal2;
    			int fak = this._numBulletsPerFire2;
                this._numBulletsPerFire2 = this._numBulletsPerFire;
                this._numBulletsPerFire = fak;
                Vec2 botl2 = this._barrelOffsetTL2;
                this._barrelOffsetTL2 = this._barrelOffsetTL;
                this._barrelOffsetTL = botl2;
                int a2 = this.ammo2;
                this.ammo2 = this.ammo;
                this.ammo = a2;
                AmmoType at2 = this._ammoType2;
                this._ammoType2 = this._ammoType;
                this._ammoType = at2;
				string s2 = this._fireSound2;
				this._fireSound2 = this._fireSound;
				this._fireSound = s2;
                bool autonik = this._fullAuto2;
                this._fullAuto2 = this._fullAuto;
                this._fullAuto = autonik;
                bool stupidshit = this.snuper;
                this.snuper = this.drobovik;
                this.drobovik = stupidshit;
            }
            base.Thrown();
        }		
        }
	}
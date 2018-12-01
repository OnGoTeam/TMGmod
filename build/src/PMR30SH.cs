using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Pistol")]
    public class PMR : Gun
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

        public PMR(float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 30;
			this.ammo2 = 1;
            this._ammoType = new AT9mm();
            this._ammoType.range = 215f;
            this._ammoType.accuracy = 0.875f;
            this._ammoType.penetration = 1f;
			this._ammoType2 = new AT9mm();
            this._ammoType2.range = 110f;
            this._ammoType2.accuracy = 0.35f;
            this._ammoType2.penetration = 1f;
			this._ammoType2.bulletSpeed = 50f;
            this._numBulletsPerFire = 1;
            this._numBulletsPerFire2 = 16;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("PMR30"));
            this.graphic1 = new Sprite(GetPath("PMR301"));
            this.graphic2 = new Sprite(GetPath("PMR302"));
            this.center = new Vec2(8f, 5f);
            this.collisionOffset = new Vec2(-8f, -5f);
            this.collisionSize = new Vec2(16f, 10f);
            this._barrelOffsetTL = new Vec2(16f, 2.5f);
            this._barrelOffsetTL2 = new Vec2(14f, 6f);
            this._holdOffset = new Vec2(0f, 2f);
            this._fireSound = GetPath("sounds/1.wav");
            this._fireSound2 = "littleGun";
            this._fullAuto = false;
            this._fireWait = 0.5f;
            this._kickForce = 0.55f;
            this.loseAccuracy = 0.025f;
            this.loseAccuracy2 = 0f;
            this.maxAccuracyLost = 0.15f;
            this.maxAccuracyLost2 = 0f;
            this._editorName = "PMR30 With SG";
			this.weight = 1f;
        }
        public override void Update()
        {
            if (this.owner != null)
            {
                if (base.isServerForObject)
                {
                    if (base.duck.inputProfile.Pressed("QUACK", false))
                    {
						if (!switched)
						{
							switched = true;
                            this.graphic = new Sprite(GetPath("pmr301"));
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
            }
            base.Thrown();
        }		
	}
}
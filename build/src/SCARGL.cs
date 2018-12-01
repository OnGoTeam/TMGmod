using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Machinegun")]
    public class scargl : Gun
    {
        int ammo2;
        AmmoType _ammoType2;
        Sprite graphic1;
        Sprite graphic2;
        Vec2 _barrelOffsetTL2;
		string _fireSound2;
        float loseAccuracy2;
        float maxAccuracyLost2;
		bool switched = false;

        public scargl (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 20;
			this.ammo2 = 1;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 900f;
            this._ammoType.accuracy = 0.9f;
            this._ammoType.penetration = 1f;
			this._ammoType.bulletSpeed = 35f;
			this._ammoType.barrelAngleDegrees = 0f;
			this._ammoType2 = new ATGrenade();
            this._ammoType2.range = 2500f;
            this._ammoType2.accuracy = 1f;
            this._ammoType2.penetration = 1f;
			this._ammoType2.bulletSpeed = 18f;
			this._ammoType2.barrelAngleDegrees = -7.5f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("scargl"));
            this.graphic1 = new Sprite(GetPath("scargl1"));
            this.graphic2 = new Sprite(GetPath("scargl2"));
            this.center = new Vec2(16.5f, 5f);
            this.collisionOffset = new Vec2(-16.5f, -5f);
            this.collisionSize = new Vec2(33f, 11f);
            this._barrelOffsetTL = new Vec2(33f, 3f);
            this._barrelOffsetTL2 = new Vec2(30f, 6.5f);
            this._holdOffset = new Vec2(2f, 0f);
            this._fireSound = GetPath("sounds/scar.wav");
            this._fireSound2 = "deepMachineGun";
            this._fullAuto = true;
            this._fireWait = 1.2f;
            this._kickForce = 0.8f;
            this.loseAccuracy = 0.1f;
            this.loseAccuracy2 = 0f;
            this.maxAccuracyLost = 0.2f;
            this.maxAccuracyLost2 = 0f;
            this._editorName = "SCAR-H With GL";
			this.weight = 6f;

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
                            this.graphic = new Sprite(GetPath("scargl1"));
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
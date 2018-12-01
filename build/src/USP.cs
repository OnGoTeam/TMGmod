using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Pistol")]
    public class usp : Gun
    {
		bool glooshitel = false;

        public usp(float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 13;
            this._ammoType = new AT9mm();
            this._ammoType.range = 100f;
            this._ammoType.accuracy = 0.8f;
            this._ammoType.penetration = 1f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("USP"));
            this.center = new Vec2(8f, 3f);
            this.collisionOffset = new Vec2(-7.5f, -3.5f);
            this.collisionSize = new Vec2(23f, 9f);
            this._barrelOffsetTL = new Vec2(15f, 3f);
            this._fireSound = GetPath("sounds/1.wav");
            this._fullAuto = false;
            this._fireWait = 0.75f;
            this._kickForce = 0f;
            this.loseAccuracy = 0.05f;
            this.maxAccuracyLost = 0.1f;
            this._editorName = "USP-S";
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
					  if (this.glooshitel)
					    {
				         this.graphic = new Sprite(GetPath("USP"));
                         this._fireSound = GetPath("sounds/1.wav");
                         this._ammoType = new AT9mm();
                         this._ammoType.range = 100f;
                         this._ammoType.accuracy = 0.8f;
			             this._barrelOffsetTL = new Vec2(15f, 3f);	
						 this.glooshitel = false;
					    }
                      else
					    {
				         this.graphic = new Sprite(GetPath("USPS"));
                         this._fireSound = GetPath("sounds/SilencedPistol.wav");
                         this._ammoType = new AT9mmS();
                         this._ammoType.range = 130f;
                         this._ammoType.accuracy = 0.9f;
			             this._barrelOffsetTL = new Vec2(23f, 3f);			 
						 this.glooshitel = true;
					    }
					}
				}
			}
		    base.Update();
		}			
	}
}

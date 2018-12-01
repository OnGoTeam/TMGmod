using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|SMG")]
    public class MAPFire : Gun
    {
		bool glooshitel = false;

        public MAPFire (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 33;
            this._ammoType = new AT9mm();
            this._ammoType.range = 70f;
            this._ammoType.accuracy = 0.9f;
            this._ammoType.penetration = 3f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("uzipro"));
            this.center = new Vec2(8f, 5f);
            this.collisionOffset = new Vec2(-8f, -5f);
            this.collisionSize = new Vec2(16f, 10f);
            this._barrelOffsetTL = new Vec2(11f, 3f);
            this._fireSound = GetPath("sounds/smg.wav");
            this._fullAuto = true;
            this._fireWait = 0.4f;
            this._kickForce = 0.5f;
            this.loseAccuracy = 0.005f;
            this.maxAccuracyLost = 0.5f;
            this._holdOffset = new Vec2(2f, 0f);
            this.laserSight = true;
            this._laserOffsetTL = new Vec2(9f, 6f);
            this._editorName = "Uzi Pro";
			this.weight = 2.5f;
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
				         this.graphic = new Sprite(GetPath("uzipro"));
                         this._ammoType = new AT9mm();
                         this._ammoType.range = 70f;
                         this._ammoType.accuracy = 0.9f;
                         this._ammoType.penetration = 3f;
			             this._barrelOffsetTL = new Vec2(11f, 3f);	
						 this.glooshitel = false;
                         this._fireSound = GetPath("sounds/smg.wav");
					    }
                      else
					    {
				         this.graphic = new Sprite(GetPath("uzipros"));
                         this._ammoType = new AT9mmS();
                         this._ammoType.range = 100f;
                         this._ammoType.accuracy = 1f;
                         this._ammoType.penetration = 2f;
			             this._barrelOffsetTL = new Vec2(17f, 3f);			 
	 					 this.glooshitel = true;
                         this._fireSound = GetPath("sounds/SilencedPistol.wav");
					    }
					}
				}
			}
		    base.Update();
		}			
	}
}

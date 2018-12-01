using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Machinegun")]
    public class Vixr : Gun
    {
		float stockngrip = 0f;
		
        public Vixr(float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 25;
            this._ammoType = new AT9mmS();
            this._ammoType.range = 400f;
            this._ammoType.accuracy = 0.78f;
            this._ammoType.penetration = 1f;
            this._ammoType.bulletSpeed = 21f;
            this._type = "gun";
			//I'M BLUE DARUDE SANDSTORM DA DUBAI
            this.graphic = new Sprite(GetPath("VixrStock"));
            this.center = new Vec2(16.5f, 4.5f);
            this.collisionOffset = new Vec2(-16.5f, -4.5f);
            this.collisionSize = new Vec2(33f, 9f);
            this._barrelOffsetTL = new Vec2(34f, 4f);
            this._holdOffset = new Vec2(3f, 0f);
            this._fireSound = GetPath("sounds/Silenced1.wav");
            this._fullAuto = true;
            this._fireWait = 0.65f;
            this._kickForce = 0.8f;
            this.loseAccuracy = 0.099f;
            this.maxAccuracyLost = 0.17f;
            this._editorName = "Vixr";
			this.weight = 3.9f;
		}
        public override void Update()
        {
            if (this.owner != null)
            {
                if (base.isServerForObject)
                {
                    if (base.duck.inputProfile.Pressed("QUACK", false))
                    {
					  if ((this.stockngrip > 0f))
					    {
				            this.graphic = new Sprite(GetPath("VixrStock"));
                            this._ammoType.accuracy = 0.78f;
                            this.loseAccuracy = 0.099f;
		                 	this.stockngrip = 0f;
			                this.weight = 3.9f;
					    }
                      else
					    {
			   	            this.graphic = new Sprite(GetPath("VixrNoStock"));
                            this._ammoType.accuracy = 0.74f;
                            this.loseAccuracy = 0.13f;
		                	this.stockngrip = 1f;
			                this.weight = 2f;
					    }
					}
				}
			}
		    base.Update();
		}			
	}
}
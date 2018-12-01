using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Machinegun")]
    public class aug : Gun
    {
        private SpriteMap _sprite;

        bool grip = false;
		
        public aug (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 42;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 650f;
            this._ammoType.accuracy = 0.91f;
            this._ammoType.penetration = 1f;
            this._type = "gun";
            this._sprite = new SpriteMap(GetPath("AUGSM"), 30, 12);
            this.graphic = this._sprite;
            this.center = new Vec2(15f, 6f);
            this.collisionOffset = new Vec2(-15f, -6f);
            this.collisionSize = new Vec2(30f, 12f);
            this._barrelOffsetTL = new Vec2(30f, 5f);
            this._holdOffset = new Vec2(-3f, 0f);
            this._fireSound = GetPath("sounds/scar.wav");
            this._fullAuto = true;
            this._fireWait = 0.8f;
            this._kickForce = 0.7f;
            this.loseAccuracy = 0.1f;
            this.maxAccuracyLost = 0.2f;
            this._editorName = "AUG A1";
			this.weight = 5.5f;
            this._sprite.AddAnimation("base", 0f, false, new int[]
            {
                0,
            });
            this._sprite.AddAnimation("grip", 0f, false, new int[]
            {
                1,
            });
        }
        public override void Update()
        {
            if (this.owner != null)
            {
                if (base.isServerForObject)
                {
                    if (base.duck.inputProfile.Pressed("QUACK", false))
                    {
			    	    if (this.grip)
				 	    {
					        this._sprite.SetAnimation("base");
                            this._fireWait = 0.8f;
                            this.loseAccuracy = 0.1f;
		   		            this.maxAccuracyLost = 0.2f;
	   		     			this._ammoType.accuracy = 0.91f;
						    this.grip = false;
					    }
                        else
					    {
					        this._sprite.SetAnimation("grip");
                            this._fireWait = 1.2f;
                            this.loseAccuracy = 0.25f;
				            this.maxAccuracyLost = 0.125f;
						    this._ammoType.accuracy = 0.94f;
						    this.grip = true;
					    }
					}
				}
			}
		    base.Update();
		}			
	}
}
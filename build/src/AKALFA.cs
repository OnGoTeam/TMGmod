using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Machinegun")]
    public class AKALFA : Gun
    {
        private SpriteMap _sprite;
        float stock = 0f;
		
        public AKALFA (float xval, float yval)
          : base(xval, yval)
		{
            this.ammo = 20;
            this._ammoType = new AT9mm();
            this._ammoType.range = 425f;
            this._ammoType.accuracy = 1f;
            this._ammoType.penetration = 1.5f;
            this._ammoType.bulletSpeed = 60f;
            this._ammoType.bulletThickness = 0.87f;
            this._type = "gun";
            this._sprite = new SpriteMap(GetPath("ALFASM"), 38, 9);
		    this.graphic = this._sprite;
            this.center = new Vec2(19f, 4.5f);
            this.collisionOffset = new Vec2(-19f, -4.5f);
            this.collisionSize = new Vec2(38f, 9f);
            this._barrelOffsetTL = new Vec2(38f, 2.5f);
            this._holdOffset = new Vec2(5f, 0f);
            this._fireSound = "deepMachineGun2";
            this._fullAuto = true;
            this._fireWait = 0.675f;
            this._kickForce = 0.65f;
            this.loseAccuracy = 0f;
            this.maxAccuracyLost = 0.3f;
            this._editorName = "Alfa";
			this.weight = 5.5f;
            this._laserOffsetTL = new Vec2(31f, 4f);
            this.laserSight = true;
		    this._sprite.AddAnimation("base", 0f, false, new int[]
		    {
		        0,
		    });
		    this._sprite.AddAnimation("stock", 0f, false, new int[]
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
					  if ((this.stock > 0f))
					    {
					        this._sprite.SetAnimation("base");
                            this._ammoType.accuracy = 1f;
                            this.loseAccuracy = 0f;
		                 	this.stock = 0f;
						    this.weight = 5.5f;
					    }
                      else
					    {
					        this._sprite.SetAnimation("stock");
                            this._ammoType.accuracy = 0.92f;
                            this.loseAccuracy = 0.045f;
		                	this.stock = 1f;
						    this.weight = 3f;
					    }
					}
				}
			}
		    base.Update();
		}			
	}
}
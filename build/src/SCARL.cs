using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Machinegun")]
    public class scarl : Gun
    {
		
        public scarl (float xval, float yval)
          : base(xval, yval)
		{
            this.ammo = 30;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 800f;
            this._ammoType.accuracy = 0.87f;
            this._ammoType.penetration = 1f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("SCARLsemiauto"));
            this.center = new Vec2(16.5f, 5.5f);
            this.collisionOffset = new Vec2(-16.5f, -5.5f);
            this.collisionSize = new Vec2(33f, 11f);
            this._barrelOffsetTL = new Vec2(33f, 4f);
            this._holdOffset = new Vec2(2f, 0f);
            this._fireSound = GetPath("sounds/scar.wav");
            this._fullAuto = false;
            this._fireWait = 1.3f;
            this._kickForce = 0.7f;
            this.loseAccuracy = 0.025f;
            this.maxAccuracyLost = 0.2f;
            this._editorName = "SCAR-L";
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
					  if (this._fullAuto)
					    {
					     this._fullAuto = false;
				         this.graphic = new Sprite(GetPath("SCARLsemiauto"));
				         this._fireWait = 1.3f;
				         this.maxAccuracyLost = 0.2f;
					    }
                      else
					    {
						 this._fullAuto = true;
				         this.graphic = new Sprite(GetPath("SCARLauto"));
				         this._fireWait = 0.9f;
				         this.maxAccuracyLost = 0.45f;
					    }
					}
				}
			}
		    base.Update();
		}			
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Machinegun")]
    public class scarpdw : Gun
    {
		bool upirka = false;
		
        public scarpdw (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 30;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 600f;
            this._ammoType.accuracy = 0.79f;
            this._ammoType.penetration = 1f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("scarpdwstock"));
            this.center = new Vec2(14f, 5f);
            this.collisionOffset = new Vec2(-14f, -5f);
            this.collisionSize = new Vec2(28f, 11f);
            this._barrelOffsetTL = new Vec2(28f, 4f);
            this._holdOffset = new Vec2(0f, 0f);
            this._fireSound = GetPath("sounds/scar.wav");
            this._fullAuto = true;
            this._fireWait = 0.8f;
            this._kickForce = 0.4f;
            this.loseAccuracy = 0.1f;
            this.maxAccuracyLost = 0.3f;
            this._editorName = "SCAR-L PDW";
			this.weight = 5.5f;
        }
        public override void Update()
        {
            if (this.owner != null)
            {
                if (base.isServerForObject)
                {
                    if (base.duck.inputProfile.Pressed("QUACK", false))
                    {
					  if (this.upirka)
					    {
				         this.graphic = new Sprite(GetPath("scarpdwstock"));
                         this.loseAccuracy = 0.1f;
				         this.maxAccuracyLost = 0.2f;
			             this.weight = 5.5f;
						 this.upirka = false;
					    }
                      else
					    {
				         this.graphic = new Sprite(GetPath("SCARPDW"));
                         this.loseAccuracy = 0.3f;
				         this.maxAccuracyLost = 0.5f;
			             this.weight = 3f;
						 this.upirka = true;
					    }
					}
				}
			}
		    base.Update();
		}			
	}
}
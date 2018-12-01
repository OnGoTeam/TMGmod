using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Rifle")]
    public class Nellegalja : Gun
    {
		bool laser = false;
		
        public Nellegalja (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 20;
            this._ammoType = new AT9mmS();
            this._ammoType.range = 800f;
            this._ammoType.accuracy = 0.95f;
            this._ammoType.penetration = 1f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("Nellegalja"));
            this.center = new Vec2(15f, 5f);
            this.collisionOffset = new Vec2(-14.5f, -5f);
            this.collisionSize = new Vec2(29f, 11f);
            this._barrelOffsetTL = new Vec2(30f, 4f);
            this._fireSound = GetPath("sounds/Silenced2.wav");
            this._fullAuto = false;
            this._fireWait = 0.8f;
            this._kickForce = 0.7f;
            this.loseAccuracy = 0.1f;
            this.maxAccuracyLost = 0.3f;
            this.laserSight = true;
            this._laserOffsetTL = new Vec2(21f, 1.5f);
            this._editorName = "Nellegalja Weapon";
			this.weight = 4f;
        }
        public override void Update()
        {
            if (this.owner != null)
            {
                if (base.isServerForObject)
                {
                    if (base.duck.inputProfile.Pressed("QUACK", false))
                    {
					  if (this.laser)
					    {
                         this.laserSight = true;
						 this.laser = false;
					    }
                      else
					    {
                         this.laserSight = false;
						 this.laser = true;
					    }
					}
				}
			}
		    base.Update();
		}			
	}
}
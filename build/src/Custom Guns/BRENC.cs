using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Machinegun|Custom")]
    public class bren : Gun
    {
		float silencer = 0f;
		
        public bren (float xval, float yval)
          : base(xval, yval)
		{
            this.ammo = 30;
            this._ammoType = new AT9mm();
            this._ammoType.range = 500f;
            this._ammoType.accuracy = 0.87f;
            this._ammoType.penetration = 1f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("CZ805BrenZ"));
            this.center = new Vec2(20.5f, 5.5f);
            this.collisionOffset = new Vec2(-20.5f, -5.5f);
            this.collisionSize = new Vec2(41f, 11f);
            this._barrelOffsetTL = new Vec2(39f, 3.5f);
            this._holdOffset = new Vec2(5f, 1f);
            this._fireSound = "deepMachineGun2";
            this._fullAuto = false;
            this._fireWait = 1.45f;
            this._kickForce = 0.7f;
            this.loseAccuracy = 0.025f;
            this.maxAccuracyLost = 0.2f;
            this._editorName = "CZ-805 Civilian";
			this.weight = 5f;
        }
        public override void Update()
        {
            if (this.owner != null)
            {
                if (base.isServerForObject)
                {
                    if (base.duck.inputProfile.Pressed("QUACK", false))
                    {
					  if ((this.silencer > 0f))
					    {
				         this.graphic = new Sprite(GetPath("CZ805BrenZ"));
            this._fireSound = "deepMachineGun2";
            this._ammoType = new AT9mm();
            this._ammoType.range = 500f;
            this._ammoType.accuracy = 0.87f;
            this.loseAccuracy = 0.025f;
            this.maxAccuracyLost = 0.2f;
            this._barrelOffsetTL = new Vec2(39f, 4f);
			this.silencer = 0f;
					    }
                      else
					    {
				         this.graphic = new Sprite(GetPath("CZ805BrenZS"));
            this._fireSound = GetPath("sounds/Silenced2.wav");
            this._ammoType = new AT9mmS();
            this._ammoType.range = 550f;
            this._ammoType.accuracy = 0.95f;
            this.loseAccuracy = 0.02f;
            this.maxAccuracyLost = 0.18f;
            this._barrelOffsetTL = new Vec2(42.5f, 4f);
			this.silencer = 1f;
					    }
					}
				}
			}
		    base.Update();
		}			
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Machinegun")]
    public class M4A1 : Gun
    {
		
		public M4A1 (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 30;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 300f;
            this._ammoType.accuracy = 0.8f;
            this._ammoType.penetration = 1.5f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("M4A1"));
            this.center = new Vec2(15f, 6f);
            this.collisionOffset = new Vec2(-15f, -6f);
            this.collisionSize = new Vec2(30f, 11f);
            this._barrelOffsetTL = new Vec2(31f, 4f);
            this._fireSound = "deepMachineGun";
            this._fullAuto = true;
            this._fireWait = 0.745f;
            this._kickForce = 0f;
            this.loseAccuracy = 0.01f;
            this.maxAccuracyLost = 0.12f;
            this._holdOffset = new Vec2(3f, 1f);
            this._editorName = "M4A1";
			this.weight = 4.5f;
        }
	}
}
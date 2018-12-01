using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Heavy")]
    public class MG44 : Gun
    {
		
		public MG44 (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 80;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 750f;
            this._ammoType.accuracy = 0.75f;
            this._ammoType.penetration = 1.5f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("mg44req"));
            this.center = new Vec2(19.5f, 6f);
            this.collisionOffset = new Vec2(-19.5f, -6f);
            this.collisionSize = new Vec2(39f, 12f);
            this._barrelOffsetTL = new Vec2(40f, 4f);
            this._fireSound = "deepMachineGun";
            this._fullAuto = true;
            this._fireWait = 0.9f;
            this._kickForce = 0.6f;
            this.loseAccuracy = 0f;
            this.maxAccuracyLost = 0f;
            this._holdOffset = new Vec2(4f, 0f);
            this._editorName = "Magnium";
			this.weight = 7.5f;
        }
		public override void Update()
		{
		base.Update();
			if (this.ammo == 1) this.graphic = new Sprite(GetPath("mg44req1"), 0f, 0f);
			if (this.ammo == 0) this.graphic = new Sprite(GetPath("mg44req2"), 0f, 0f);
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGMod.src
{
    
    [BaggedProperty("isInDemo", true)]
	public class PTP1 : Gun
	{

		public PTP1(float xval, float yval)
            : base(xval, yval)
		{
            this.ammo = 30;
            this._ammoType = new PTPA();
            this._type = "gun";
            base.graphic = new Sprite(GetPath("PTP1"), 0f, 0f);
            this.center = new Vec2(8f, 4f);
            this.collisionOffset = new Vec2(-8f, -2f);
            this.collisionSize = new Vec2(16f, 8f);
            this._barrelOffsetTL = new Vec2(17f, 2f);
            this._fireSound = "smg";
            this._fullAuto = true;
            this._fireWait = 2f;
            this._kickForce = 1f;
            this._holdOffset = new Vec2(-1f, 0f);
            this.loseAccuracy = 0.1f;
            this.maxAccuracyLost = 0.2f;
		}

		
	}
}

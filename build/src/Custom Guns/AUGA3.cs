using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Misc|Custom Guns")]
    public class augC : Gun
    {
        public augC (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 30;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 680f;
            this._ammoType.accuracy = 0.96f;
            this._ammoType.penetration = 1f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("auga3"));
            this.center = new Vec2(15f, 6f);
            this.collisionOffset = new Vec2(-15f, -6f);
            this.collisionSize = new Vec2(30f, 12f);
            this._barrelOffsetTL = new Vec2(30f, 5f);
            this._holdOffset = new Vec2(-3f, 0f);
            this._fireSound = GetPath("sounds/scar.wav");
            this._fullAuto = true;
            this._fireWait = 0.8f;
            this._kickForce = 0.7f;
            this.loseAccuracy = 0.025f;
            this.maxAccuracyLost = 0.1f;
            this._editorName = "AUG A3";
			this.weight = 5.5f;
        }
	}
}
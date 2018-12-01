using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Pistol")]
    public class BigShot : Gun
    {
        public BigShot (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 7;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 250f;
            this._ammoType.accuracy = 1f;
            this._ammoType.penetration = 2f;
            this._ammoType.bulletThickness = 3f;
            this._numBulletsPerFire = 4;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("pistol50"));
            this.center = new Vec2(10f, 5f);
            this.collisionOffset = new Vec2(-11.5f, -5f);
            this.collisionSize = new Vec2(23f, 11f);
            this._barrelOffsetTL = new Vec2(25f, 2f);
            this._fireSound = "magnum";
            this._fullAuto = false;
            this._fireWait = 1.6f;
            this._kickForce = 1f;
            this.loseAccuracy = 0f;
            this.maxAccuracyLost = 0f;
            this._holdOffset = new Vec2(2f, 0f);
            this._editorName = "Pistol .50";
			this.weight = 2.5f;
        }


    }
}

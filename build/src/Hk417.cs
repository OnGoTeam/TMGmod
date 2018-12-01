using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Rifle")]
    public class hk417 : Gun
    {
        public hk417 (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 20;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 700f;
            this._ammoType.accuracy = 0.9f;
            this._ammoType.penetration = 1f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("Hk417"));
            this.center = new Vec2(15f, 5f);
            this.collisionOffset = new Vec2(-14.5f, -5f);
            this.collisionSize = new Vec2(30f, 10f);
            this._barrelOffsetTL = new Vec2(31f, 3f);
            this._fireSound = GetPath("sounds/HeavyRifle.wav");
            this._fullAuto = false;
            this._fireWait = 0.8f;
            this._kickForce = 0.7f;
            this.loseAccuracy = 0.1f;
            this.maxAccuracyLost = 0.3f;
            this._editorName = "Hk-417C";
			this.weight = 3.5f;
        }
    }
}
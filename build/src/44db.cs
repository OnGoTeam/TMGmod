using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Shotgun")]
    public class Deadly44 : Gun
    {
		
		public EditorProperty<bool> onemoreammo = new EditorProperty<bool>(false, null, 0f, 1f, 1f, "Second Bullet", false, false);
		
        public Deadly44 (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 1;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 150f;
            this._ammoType.accuracy = 0.2f;
            this._ammoType.penetration = 4f;
            this._numBulletsPerFire = 44;
            this._ammoType.bulletThickness = 2f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("44db"));
            this.center = new Vec2(16.5f, 5f);
            this.collisionOffset = new Vec2(-16.5f, -5f);
            this.collisionSize = new Vec2(33f, 10f);
            this._barrelOffsetTL = new Vec2(34f, 2.5f);
            this._holdOffset = new Vec2(2f, 1f);
            this._fireSound = "shotgun";
            this._fullAuto = false;
            this._fireWait = 4f;
            this._kickForce = 1.8f;
            this.loseAccuracy = 0.25f;
            this.maxAccuracyLost = 0.5f;
            this._editorName = "DeadlyGauge";
			this.weight = 4f;
        }
    }
}
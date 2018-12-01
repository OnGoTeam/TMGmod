using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Sniper")]
    public class ussrgun: Gun
    {
        public ussrgun (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 20;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 1400f;
            this._ammoType.accuracy = 0.92f;
            this._ammoType.penetration = 1f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("SCAR20SSR"));
            this.center = new Vec2(18f, 7f);
            this.collisionOffset = new Vec2(-18f, -7f);
            this.collisionSize = new Vec2(36f, 14f);
            this._barrelOffsetTL = new Vec2(37f, 6f);
            this._holdOffset = new Vec2(3f, -1f);
            this._fireSound = GetPath("sounds/scar.wav");
            this._fullAuto = true;
            this._fireWait = 0.8f;
            this._kickForce = 0.7f;
            this.loseAccuracy = 0.1f;
            this.maxAccuracyLost = 0.3f;
            this._editorName = "SCAR SSR";
            this.laserSight = true;
            this._laserOffsetTL = new Vec2(19f, 4f);
			this.weight = 7f;
        }
          public override void Update()
        {
            if (this._owner != null && this._owner.height < 17f)
            {
                this._kickForce = 0f;
				this.loseAccuracy = 0f;
                this.maxAccuracyLost = 0f;
				this.graphic = new Sprite(GetPath("SCAR20SSRbipods"));
            }
            else
            {
                this._kickForce = 0.7f;
                this.loseAccuracy = 0.1f;
                this.maxAccuracyLost = 0.3f;
				this.graphic = new Sprite(GetPath("SCAR20SSR"));
            }
            base.Update();
        }
	}
}
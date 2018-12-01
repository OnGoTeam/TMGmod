using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Sniper")]
    public class M50 : Gun
    {
        public M50 (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 6;
            this._ammoType = new cal50explode();
            this._ammoType.range = 1500f;
            this._ammoType.accuracy = 1f;
            this._ammoType.penetration = 1f;
            this._ammoType.bulletThickness = 2.5f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("m50"));
            this.center = new Vec2(20f, 6.5f);
            this.collisionOffset = new Vec2(-20f, -6.5f);
            this.collisionSize = new Vec2(40f, 13f);
            this._barrelOffsetTL = new Vec2(40f, 6f);
            this._fireSound = GetPath("sounds/HeavySniper.wav");
            this._fullAuto = false;
            this._fireWait = 3.75f;
            this._kickForce = 1.6f;
            this.loseAccuracy = 0.1f;
            this.maxAccuracyLost = 0.3f;
            this._holdOffset = new Vec2(6f, -1f);
            this.laserSight = true;
            this._laserOffsetTL = new Vec2(31f, 10f);
            this._editorName = "M50 with Explosive Ammo";
			this.weight = 6.75f;
        }
    }
}

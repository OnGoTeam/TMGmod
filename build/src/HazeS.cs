using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{

    [BaggedProperty("isInDemo", true), EditorGroup("TMG|AutoPistol")]
    public class HazeS : Gun
    {

        public HazeS(float xval, float yval) :
            base(xval, yval)
        {
            this.ammo = 36;
            this._ammoType = new HA();
            this._ammoType.range = 400f;
            this._type = "gun";
            base.graphic = new Sprite(GetPath("haze"), 0f, 0f);
            this.center = new Vec2(12f, 3f);
            this.collisionOffset = new Vec2(-12f, -3f);
            this.collisionSize = new Vec2(24f, 12f);
            this._barrelOffsetTL = new Vec2(25f, 2f);
            this._fireSound = GetPath("sounds/SilencedPistol.wav");
            this._fullAuto = true;
            this._fireWait = 1f;
            this._kickForce = 0.5f;
            this._holdOffset = new Vec2(1f, 0f);
            this.loseAccuracy = 0.05f;
            this.maxAccuracyLost = 0.1f;
            this._editorName = "AF Haze";
            this.laserSight = true;
            this._laserOffsetTL = new Vec2(16f, 6f);
			this.weight = 2f;
        }


    }
}

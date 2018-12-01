using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Pistol")]
    public class PMRC : Gun
    {

        public PMRC(float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 30;
            this._ammoType = new AT9mm();
            this._ammoType.range = 215f;
            this._ammoType.accuracy = 0.875f;
            this._ammoType.penetration = 1f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("PMR30Civilian"));
            this.center = new Vec2(8f, 5f);
            this.collisionOffset = new Vec2(-8f, -5f);
            this.collisionSize = new Vec2(16f, 10f);
            this._barrelOffsetTL = new Vec2(16f, 2.5f);
            this._holdOffset = new Vec2(0f, 2f);
            this._fireSound = GetPath("sounds/1.wav");
            this._fullAuto = false;
            this._fireWait = 0.5f;
            this._kickForce = 0.55f;
            this.loseAccuracy = 0.025f;
            this.maxAccuracyLost = 0.15f;
            this._editorName = "PMR30";
			this.weight = 1f;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|SMG")]
    public class MAP : Gun
    {
        public MAP(float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 27;
            this._ammoType = new AT9mm();
            this._ammoType.range = 100f;
            this._ammoType.accuracy = 0.4f;
            this._ammoType.penetration = 1f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("Mixaly4sPistol"));
            this.center = new Vec2(7f, 7f);
            this.collisionOffset = new Vec2(-8f, -7f);
            this.collisionSize = new Vec2(16f, 15f);
            this._barrelOffsetTL = new Vec2(16f, 5f);
            this._fireSound = GetPath("sounds/1.wav");
            this._fullAuto = true;
            this._fireWait = 0.45f;
            this._kickForce = 0f;
            this.loseAccuracy = 0.1f;
            this.maxAccuracyLost = 0.2f;
            this._holdOffset = new Vec2(-2f, 2f);
            this._editorName = "Michael";
			this.weight = 2.5f;
        }


    }
}

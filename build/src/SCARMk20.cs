using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Rifle")]
    public class mk20 : Gun
    {
        public mk20 (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 20;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 800f;
            this._ammoType.accuracy = 0.87f;
            this._ammoType.penetration = 1f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("SCARMk1"));
            this.center = new Vec2(16f, 7f);
            this.collisionOffset = new Vec2(-16.5f, -7f);
            this.collisionSize = new Vec2(33f, 14f);
            this._barrelOffsetTL = new Vec2(33f, 5.5f);
            this._holdOffset = new Vec2(1f, -1f);
            this._fireSound = GetPath("sounds/scar.wav");
            this._fullAuto = false;
            this._fireWait = 0.95f;
            this._kickForce = 0.7f;
            this.loseAccuracy = 0.1f;
            this.maxAccuracyLost = 0.3f;
            this._editorName = "SCAR Mk20";
			this.weight = 6.5f;
        }
        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
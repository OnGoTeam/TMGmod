using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Sniper|Custom")]
    public class SVUC : Gun
    {
        public SVUC (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 5;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 700f;
            this._ammoType.accuracy = 0.925f;
            this._ammoType.penetration = 1.5f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("SVUlmag"));
            this.center = new Vec2(20f, 8f);
            this.collisionOffset = new Vec2(-14.5f, -8f);
            this.collisionSize = new Vec2(31f, 11f);
            this._barrelOffsetTL = new Vec2(31f, 5f);
            this._fireSound = GetPath("sounds/HeavyRifle.wav");
            this._fullAuto = true;
            this._fireWait = 0.75f;
            this._kickForce = 1.5f;
            this.loseAccuracy = 0.05f;
            this.maxAccuracyLost = 0.25f;
            this._holdOffset = new Vec2(1f, 2f);
            this._editorName = "SVU with Low Mag";
			this.weight = 5f;
        }
        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
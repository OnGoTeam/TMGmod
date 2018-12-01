using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Pistol")]
    public class AF2011 : Gun
    {
        public AF2011 (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 10;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 150f;
            this._ammoType.accuracy = 0.95f;
            this._ammoType.penetration = 1f;
            this._numBulletsPerFire = 2;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("AF2011"));
            this.center = new Vec2(7f, 4f);
            this.collisionOffset = new Vec2(-8f, -4f);
            this.collisionSize = new Vec2(16f, 10f);
            this._barrelOffsetTL = new Vec2(16f, 1f);
            this._fireSound = "pistolFire";
            this._fullAuto = false;
            this._fireWait = 1.2f;
            this._kickForce = 0f;
            this.loseAccuracy = 0f;
            this.maxAccuracyLost = 0f;
            this._holdOffset = new Vec2(-1f, 1f);
            this._editorName = "AF-2011";
			this.weight = 2.5f;
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        public override void Fire()
        {
            
            
            if (this.ammo > 0)
            {
                this._ammoType.accuracy = this._ammoType.accuracy - 0.05f;
            }
            base.Fire();
        }

        public override void Update()
        {
            if (this._ammoType.accuracy + 0.01f < 0.95f)
            {
                this._ammoType.accuracy = this._ammoType.accuracy + 0.003f;
                base.Update();
            }
            else
            {
                this._ammoType.accuracy = 0.95f;
            }
            base.Update();
        }

    }
}

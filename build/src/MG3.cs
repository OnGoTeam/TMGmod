using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|LMG")]
    public class MG3 : Gun
    {
		bool bipodes = false;
		
		public MG3 (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 95;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 600f;
            this._ammoType.accuracy = 0.8f;
            this._ammoType.penetration = 1.5f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("mg3"));
            this.center = new Vec2(19.5f, 6f);
            this.collisionOffset = new Vec2(-19.5f, -6f);
            this.collisionSize = new Vec2(39f, 9f);
            this._barrelOffsetTL = new Vec2(40f, 4f);
            this._fireSound = GetPath("sounds/RifleOrMG.wav");
            this._fullAuto = true;
            this._fireWait = 0.5f;
            this._kickForce = 0.95f;
            this.loseAccuracy = 0.025f;
            this.maxAccuracyLost = 0.1f;
            this._holdOffset = new Vec2(4f, 2f);
            this._editorName = "MG3";
			this.weight = 7f;
        }
          public override void Update()
        {
            if (this._owner != null && this._owner.height < 17f)
            {
                this._kickForce = 0f;
				this.loseAccuracy = 0f;
                this.maxAccuracyLost = 0f;
				this.graphic = new Sprite(GetPath("mg3bipods"));
				this.bipodes = true;
            }
            else
            {
                this._kickForce = 0.95f;
                this.loseAccuracy = 0.025f;
                this.maxAccuracyLost = 0.1f;
				this.graphic = new Sprite(GetPath("mg3"));
				this.bipodes = false;
            }
            base.Update();
			if ((this.ammo == 0) && (this.bipodes == false)) this.graphic = new Sprite(GetPath("mg31"), 0f, 0f);
			if ((this.ammo == 0) && (this.bipodes == true)) this.graphic = new Sprite(GetPath("mg3bipods1"), 0f, 0f);
        }
	}
}	
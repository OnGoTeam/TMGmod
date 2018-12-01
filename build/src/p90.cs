using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|SMG")]
    public class P90 : Gun
    {
		
		public EditorProperty<bool> elongated = new EditorProperty<bool>(false, null, 0f, 1f, 1f, null, false, false);
		
        public P90(float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 50;
            this._ammoType = new AT9mm();
            this._ammoType.range = 150f;
            this._ammoType.accuracy = 0.7f;
            this._ammoType.penetration = 1f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("P90"));
            this.center = new Vec2(9.5f, 3f);
            this.collisionOffset = new Vec2(-8.5f, -3f);
            this.collisionSize = new Vec2(19f, 6f);
            this._barrelOffsetTL = new Vec2(19f, 3f);
            this._fireSound = GetPath("sounds/2.wav");
            this._fullAuto = true;
            this._fireWait = 0.2f;
            this._kickForce = 0.4f;
            this.loseAccuracy = 0f;
            this.maxAccuracyLost = 0.1f;
            this._holdOffset = new Vec2(-3f, 0f);
            this.handOffset = new Vec2(2f, 0f);
            this._editorName = "FN P90";
			this.weight = 1.5f;
        }
        public override void Initialize()
        {
			if (!(Level.current is Editor))
            {
                if (this.elongated.value == true)
                {
                 this._ammoType.accuracy = 0.8f;
                 this._ammoType.range = 200f;
				 this.graphic = new Sprite(GetPath("p90long2"));
                }
            }
            base.Initialize();
        }
	}
}
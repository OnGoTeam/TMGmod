using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|AutoPistol")]
    public class Vag : Gun
    {
		float mode = 1f;
		
		public Vag(float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 48;
            this._ammoType = new AT9mm();
            this._ammoType.range = 175f;
            this._ammoType.accuracy = 0.81f;
            this._ammoType.penetration = 1f;
            this._ammoType.bulletSpeed = 12f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("VAG731"));
            this.center = new Vec2(8f, 3f);
            this.collisionOffset = new Vec2(-7.5f, -3.5f);
            this.collisionSize = new Vec2(16f, 11f);
            this._barrelOffsetTL = new Vec2(16f, 1f);
            this._holdOffset = new Vec2(-2f, -3.5f);
            this._fireSound = GetPath("sounds/2.wav");
            this._fullAuto = true;
            this._fireWait = 0.3f;
            this._kickForce = 0.1f;
            this.loseAccuracy = 0.075f;
            this.maxAccuracyLost = 0.225f;
            this._editorName = "Dominator";
			this.weight = 2f;
        }
        public override void Update()
        {
            if (this.owner != null)
            {
                if (base.isServerForObject)
                {
                    if (base.duck.inputProfile.Pressed("QUACK", false))
                    {
					  if ((this.mode > 0f) && (this.mode < 2f))
					    {
				         this.graphic = new Sprite(GetPath("VAG732"));
						 this._fireWait = 0.6f;
						 this.mode = 2f;
					    }
                      else if ((this.mode > 1f) && (this.mode < 3f))
					    {
				         this.graphic = new Sprite(GetPath("VAG733"));
						 this._fireWait = 0.9f;
						 this.mode = 3f;
					    }
                      else if ((this.mode > 2f) && (this.mode < 4f))
					    {
				         this.graphic = new Sprite(GetPath("VAG731"));
						 this._fireWait = 0.3f;
						 this.mode = 1f;
					    }
					}
				}
			}
		    base.Update();
		}			
	}
}
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Rifle")]
    public class SKS : Gun
    {
		int patrons = 16;
		int bullets = 0;
		bool stick = false;
		
        public SKS (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 16;
            this._ammoType = new AT9mm();
            this._ammoType.range = 900f;
            this._ammoType.accuracy = 0.965f;
            this._ammoType.penetration = 1f;
            this._ammoType.bulletSpeed = 75f;
            this._ammoType.bulletThickness = 1f;
            this._type = "gun";
            this.graphic = new Sprite(GetPath("SKS"));
            this.center = new Vec2(30f, 6f);
            this.collisionOffset = new Vec2(-30f, -6f);
            this.collisionSize = new Vec2(60f, 12f);
            this._barrelOffsetTL = new Vec2(53f, 5f);
            this._holdOffset = new Vec2(11f, 0f);
            this._fireSound = GetPath("sounds/scar.wav");
            this._flare.center = new Vec2(0f, 5f);
            this._fullAuto = false;
            this._fireWait = 1.55f;
            this._kickForce = 0.6f;
            this.loseAccuracy = 0.1f;
            this.maxAccuracyLost = 0.8f;
            this._editorName = "SKS";
			this.weight = 6f;
        }
        public override void Update()
        {
		    base.Update();
			if (this.ammo < 17)
			{
			    this.patrons = this.ammo;	
			}
            if (this.owner != null)
            {
                if (base.isServerForObject)
                {
					if (base.duck.inputProfile.Pressed("QUACK"))
					{
						 if (this.ammo < 17)
						 {
							this.patrons = this.ammo;
							this.bullets = this.patrons + 20;
						    this.ammo += 20;
						 }
			            this._flare = new SpriteMap((GetPath("takezis")), 4, 4, false);
                        this._flare.center = new Vec2(0f, 0f);
                        this._ammoType = new ATNB();
                        this._fullAuto = false;
                        this._fireWait = 0.1f;
						this._numBulletsPerFire = 1;
                        this._barrelOffsetTL = new Vec2(53f, 6f);
                        this._fireSound = "";
                        this.loseAccuracy = 0f;
                        this.maxAccuracyLost = 0f;
                        this._ammoType.bulletThickness = 0.1f;
                        this._kickForce = 0f;
						this.stick = true;
				        base.Fire();
					}
                    if (base.duck.inputProfile.Down("QUACK"))
					    {
						this._holdOffset = new Vec2(17f, 0f);
						if (this.ammo < this.bullets)
					        {
								this.ammo += 1;
							}
					    }
                    if (base.duck.inputProfile.Released("QUACK"))
					    {
						this.ammo = this.patrons;
                        this._ammoType = new AT9mm();
                        this._ammoType.range = 900f;
                        this._ammoType.accuracy = 0.91f;
                        this._ammoType.penetration = 1f;
                        this._ammoType.bulletSpeed = 75f;
                        this._fullAuto = false;
                        this._fireWait = 1.3f;
						this._numBulletsPerFire = 1;
                        this._barrelOffsetTL = new Vec2(53f, 3f);
                        this._fireSound = GetPath("sounds/scar.wav");
                        this._holdOffset = new Vec2(11f, 0f);
                        this.loseAccuracy = 0.1f;
                        this.maxAccuracyLost = 0.8f;
                        this._ammoType.bulletThickness = 1f;
                        this._kickForce = 0.6f;
			            this._flare = new SpriteMap("smallFlare", 11, 10, false);
                        this._flare.center = new Vec2(0f, 4f);
						this.stick = false;
					    }
			    }
			}
		}	
        public override void Thrown()
        {
			if (this.ammo != 0)
			{           
						this.ammo = this.patrons;
						this._ammoType = new AT9mm();
                        this._ammoType.range = 900f;
                        this._ammoType.accuracy = 0.91f;
                        this._ammoType.penetration = 1f;
                        this._ammoType.bulletSpeed = 75f;
                        this._fullAuto = false;
                        this._fireWait = 1.3f;
						this._numBulletsPerFire = 1;
                        this._barrelOffsetTL = new Vec2(53f, 3f);
                        this._fireSound = GetPath("sounds/scar.wav");
                        this._holdOffset = new Vec2(11f, 0f);
                        this.loseAccuracy = 0.1f;
                        this.maxAccuracyLost = 0.8f;
                        this._ammoType.bulletThickness = 1f;
                        this._kickForce = 0.6f;
			            this._flare = new SpriteMap("smallFlare", 11, 10, false);
                        this._flare.center = new Vec2(0f, 4f);
			}
			if ((this.stick = true) && (this.patrons == 0))
			{
				this.ammo = 0;
			}
            base.Thrown();
        }		
	}
}
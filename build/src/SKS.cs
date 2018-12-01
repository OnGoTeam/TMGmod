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
            ammo = 16;
            _ammoType = new AT9mm
            {
                range = 900f,
                accuracy = 0.965f,
                penetration = 1f,
                bulletSpeed = 75f,
                bulletThickness = 1f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("SKS"));
            center = new Vec2(30f, 6f);
            collisionOffset = new Vec2(-30f, -6f);
            collisionSize = new Vec2(60f, 12f);
            _barrelOffsetTL = new Vec2(53f, 5f);
            _holdOffset = new Vec2(11f, 0f);
            _fireSound = GetPath("sounds/scar.wav");
            _flare.center = new Vec2(0f, 5f);
            _fullAuto = false;
            _fireWait = 1.55f;
            _kickForce = 0.6f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.8f;
            _editorName = "SKS";
			weight = 6f;
        }
        public override void Update()
        {
		    base.Update();
			if (ammo < 17)
			{
			    patrons = ammo;	
			}
            if (owner != null)
            {
                if (isServerForObject)
                {
					if (duck.inputProfile.Pressed("QUACK"))
					{
						 if (ammo < 17)
						 {
							patrons = ammo;
							bullets = patrons + 20;
						    ammo += 20;
						 }
                        _flare = new SpriteMap((GetPath("takezis")), 4, 4, false)
                        {
                            center = new Vec2(0f, 0f)
                        };
                        _ammoType = new ATNB();
                        _fullAuto = false;
                        _fireWait = 0.1f;
						_numBulletsPerFire = 1;
                        _barrelOffsetTL = new Vec2(53f, 6f);
                        _fireSound = "";
                        loseAccuracy = 0f;
                        maxAccuracyLost = 0f;
                        _ammoType.bulletThickness = 0.1f;
                        _kickForce = 0f;
						stick = true;
				        Fire();
					}
                    if (duck.inputProfile.Down("QUACK"))
					    {
						_holdOffset = new Vec2(17f, 0f);
						if (ammo < bullets)
					        {
								ammo += 1;
							}
					    }
                    if (duck.inputProfile.Released("QUACK"))
					    {
						ammo = patrons;
                        _ammoType = new AT9mm
                        {
                            range = 900f,
                            accuracy = 0.91f,
                            penetration = 1f,
                            bulletSpeed = 75f
                        };
                        _fullAuto = false;
                        _fireWait = 1.3f;
						_numBulletsPerFire = 1;
                        _barrelOffsetTL = new Vec2(53f, 3f);
                        _fireSound = GetPath("sounds/scar.wav");
                        _holdOffset = new Vec2(11f, 0f);
                        loseAccuracy = 0.1f;
                        maxAccuracyLost = 0.8f;
                        _ammoType.bulletThickness = 1f;
                        _kickForce = 0.6f;
                        _flare = new SpriteMap("smallFlare", 11, 10, false)
                        {
                            center = new Vec2(0f, 4f)
                        };
                        stick = false;
					    }
			    }
			}
		}	
        public override void Thrown()
        {
			if (ammo != 0)
			{           
						ammo = patrons;
                _ammoType = new AT9mm
                {
                    range = 900f,
                    accuracy = 0.91f,
                    penetration = 1f,
                    bulletSpeed = 75f
                };
                _fullAuto = false;
                        _fireWait = 1.3f;
						_numBulletsPerFire = 1;
                        _barrelOffsetTL = new Vec2(53f, 3f);
                        _fireSound = GetPath("sounds/scar.wav");
                        _holdOffset = new Vec2(11f, 0f);
                        loseAccuracy = 0.1f;
                        maxAccuracyLost = 0.8f;
                        _ammoType.bulletThickness = 1f;
                        _kickForce = 0.6f;
                _flare = new SpriteMap("smallFlare", 11, 10, false)
                {
                    center = new Vec2(0f, 4f)
                };
            }
			if ((stick = true) && (patrons == 0))
			{
				ammo = 0;
			}
            base.Thrown();
        }		
	}
}
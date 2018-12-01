using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Rifle")]
    public class Nellegalja : Gun
    {
		bool laser = false;
		
        public Nellegalja (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 20;
            _ammoType = new AT9mmS
            {
                range = 800f,
                accuracy = 0.95f,
                penetration = 1f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("Nellegalja"));
            center = new Vec2(15f, 5f);
            collisionOffset = new Vec2(-14.5f, -5f);
            collisionSize = new Vec2(29f, 11f);
            _barrelOffsetTL = new Vec2(30f, 4f);
            _fireSound = GetPath("sounds/Silenced2.wav");
            _fullAuto = false;
            _fireWait = 0.8f;
            _kickForce = 0.7f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            laserSight = true;
            _laserOffsetTL = new Vec2(21f, 1.5f);
            _editorName = "Nellegalja Weapon";
			weight = 4f;
        }
        public override void Update()
        {
            if (owner != null)
            {
                if (isServerForObject)
                {
                    if (duck.inputProfile.Pressed("QUACK", false))
                    {
					  if (laser)
					    {
                         laserSight = true;
						 laser = false;
					    }
                      else
					    {
                         laserSight = false;
						 laser = true;
					    }
					}
				}
			}
		    base.Update();
		}			
	}
}
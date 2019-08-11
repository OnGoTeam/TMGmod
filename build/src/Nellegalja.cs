using DuckGame;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Rifle")]
    public class Nellegalja : Gun
    {
        private bool _laser;
		
        public Nellegalja (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 20;
            _ammoType = new AT9mmS {range = 800f, accuracy = 0.95f, penetration = 1f};
            _type = "gun";
            _graphic = new Sprite(GetPath("Nellegalja"));
            _center = new Vec2(15f, 5f);
            _collisionOffset = new Vec2(-14.5f, -5f);
            _collisionSize = new Vec2(29f, 11f);
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
			_weight = 4f;
        }
        public override void Update()
        {
            if (owner != null)
            {
                if (isServerForObject)
                {
                    if (duck.inputProfile.Pressed("QUACK"))
                    {
					    if (_laser)
					    {
                            laserSight = true;
						    _laser = false;
					    }
                        else
					    {
                            laserSight = false;
						    _laser = true;
					    }
					}
				}
			}
		    base.Update();
		}			
	}
}
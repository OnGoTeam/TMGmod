using DuckGame;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|SMG")]
    // ReSharper disable once InconsistentNaming
    public class MAPFire : Gun
    {
        private bool _glooshitel;

        public MAPFire (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 33;
            _ammoType = new AT9mm {range = 70f, accuracy = 0.9f, penetration = 3f};
            _type = "gun";
            _graphic = new Sprite(GetPath("uzipro"));
            _center = new Vec2(8f, 5f);
            _collisionOffset = new Vec2(-8f, -5f);
            _collisionSize = new Vec2(16f, 10f);
            _barrelOffsetTL = new Vec2(11f, 3f);
            _fireSound = GetPath("sounds/smg.wav");
            _fullAuto = true;
            _fireWait = 0.4f;
            _kickForce = 0.5f;
            loseAccuracy = 0.005f;
            maxAccuracyLost = 0.5f;
            _holdOffset = new Vec2(2f, 0f);
            laserSight = true;
            _laserOffsetTL = new Vec2(9f, 6f);
            _editorName = "Uzi Pro";
			_weight = 2.5f;
        }
        public override void Update()
        {
            if (owner != null)
            {
                if (isServerForObject)
                {
                    if (duck.inputProfile.Pressed("QUACK"))
                    {
					    if (_glooshitel)
                        {
                            _graphic = new Sprite(GetPath("uzipro"));
                            _ammoType = new AT9mm {range = 70f, accuracy = 0.9f, penetration = 3f};
                            _barrelOffsetTL = new Vec2(11f, 3f);	
                            _glooshitel = false;
                            _fireSound = GetPath("sounds/smg.wav");
                        }
                        else
                        {
                            _graphic = new Sprite(GetPath("uzipros"));
                            _ammoType = new AT9mmS {range = 100f, accuracy = 1f, penetration = 2f};
                            _barrelOffsetTL = new Vec2(17f, 3f);			 
                            _glooshitel = true;
                            _fireSound = GetPath("sounds/SilencedPistol.wav");
                        }
					}
				}
			}
		    base.Update();
		}			
	}
}

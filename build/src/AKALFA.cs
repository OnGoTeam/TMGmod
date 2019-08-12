using DuckGame;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Machinegun")]
    // ReSharper disable once InconsistentNaming
    public class AKALFA : Gun
    {
        private readonly SpriteMap _sprite;
        private bool _stock;
		
        public AKALFA (float xval, float yval)
          : base(xval, yval)
		{
            ammo = 20;
            _ammoType = new AT9mm
            {
                range = 425f,
                accuracy = 1f,
                penetration = 1.5f,
                bulletSpeed = 60f,
                bulletThickness = 0.87f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("ALFASM"), 38, 9);
		    _graphic = _sprite;
            _center = new Vec2(19f, 4.5f);
            _collisionOffset = new Vec2(-19f, -4.5f);
            _collisionSize = new Vec2(38f, 9f);
            _barrelOffsetTL = new Vec2(38f, 2.5f);
            _holdOffset = new Vec2(5f, 0f);
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.675f;
            _kickForce = 0.65f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0.3f;
            _editorName = "Alfa";
			_weight = 5.5f;
            _laserOffsetTL = new Vec2(31f, 4f);
            laserSight = true;
		    _sprite.AddAnimation("base", 0f, false, 0);
		    _sprite.AddAnimation("stock", 0f, false, 1);
        }
        public override void Update()
        {
            if (owner != null)
            {
                if (isServerForObject)
                {
                    if (duck.inputProfile.Pressed("QUACK"))
                    {
					    if (_stock)
					    {
					        _sprite.SetAnimation("base");
                            _ammoType.accuracy = 1f;
                            loseAccuracy = 0f;
		                 	_stock = false;
						    _weight = 5.5f;
					    }
                        else
					    {
					        _sprite.SetAnimation("stock");
                            _ammoType.accuracy = 0.92f;
                            loseAccuracy = 0.045f;
		                	_stock = true;
						    _weight = 3f;
					    }
					}
				}
			}
		    base.Update();
		}			
	}
}
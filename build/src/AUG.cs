using DuckGame;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    public class Aug : Gun
    {
        private readonly SpriteMap _sprite;

        private bool _grip;
		
        public Aug (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 42;
            _ammoType = new ATMagnum
            {
                range = 650f,
                accuracy = 0.91f,
                penetration = 1f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("AUGSM"), 30, 12);
            graphic = _sprite;
            center = new Vec2(15f, 6f);
            collisionOffset = new Vec2(-15f, -6f);
            collisionSize = new Vec2(30f, 12f);
            _barrelOffsetTL = new Vec2(30f, 5f);
            _holdOffset = new Vec2(-3f, 0f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 0.8f;
            _kickForce = 0.7f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.2f;
            _editorName = "AUG A1";
			weight = 5.5f;
            _sprite.AddAnimation("base", 0f, false, new int[]
            {
                0,
            });
            _sprite.AddAnimation("grip", 0f, false, new int[]
            {
                1,
            });
        }
        public override void Update()
        {
            if (owner != null)
            {
                if (isServerForObject)
                {
                    if (duck.inputProfile.Pressed("QUACK"))
                    {
			    	    if (_grip)
				 	    {
					        _sprite.SetAnimation("base");
                            _fireWait = 0.8f;
                            loseAccuracy = 0.1f;
		   		            maxAccuracyLost = 0.2f;
	   		     			_ammoType.accuracy = 0.91f;
						    _grip = false;
					    }
                        else
					    {
					        _sprite.SetAnimation("grip");
                            _fireWait = 1.2f;
                            loseAccuracy = 0.25f;
				            maxAccuracyLost = 0.125f;
						    _ammoType.accuracy = 0.94f;
						    _grip = true;
					    }
					}
				}
			}
		    base.Update();
		}			
	}
}
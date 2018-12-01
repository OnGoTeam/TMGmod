using DuckGame;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    public class Scarl : Gun
    {
		
        public Scarl (float xval, float yval)
          : base(xval, yval)
		{
            ammo = 30;
            _ammoType = new ATMagnum
            {
                range = 800f,
                accuracy = 0.87f,
                penetration = 1f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("SCARLsemiauto"));
            center = new Vec2(16.5f, 5.5f);
            collisionOffset = new Vec2(-16.5f, -5.5f);
            collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 4f);
            _holdOffset = new Vec2(2f, 0f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = false;
            _fireWait = 1.3f;
            _kickForce = 0.7f;
            loseAccuracy = 0.025f;
            maxAccuracyLost = 0.2f;
            _editorName = "SCAR-L";
			weight = 6f;
        }
        public override void Update()
        {
            if (owner != null)
            {
                if (isServerForObject)
                {
                    if (duck.inputProfile.Pressed("QUACK"))
                    {
					  if (_fullAuto)
					    {
					     _fullAuto = false;
				         graphic = new Sprite(GetPath("SCARLsemiauto"));
				         _fireWait = 1.3f;
				         maxAccuracyLost = 0.2f;
					    }
                      else
					    {
						 _fullAuto = true;
				         graphic = new Sprite(GetPath("SCARLauto"));
				         _fireWait = 0.9f;
				         maxAccuracyLost = 0.45f;
					    }
					}
				}
			}
		    base.Update();
		}			
	}
}
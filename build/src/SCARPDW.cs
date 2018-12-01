using DuckGame;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    public class Scarpdw : Gun
    {
        private bool _upirka;
		
        public Scarpdw (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new ATMagnum
            {
                range = 600f,
                accuracy = 0.79f,
                penetration = 1f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("scarpdwstock"));
            center = new Vec2(14f, 5f);
            collisionOffset = new Vec2(-14f, -5f);
            collisionSize = new Vec2(28f, 11f);
            _barrelOffsetTL = new Vec2(28f, 4f);
            _holdOffset = new Vec2(0f, 0f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 0.8f;
            _kickForce = 0.4f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _editorName = "SCAR-L PDW";
			weight = 5.5f;
        }
        public override void Update()
        {
            if (owner != null)
            {
                if (isServerForObject)
                {
                    if (duck.inputProfile.Pressed("QUACK"))
                    {
					  if (_upirka)
					    {
				         graphic = new Sprite(GetPath("scarpdwstock"));
                         loseAccuracy = 0.1f;
				         maxAccuracyLost = 0.2f;
			             weight = 5.5f;
						 _upirka = false;
					    }
                      else
					    {
				         graphic = new Sprite(GetPath("SCARPDW"));
                         loseAccuracy = 0.3f;
				         maxAccuracyLost = 0.5f;
			             weight = 3f;
						 _upirka = true;
					    }
					}
				}
			}
		    base.Update();
		}			
	}
}
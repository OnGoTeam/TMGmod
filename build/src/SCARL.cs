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
            ammo = 20;
            _ammoType = new ATMagnum
            {
                range = 650f,
                accuracy = 0.92f,
                penetration = 1f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("RFBSemi"));
            center = new Vec2(16.5f, 5.5f);
            collisionOffset = new Vec2(-16.5f, -5.5f);
            collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 4f);
            _holdOffset = new Vec2(1f, 1f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = false;
            _fireWait = 0.3f;
            _kickForce = 0.7f;
            loseAccuracy = 0.025f;
            maxAccuracyLost = 0.2f;
            _editorName = "RFB";
			weight = 6f;
        }
        public override void Update()
        {
            if (owner != null)
            {
                if (duck.inputProfile.Pressed("QUACK"))
                {
					if (_fullAuto)
					{
					    _fullAuto = false;
				        graphic = new Sprite(GetPath("RFBSemi"));
				        _fireWait = 0.3f;
				        maxAccuracyLost = 0.2f;
					}
                    else
					{
						_fullAuto = true;
				        graphic = new Sprite(GetPath("RFBauto"));
				        _fireWait = 0.63f;
				        maxAccuracyLost = 0.25f;
					}
				}
			}
		    base.Update();
		}			
	}
}
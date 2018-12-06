using DuckGame;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    public class Scarpdw : Gun
    {
        private bool _stock;
		
        public Scarpdw (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 40;
            _ammoType = new ATMagnum
            {
                range = 345f,
                accuracy = 0.83f,
                penetration = 1f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("DaewooK1Stock"));
            center = new Vec2(14f, 5f);
            collisionOffset = new Vec2(-14f, -5f);
            collisionSize = new Vec2(28f, 11f);
            _barrelOffsetTL = new Vec2(28f, 3f);
            _holdOffset = new Vec2(-1f, 0f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 0.86f;
            _kickForce = 0.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.24f;
            _editorName = "Daewoo K1";
			weight = 4.5f;
        }
        public override void Update()
        {
            if (owner != null)
            {
                if (duck.inputProfile.Pressed("QUACK"))
                {
					if (_stock)
					{
				        graphic = new Sprite(GetPath("DaewooK1Stock"));
                        loseAccuracy = 0.1f;
				        maxAccuracyLost = 0.24f;
			            weight = 4.5f;
						_stock = false;
					}
                    else
					{
				        graphic = new Sprite(GetPath("DaewooK1NoStock"));
                        loseAccuracy = 0.2f;
				        maxAccuracyLost = 0.36f;
			            weight = 3f;
						_stock = true;
					}
				}
			}
		    base.Update();
		}			
	}
}
using DuckGame;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Misc|Custom Guns")]
    // ReSharper disable once InconsistentNaming
    public class bren : Gun
    {
        private float _silencer;
		
        public bren (float xval, float yval)
          : base(xval, yval)
		{
            ammo = 30;
            _ammoType = new AT9mm {range = 500f, accuracy = 0.87f, penetration = 1f};
            _type = "gun";
            _graphic = new Sprite(GetPath("CZ805BrenZ"));
            _center = new Vec2(20.5f, 5.5f);
            _collisionOffset = new Vec2(-20.5f, -5.5f);
            _collisionSize = new Vec2(41f, 11f);
            _barrelOffsetTL = new Vec2(39f, 3.5f);
            _holdOffset = new Vec2(5f, 1f);
            _fireSound = "deepMachineGun2";
            _fullAuto = false;
            _fireWait = 1.45f;
            _kickForce = 0.7f;
            loseAccuracy = 0.025f;
            maxAccuracyLost = 0.2f;
            _editorName = "CZ-805 Civilian";
			_weight = 5f;
        }
        public override void Update()
        {
            if (owner != null)
            {
                if (isServerForObject)
                {
                    if (duck.inputProfile.Pressed("QUACK"))
                    {
				        if (_silencer > 0f)
                        {
                            _graphic = new Sprite(GetPath("CZ805BrenZ"));
                            _fireSound = "deepMachineGun2";
                            _ammoType = new AT9mm {range = 500f, accuracy = 0.87f};
                            loseAccuracy = 0.025f;
                            maxAccuracyLost = 0.2f;
                            _barrelOffsetTL = new Vec2(39f, 4f);
                            _silencer = 0f;
                        }
                        else
                        {
                            _graphic = new Sprite(GetPath("CZ805BrenZS"));
                            _fireSound = GetPath("sounds/Silenced2.wav");
                            _ammoType = new AT9mmS {range = 550f, accuracy = 0.95f};
                            loseAccuracy = 0.02f;
                            maxAccuracyLost = 0.18f;
                            _barrelOffsetTL = new Vec2(42.5f, 4f);
                            _silencer = 1f;
                        }
					}
				}
			}
		    base.Update();
		}			
	}
}
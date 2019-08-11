using DuckGame;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Machinegun")]
    // ReSharper disable once InconsistentNaming
    public class cz805 : Gun
    {
        private bool _silencer;
		
        public cz805 (float xval, float yval)
          : base(xval, yval)
		{
            ammo = 30;
            _ammoType = new AT9mm {range = 800f, accuracy = 0.87f, penetration = 1f};
            _type = "gun";
            _graphic = new Sprite(GetPath("CZ805Bren"));
            _center = new Vec2(20.5f, 5.5f);
            _collisionOffset = new Vec2(-20.5f, -5.5f);
            _collisionSize = new Vec2(41f, 11f);
            _barrelOffsetTL = new Vec2(39f, 3.5f);
            _holdOffset = new Vec2(5f, 1f);
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.9f;
            _kickForce = 0.7f;
            loseAccuracy = 0.025f;
            maxAccuracyLost = 0.2f;
            _editorName = "CZ-805 BREN";
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
					    if (_silencer)
					    {
				            _graphic = new Sprite(GetPath("CZ805Bren"));
                            _fireSound = "deepMachineGun2";
                            _ammoType = new AT9mm {range = 800f, accuracy = 0.87f};
                            loseAccuracy = 0.025f;
                            maxAccuracyLost = 0.2f;
                            _barrelOffsetTL = new Vec2(39f, 4f);
			                _silencer = false;
					    }
                        else
					    {
				            _graphic = new Sprite(GetPath("CZ805BrenS"));
                            _fireSound = GetPath("sounds/Silenced2.wav");
                            _ammoType = new AT9mmS {range = 870f, accuracy = 0.95f};
                            loseAccuracy = 0.02f;
                            maxAccuracyLost = 0.18f;
                            _barrelOffsetTL = new Vec2(42.5f, 4f);
			                _silencer = true;
					    }
					}
				}
			}
		    base.Update();
            if (_silencer)
            {
                if (ammo < 28) _graphic = new Sprite(GetPath("CZ805BrenS1"));
                if (ammo < 20) _graphic = new Sprite(GetPath("CZ805BrenS2"));
                if (ammo < 12) _graphic = new Sprite(GetPath("CZ805BrenS3"));
                if (ammo < 5) _graphic = new Sprite(GetPath("CZ805BrenS4"));
            }
            else
            {
                if (ammo < 28) _graphic = new Sprite(GetPath("CZ805Bren1"));
                if (ammo < 20) _graphic = new Sprite(GetPath("CZ805Bren2"));
                if (ammo < 12) _graphic = new Sprite(GetPath("CZ805Bren3"));
                if (ammo < 5) _graphic = new Sprite(GetPath("CZ805Bren4"));
            }
        }			
	}
}
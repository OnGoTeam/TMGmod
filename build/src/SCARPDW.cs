using DuckGame;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Machinegun")]
    // ReSharper disable once InconsistentNaming
    public class scarpdw : Gun
    {
        private bool _upirka;
		
        public scarpdw (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new ATMagnum {range = 600f, accuracy = 0.79f, penetration = 1f};
            _type = "gun";
            _graphic = new Sprite(GetPath("scarpdwstock"));
            _center = new Vec2(14f, 5f);
            _collisionOffset = new Vec2(-14f, -5f);
            _collisionSize = new Vec2(28f, 11f);
            _barrelOffsetTL = new Vec2(28f, 4f);
            _holdOffset = new Vec2(0f, 0f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 0.8f;
            _kickForce = 0.4f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _editorName = "SCAR-L PDW";
			_weight = 5.5f;
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
                            _graphic = new Sprite(GetPath("scarpdwstock"));
                            loseAccuracy = 0.1f;
				            maxAccuracyLost = 0.2f;
			                _weight = 5.5f;
						    _upirka = false;
					    }
                        else
					    {
                            _graphic = new Sprite(GetPath("SCARPDW"));
                            loseAccuracy = 0.3f;
				            maxAccuracyLost = 0.5f;
			                _weight = 3f;
						    _upirka = true;
					    }
					}
				}
			}
		    base.Update();
		}			
	}
}
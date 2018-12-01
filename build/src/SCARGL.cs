using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Machinegun")]
    public class scargl : Gun
    {
        int ammo2;
        AmmoType _ammoType2;
        Sprite graphic1;
        Sprite graphic2;
        Vec2 _barrelOffsetTL2;
		string _fireSound2;
        float loseAccuracy2;
        float maxAccuracyLost2;
		bool switched = false;

        public scargl (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 20;
			ammo2 = 1;
            _ammoType = new ATMagnum
            {
                range = 900f,
                accuracy = 0.9f,
                penetration = 1f,
                bulletSpeed = 35f,
                barrelAngleDegrees = 0f
            };
            _ammoType2 = new ATGrenade
            {
                range = 2500f,
                accuracy = 1f,
                penetration = 1f,
                bulletSpeed = 18f,
                barrelAngleDegrees = -7.5f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("scargl"));
            graphic1 = new Sprite(GetPath("scargl1"));
            graphic2 = new Sprite(GetPath("scargl2"));
            center = new Vec2(16.5f, 5f);
            collisionOffset = new Vec2(-16.5f, -5f);
            collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 3f);
            _barrelOffsetTL2 = new Vec2(30f, 6.5f);
            _holdOffset = new Vec2(2f, 0f);
            _fireSound = GetPath("sounds/scar.wav");
            _fireSound2 = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 1.2f;
            _kickForce = 0.8f;
            loseAccuracy = 0.1f;
            loseAccuracy2 = 0f;
            maxAccuracyLost = 0.2f;
            maxAccuracyLost2 = 0f;
            _editorName = "SCAR-H With GL";
			weight = 6f;

        }
        public override void Update()
        {
            if (owner != null)
            {
                if (isServerForObject)
                {
                    if (duck.inputProfile.Pressed("QUACK", false))
                    {
						if (!switched)
						{
							switched = true;
                            graphic = new Sprite(GetPath("scargl1"));
                        }
    			        Sprite g2 = graphic2;
                        graphic2 = graphic;
                        graphic = g2;
                        float la2 = loseAccuracy2;
                        loseAccuracy2 = loseAccuracy;
                        loseAccuracy = la2;
    			        float mal2 = maxAccuracyLost2;
                        maxAccuracyLost2 = maxAccuracyLost;
                        maxAccuracyLost = mal2;
                        Vec2 botl2 = _barrelOffsetTL2;
                        _barrelOffsetTL2 = _barrelOffsetTL;
                        _barrelOffsetTL = botl2;
                        int a2 = ammo2;
                        ammo2 = ammo;
                        ammo = a2;
                        AmmoType at2 = _ammoType2;
                        _ammoType2 = _ammoType;
                        _ammoType = at2;
						string s2 = _fireSound2;
						_fireSound2 = _fireSound;
						_fireSound = s2;
					}
				}
			}
		    base.Update();
		}
        public override void Thrown()
        {
            if (ammo == 0)
            {
                Sprite g2 = graphic2;
                graphic2 = graphic;
                graphic = g2;
                float la2 = loseAccuracy2;
                loseAccuracy2 = loseAccuracy;
                loseAccuracy = la2;
                float mal2 = maxAccuracyLost2;
                maxAccuracyLost2 = maxAccuracyLost;
                maxAccuracyLost = mal2;
                Vec2 botl2 = _barrelOffsetTL2;
                _barrelOffsetTL2 = _barrelOffsetTL;
                _barrelOffsetTL = botl2;
                int a2 = ammo2;
                ammo2 = ammo;
                ammo = a2;
                AmmoType at2 = _ammoType2;
                _ammoType2 = _ammoType;
                _ammoType = at2;
				string s2 = _fireSound2;
				_fireSound2 = _fireSound;
				_fireSound = s2;
            }
            base.Thrown();
        }		
	}
}
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Pistol")]
    public class PMR : Gun
    {
        int ammo2;
        AmmoType _ammoType2;
        Sprite graphic1;
        Sprite graphic2;
        Vec2 _barrelOffsetTL2;
		string _fireSound2;
        float loseAccuracy2;
        float maxAccuracyLost2;
        int _numBulletsPerFire2;
		bool switched = false;

        public PMR(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 30;
			ammo2 = 1;
            _ammoType = new AT9mm
            {
                range = 215f,
                accuracy = 0.875f,
                penetration = 1f
            };
            _ammoType2 = new AT9mm
            {
                range = 110f,
                accuracy = 0.35f,
                penetration = 1f,
                bulletSpeed = 50f
            };
            _numBulletsPerFire = 1;
            _numBulletsPerFire2 = 16;
            _type = "gun";
            graphic = new Sprite(GetPath("PMR30"));
            graphic1 = new Sprite(GetPath("PMR301"));
            graphic2 = new Sprite(GetPath("PMR302"));
            center = new Vec2(8f, 5f);
            collisionOffset = new Vec2(-8f, -5f);
            collisionSize = new Vec2(16f, 10f);
            _barrelOffsetTL = new Vec2(16f, 2.5f);
            _barrelOffsetTL2 = new Vec2(14f, 6f);
            _holdOffset = new Vec2(0f, 2f);
            _fireSound = GetPath("sounds/1.wav");
            _fireSound2 = "littleGun";
            _fullAuto = false;
            _fireWait = 0.5f;
            _kickForce = 0.55f;
            loseAccuracy = 0.025f;
            loseAccuracy2 = 0f;
            maxAccuracyLost = 0.15f;
            maxAccuracyLost2 = 0f;
            _editorName = "PMR30 With SG";
			weight = 1f;
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
                            graphic = new Sprite(GetPath("pmr301"));
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
    			        int fak = _numBulletsPerFire2;
                        _numBulletsPerFire2 = _numBulletsPerFire;
                        _numBulletsPerFire = fak;
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
    			int fak = _numBulletsPerFire2;
                _numBulletsPerFire2 = _numBulletsPerFire;
                _numBulletsPerFire = fak;
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
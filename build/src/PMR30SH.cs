using DuckGame;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Pistol")]
    // ReSharper disable once InconsistentNaming
    public class PMR : Gun
    {
        private int _ammo2;
        private AmmoType _ammoType2;
        private readonly Sprite _graphic1;
        private Sprite _graphic2;
        private Vec2 _barrelOffsetTl2;
        private string _fireSound2;
        private float _loseAccuracy2;
        private float _maxAccuracyLost2;
        private int _numBulletsPerFire2;
        private bool _switched;

        public PMR(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 30;
			_ammo2 = 1;
            _ammoType = new AT9mm {range = 215f, accuracy = 0.875f, penetration = 1f};
            _ammoType2 = new AT9mm {range = 110f, accuracy = 0.35f, penetration = 1f, bulletSpeed = 50f};
            _numBulletsPerFire = 1;
            _numBulletsPerFire2 = 16;
            _type = "gun";
            _graphic = new Sprite(GetPath("PMR30"));
            _graphic1 = new Sprite(GetPath("PMR301"));
            _graphic2 = new Sprite(GetPath("PMR302"));
            _center = new Vec2(8f, 5f);
            _collisionOffset = new Vec2(-8f, -5f);
            _collisionSize = new Vec2(16f, 10f);
            _barrelOffsetTL = new Vec2(16f, 2.5f);
            _barrelOffsetTl2 = new Vec2(14f, 6f);
            _holdOffset = new Vec2(0f, 2f);
            _fireSound = GetPath("sounds/1.wav");
            _fireSound2 = "littleGun";
            _fullAuto = false;
            _fireWait = 0.5f;
            _kickForce = 0.55f;
            loseAccuracy = 0.025f;
            _loseAccuracy2 = 0f;
            maxAccuracyLost = 0.15f;
            _maxAccuracyLost2 = 0f;
            _editorName = "PMR30 With SG";
			_weight = 1f;
        }
        public override void Update()
        {
            if (owner != null)
            {
                if (isServerForObject)
                {
                    if (duck.inputProfile.Pressed("QUACK"))
                    {
						if (!_switched)
						{
							_switched = true;
                            _graphic = _graphic1;
                        }
    			        var g2 = _graphic2;
                        _graphic2 = graphic;
                        _graphic = g2;
                        var la2 = _loseAccuracy2;
                        _loseAccuracy2 = loseAccuracy;
                        loseAccuracy = la2;
    			        var mal2 = _maxAccuracyLost2;
                        _maxAccuracyLost2 = maxAccuracyLost;
                        maxAccuracyLost = mal2;
    			        var fak = _numBulletsPerFire2;
                        _numBulletsPerFire2 = _numBulletsPerFire;
                        _numBulletsPerFire = fak;
                        var botl2 = _barrelOffsetTl2;
                        _barrelOffsetTl2 = _barrelOffsetTL;
                        _barrelOffsetTL = botl2;
                        var a2 = _ammo2;
                        _ammo2 = ammo;
                        ammo = a2;
                        var at2 = _ammoType2;
                        _ammoType2 = _ammoType;
                        _ammoType = at2;
						var s2 = _fireSound2;
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
                var g2 = _graphic2;
                _graphic2 = graphic;
                _graphic = g2;
                var la2 = _loseAccuracy2;
                _loseAccuracy2 = loseAccuracy;
                loseAccuracy = la2;
                var mal2 = _maxAccuracyLost2;
                _maxAccuracyLost2 = maxAccuracyLost;
                maxAccuracyLost = mal2;
    			var fak = _numBulletsPerFire2;
                _numBulletsPerFire2 = _numBulletsPerFire;
                _numBulletsPerFire = fak;
                var botl2 = _barrelOffsetTl2;
                _barrelOffsetTl2 = _barrelOffsetTL;
                _barrelOffsetTL = botl2;
                var a2 = _ammo2;
                _ammo2 = ammo;
                ammo = a2;
                var at2 = _ammoType2;
                _ammoType2 = _ammoType;
                _ammoType = at2;
				var s2 = _fireSound2;
				_fireSound2 = _fireSound;
				_fireSound = s2;
            }
            base.Thrown();
        }		
	}
}
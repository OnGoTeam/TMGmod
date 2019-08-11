using DuckGame;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Machinegun")]
    // ReSharper disable once InconsistentNaming
    public class scargl : Gun
    {
        private int _ammo2;
        private AmmoType _ammoType2;
        private readonly Sprite _graphic1;
        private Sprite _graphic2;
        private Vec2 _barrelOffsetTl2;
        private string _fireSound2;
        private float _loseAccuracy2;
        private float _maxAccuracyLost2;
        private bool _switched;

        public scargl (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 20;
			_ammo2 = 1;
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
            _graphic = new Sprite(GetPath("scargl"));
            _graphic1 = new Sprite(GetPath("scargl1"));
            _graphic2 = new Sprite(GetPath("scargl2"));
            _center = new Vec2(16.5f, 5f);
            _collisionOffset = new Vec2(-16.5f, -5f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 3f);
            _barrelOffsetTl2 = new Vec2(30f, 6.5f);
            _holdOffset = new Vec2(2f, 0f);
            _fireSound = GetPath("sounds/scar.wav");
            _fireSound2 = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 1.2f;
            _kickForce = 0.8f;
            loseAccuracy = 0.1f;
            _loseAccuracy2 = 0f;
            maxAccuracyLost = 0.2f;
            _maxAccuracyLost2 = 0f;
            _editorName = "SCAR-H With GL";
			_weight = 6f;

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
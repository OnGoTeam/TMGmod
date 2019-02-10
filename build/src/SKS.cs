using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle")]
    // ReSharper disable once InconsistentNaming
    public class SKS : BaseGun, ISpeedAccuracy
    {
        private int _patrons = 16;
        private int _bullets;
        public bool Stick;
        public StateBinding StickBinding = new StateBinding(nameof(Stick));

        public SKS (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 16;
            _ammoType = new AT9mm
            {
                range = 1400f,
                accuracy = 0.965f,
                penetration = 1f,
                bulletSpeed = 95f,
                bulletThickness = 1.5f
            };
            _type = "gun";
            _graphic = new Sprite(GetPath("SKS"));
            _center = new Vec2(30f, 6f);
            _collisionOffset = new Vec2(-30f, -6f);
            _collisionSize = new Vec2(60f, 12f);
            _barrelOffsetTL = new Vec2(53f, 5f);
            _holdOffset = new Vec2(11f, 0f);
            _fireSound = GetPath("sounds/scar.wav");
            _flare.center = new Vec2(0f, 5f);
            _fullAuto = false;
            _fireWait = 1.55f;
            _kickForce = 0.6f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.8f;
            _editorName = "SKS";
			_weight = 6f;
            MuAccuracySr = 1f;
            LambdaAccuracySr = 0.5f;
        }
        public override void Update()
        {
		    base.Update();
			if (ammo < 17)
			{
			    _patrons = ammo;	
			}

            if (duck == null) return;
            //else
            if (duck.inputProfile.Pressed("QUACK"))
            {
                if (ammo < 17)
                {
                    _patrons = ammo;
                    _bullets = _patrons + 20;
                    ammo += 20;
                }
                _flare = new SpriteMap(GetPath("takezis"), 4, 4)
                {
                    center = new Vec2(0f, 0f)
                };
                _ammoType = new ATNB();
                _fullAuto = false;
                _fireWait = 0.1f;
                _numBulletsPerFire = 1;
                _barrelOffsetTL = new Vec2(53f, 6f);
                _fireSound = "";
                loseAccuracy = 0f;
                maxAccuracyLost = 0f;
                _ammoType.bulletThickness = 0.1f;
                _kickForce = 0f;
                Stick = true;
                Fire();
            }
            if (duck.inputProfile.Down("QUACK"))
            {
                _holdOffset = new Vec2(17f, 0f);
                if (ammo < _bullets)
                {
                    ammo += 1;
                }
            }

            if (!duck.inputProfile.Released("QUACK")) return;
            //else
            ammo = _patrons;
            _ammoType = new AT9mm
            {
                range = 900f,
                accuracy = 0.965f,
                penetration = 1f,
                bulletSpeed = 95f,
                bulletThickness = 1.5f
            };
            _fullAuto = false;
            _fireWait = 1.3f;
            _numBulletsPerFire = 1;
            _barrelOffsetTL = new Vec2(53f, 3f);
            _fireSound = GetPath("sounds/scar.wav");
            _holdOffset = new Vec2(11f, 0f);
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.8f;
            _kickForce = 0.6f;
            _flare = new SpriteMap("smallFlare", 11, 10)
            {
                center = new Vec2(0f, 4f)
            };
            Stick = false;
        }	
        public override void Thrown()
        {
			if (ammo != 0)
			{           
						ammo = _patrons;
			    _ammoType = new AT9mm
			    {
			        range = 900f,
			        accuracy = 0.965f,
			        penetration = 1f,
			        bulletSpeed = 95f,
			        bulletThickness = 1.5f
			    };
			    _fullAuto = false;
                _fireWait = 1.3f;
				_numBulletsPerFire = 1;
                _barrelOffsetTL = new Vec2(53f, 3f);
                _fireSound = GetPath("sounds/scar.wav");
                _holdOffset = new Vec2(11f, 0f);
                loseAccuracy = 0.1f;
                maxAccuracyLost = 0.8f;
                _kickForce = 0.6f;
                _flare = new SpriteMap("smallFlare", 11, 10)
                {
                    center = new Vec2(0f, 4f)
                };
            }
			if (Stick && _patrons == 0)
			{
				ammo = 0;
			}
            base.Thrown();
        }

        public float MuAccuracySr { get; }
        public float LambdaAccuracySr { get; }
    }
}
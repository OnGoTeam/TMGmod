using DuckGame;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Sniper")]
    // ReSharper disable once InconsistentNaming
    public class ussrgun: Gun
    {
        public ussrgun (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 20;
            _ammoType = new ATMagnum {range = 1400f, accuracy = 0.92f, penetration = 1f};
            _type = "gun";
            _graphic = new Sprite(GetPath("SCAR20SSR"));
            _center = new Vec2(18f, 7f);
            _collisionOffset = new Vec2(-18f, -7f);
            _collisionSize = new Vec2(36f, 14f);
            _barrelOffsetTL = new Vec2(37f, 6f);
            _holdOffset = new Vec2(3f, -1f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 0.8f;
            _kickForce = 0.7f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _editorName = "SCAR SSR";
            laserSight = true;
            _laserOffsetTL = new Vec2(19f, 4f);
			_weight = 7f;
        }
          public override void Update()
        {
            if (_owner != null && _owner.height < 17f)
            {
                _kickForce = 0f;
				loseAccuracy = 0f;
                maxAccuracyLost = 0f;
				_graphic = new Sprite(GetPath("SCAR20SSRbipods"));
            }
            else
            {
                _kickForce = 0.7f;
                loseAccuracy = 0.1f;
                maxAccuracyLost = 0.3f;
				_graphic = new Sprite(GetPath("SCAR20SSR"));
            }
            base.Update();
        }
	}
}
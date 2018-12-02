using DuckGame;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun")]
    // ReSharper disable once InconsistentNaming
    public class SIX12 : Gun
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public readonly EditorProperty<bool> Laser = new EditorProperty<bool>(false, null, 0f, 1f, 1f);
		
		public SIX12 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 6;
            _ammoType = new ATMagnum
            {
                range = 225f,
                accuracy = 0.87f,
                penetration = 1f,
                bulletThickness = 0.5f
            };
            _numBulletsPerFire = 14;
            _type = "gun";
            graphic = new Sprite(GetPath("SIX12"));
            center = new Vec2(19.5f, 5f);
            collisionOffset = new Vec2(-19.5f, -5f);
            collisionSize = new Vec2(29f, 10f);
            _barrelOffsetTL = new Vec2(30f, 4.5f);
            _fireSound = "shotgunFire";
            _fullAuto = false;
            _fireWait = 1.7f;
            _kickForce = 1.4f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            laserSight = false;
            _laserOffsetTL = new Vec2(24f, 7f);
            _holdOffset = new Vec2(2f, 0f);
            _editorName = "SIX12";
			weight = 4f;
        }
        public override void Initialize()
        {
			if (!(Level.current is Editor))
            {
                if (Laser.value)
                {
                 laserSight = true;
				 graphic = new Sprite(GetPath("SIX12laser2"));
                 loseAccuracy = 0.5f;
                 maxAccuracyLost = 0.5f;
                }
            }
            base.Initialize();
        }
	}
}
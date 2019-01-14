using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Sniper")]
    // ReSharper disable once InconsistentNaming
    public class SNR22 : Gun
    {
        public SNR22 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 6;
            _ammoType = new ATSniper
            {
                range = 1200f,
                accuracy = 1f,
                penetration = 1f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("SNR22"));
            center = new Vec2(14f, 6f);
            collisionOffset = new Vec2(-14.5f, -5f);
            collisionSize = new Vec2(33f, 10f);
            _barrelOffsetTL = new Vec2(33f, 4f);
            _fireSound = GetPath("sounds/HeavySniper.wav");
            _fullAuto = false;
            _fireWait = 5f;
            _kickForce = 0.8f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(0f, 1f);
            laserSight = true;
            _laserOffsetTL = new Vec2(22f, 3.5f);
            _editorName = "Gepard Lynx";
			weight = 6f;
        }
          public override void Update()
        {
            if (_owner != null && _owner.height < 17f)
            {
                _kickForce = 0f;
				loseAccuracy = 0f;
                maxAccuracyLost = 0f;
				graphic = new Sprite(GetPath("SNR22bipods"));
            }
            else
            {
                _kickForce = 0.8f;
                loseAccuracy = 0.1f;
                maxAccuracyLost = 0.3f;
				graphic = new Sprite(GetPath("SNR22"));
            }
            base.Update();
        }
        public override void UpdateOnFire()
        {
            loseAccuracy += 0.15f;
            base.UpdateOnFire();
        }

    }
}
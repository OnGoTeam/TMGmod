using DuckGame;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Pistol")]
    // ReSharper disable once InconsistentNaming
    public class AF2011 : Gun
    {
        public AF2011 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 10;
            _ammoType = new ATMagnum
            {
                range = 150f,
                accuracy = 0.95f,
                penetration = 1f
            };
            _numBulletsPerFire = 2;
            _type = "gun";
            graphic = new Sprite(GetPath("AF2011"));
            center = new Vec2(7f, 4f);
            collisionOffset = new Vec2(-8f, -4f);
            collisionSize = new Vec2(16f, 10f);
            _barrelOffsetTL = new Vec2(16f, 1f);
            _fireSound = "pistolFire";
            _fullAuto = false;
            _fireWait = 1.2f;
            _kickForce = 0f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            _holdOffset = new Vec2(-1f, 1f);
            _editorName = "AF-2011";
			weight = 2.5f;
        }

        public override void Fire()
        {
            
            
            if (ammo > 0)
            {
                _ammoType.accuracy = _ammoType.accuracy - 0.05f;
            }
            base.Fire();
        }

        public override void Update()
        {
            if (_ammoType.accuracy + 0.01f < 0.95f)
            {
                _ammoType.accuracy = _ammoType.accuracy + 0.003f;
                base.Update();
            }
            else
            {
                _ammoType.accuracy = 0.95f;
            }
            base.Update();
        }

    }
}

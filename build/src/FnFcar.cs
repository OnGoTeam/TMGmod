using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Sniper")]
    public class FnFcar: Gun
    {
        public FnFcar (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 14;
            _ammoType = new ATMagnum
            {
                range = 800f,
                accuracy = 1f,
                penetration = 1f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("FCARNoBipods"));
            center = new Vec2(18f, 7f);
            collisionOffset = new Vec2(-18f, -7f);
            collisionSize = new Vec2(36f, 14f);
            _barrelOffsetTL = new Vec2(37f, 6f);
            _holdOffset = new Vec2(3f, -1f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 0.775f;
            _kickForce = 0.9f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.45f;
            _editorName = "FN FCAR";
            laserSight = true;
            _laserOffsetTL = new Vec2(19f, 4f);
			weight = 7f;
        }

        public override void Update()
        {
            if (duck != null && duck.height < 17f)
            {
                _kickForce = 0f;
				loseAccuracy = 0f;
                maxAccuracyLost = 0f;
				graphic = new Sprite(GetPath("FCARBipods"));
            }
            else
            {
                _kickForce = 0.9f;
                loseAccuracy = 0.1f;
                maxAccuracyLost = 0.45f;
				graphic = new Sprite(GetPath("FCARNoBipods"));
            }
            base.Update();
        }
        public override void OnHoldAction()
        {
            if (_kickForce > 0f && _ammoType.accuracy > 0.1f) { _ammoType.accuracy -= 0.02f; } else { _ammoType.accuracy -= 0.0005f; }

            base.OnHoldAction();
        }
        public override void OnReleaseAction()
        {
            if (_ammoType.accuracy < 1f) _ammoType.accuracy += 0.1f;
            base.OnReleaseAction();
        }
    }
}
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun")]
    public class DragoShot : BaseBurst
    {
        public float Counter;
        public StateBinding CounterBinding = new StateBinding(nameof(Counter));
        private const float Step = 0.01f;
        private const float TimeToHappend = 1f;
        public bool LoockerOfSound;
        public StateBinding LoockerOfSoundBinding = new StateBinding(nameof(LoockerOfSound));
        public DragoShot (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 16;
            _ammoType = new ATMagnum
            {
                range = 120f,
                accuracy = 0.7f,
                penetration = 2f,
                bulletThickness = 0.8f
            };
            _numBulletsPerFire = 8;
            _type = "gun";
            _graphic = new Sprite(GetPath("DragoShot"));
            _center = new Vec2(17f, 7f);
            _collisionOffset = new Vec2(-14f, -7f);
            _collisionSize = new Vec2(29f, 11f);
            _barrelOffsetTL = new Vec2(30f, 2.5f);
            _fireSound = "shotgunFire";
            _fullAuto = false;
            _fireWait = 1.5f;
            _kickForce = 5.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.4f;
            laserSight = true;
            _laserOffsetTL = new Vec2(23f, 3f);
            _holdOffset = new Vec2(0f, 3f);
            _editorName = "DragoShot";
			_weight = 5f;
            DeltaWait = 0.15f;
            BurstNum = 1;
        }
        public override void OnPressAction()
        {
            //Nothing Happend
        }
        public override void OnHoldAction()
        {
            if (ammo != 0 && Counter <= TimeToHappend) Counter += Step;
            base.OnHoldAction();
        }
        public override void OnReleaseAction()
        {
            Counter = 0f;
            base.OnReleaseAction();
            if (duck != null) Fire();
            LoockerOfSound = false;
        }
        public override void Update()
        {
            if (Counter >= TimeToHappend)
            {
                if (!LoockerOfSound) SFX.Play("woodHit");
                LoockerOfSound = true;
                _ammoType.range = 170f;
                _ammoType.accuracy = 0.9f;
                maxAccuracyLost = 0.1f;
                _kickForce = 3f;
                BurstNum = 4;
            }
            else
            {
                _ammoType.range = 120f;
                _ammoType.accuracy = 0.7f;
                maxAccuracyLost = 0.4f;
                _kickForce = 5.5f;
                BurstNum = 1;
            }
            base.Update();
        }
    }
}

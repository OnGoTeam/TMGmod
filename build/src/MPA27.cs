using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|SMG")]
    // ReSharper disable once InconsistentNaming
    public class MPA27 : Gun
    {

        int _burstNumB;
        readonly int _burstValue;
        float _bw = 5.1f;
        public MPA27(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new AT9mm
            {
                range = 200f,
                accuracy = 0.75f,
                penetration = 1f,
                bulletSpeed = 55f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("MPA27"));
            center = new Vec2(6f, 7f);
            collisionOffset = new Vec2(-8f, -7f);
            collisionSize = new Vec2(16f, 14f);
            _barrelOffsetTL = new Vec2(16f, 3f);
            _fireSound = GetPath("sounds/2.wav");
            _fullAuto = true;
            _fireWait = 0.36f;
            _kickForce = 0f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.44f;
            _holdOffset = new Vec2(-1f, 3f);
            _editorName = "Vista";
            _burstValue = 3;
            weight = 2f;
        }

        public override void Fire()
        {
            // base.Fire();
        }
        public override void Update()
        {
            //object obj;

            if (_burstNumB > 0 && _bw > 0.1f)
            {
                base.Fire();
                _burstNumB = _burstNumB - 1;
                _bw = 0;
            }
            else
            {
                _bw = _bw + 0.1f;
            }
            base.Update();
        }
        public override void OnPressAction()
        {
            if (_bw > 1f)
            {
                _bw = 0.3f;
                _burstNumB = _burstValue;
            }
        }
    }
}

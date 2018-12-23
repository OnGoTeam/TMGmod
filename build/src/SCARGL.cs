using DuckGame;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    // ReSharper disable once InconsistentNaming
    public class ScarGL : Gun
    {
        private int _mode;
        private readonly int[] _ammom = {20, 1};

        private readonly AmmoType[] _ammoTypem =
        {
            new ATMagnum
            {
                range = 900f,
                accuracy = 0.9f,
                penetration = 1f,
                bulletSpeed = 35f,
                barrelAngleDegrees = 0f
            },
            new ATGrenade
            {
            range = 2500f,
            accuracy = 1f,
            bulletSpeed = 18f,
            barrelAngleDegrees = -7.5f
            }
        };

        private readonly Sprite[] _graphicm = {new Sprite(), new Sprite(), new Sprite()};
        private readonly Vec2[] _barrelOffsetTLm = {new Vec2(33f, 3f), new Vec2(30f, 6.5f)};
        private readonly string[] _fireSoundm = {"sounds/1.wav", "deepMachineGun"};
        private readonly float[] _loseAccuracym = {.01f, 0f};
        private readonly float[] _maxAccuracyLostm = {.2f, 0f};
        private bool _switched;
        private bool _changed;

        public ScarGL (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 20;
            _ammoType = new ATMagnum
            {
                range = 900f,
                accuracy = 0.9f,
                penetration = 1f,
                bulletSpeed = 35f,
                barrelAngleDegrees = 0f
            };
            _type = "gun";
            _graphicm[0] = new Sprite(GetPath("scargl1"));
            _graphicm[1] = new Sprite(GetPath("scargl2"));
            _graphicm[2] = new Sprite(GetPath("scargl"));
            center = new Vec2(16.5f, 5f);
            collisionOffset = new Vec2(-16.5f, -5f);
            collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 3f);
            _holdOffset = new Vec2(2f, 0f);
            _fireSound = GetPath("sounds/scar.wav");
            _fireSoundm[0] = _fireSound;
            _fullAuto = true;
            _fireWait = 1.2f;
            _kickForce = 0.8f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.2f;
            _editorName = "SCAR-H With GL";
			weight = 6f;
            graphic = _graphicm[_switched ? _mode : 2];
        }

        private void UpdateMode()
        {
            graphic = _graphicm[_switched ? _mode : 2];
            _ammoType = _ammoTypem[_mode];
            _barrelOffsetTL = _barrelOffsetTLm[_mode];
            _fireSound = _fireSoundm[_mode];
            loseAccuracy = _loseAccuracym[_mode];
            maxAccuracyLost = _maxAccuracyLostm[_mode];
        }

        public override void Update()
        {
            if (owner != null)
            {
                if (duck.inputProfile.Down("QUACK"))
                {
                    if (!_changed)
                    {
                        _mode = 1 - _mode;
                        _changed = true;
                        _switched = true;
                    }
                }
                else
                {
                    _changed = false;
                }
            }
            UpdateMode();
            base.Update();
        }
        public override void Fire()
        {
            ammo = _ammom[_mode];
            base.Fire();
            _ammom[_mode] = ammo;
            ammo = _ammom[0] + _ammom[1];
        }
    }
}
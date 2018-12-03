using DuckGame;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Pistol")]
    // ReSharper disable once InconsistentNaming
    public class PMR : Gun
    {
        private int _mode;
        private int[] _ammom = {30, 1};
        private AmmoType[] _ammoTypem =
        {
            new AT9mm
            {
                range = 215f,
                accuracy = 0.875f,
                penetration = 1f
            },
            new AT9mm
            {
                range = 110f,
                accuracy = 0.35f,
                penetration = 1f,
                bulletSpeed = 50f
            }
        };

        private readonly Sprite[] _graphicm = {new Sprite(), new Sprite(), new Sprite()};
        private readonly Vec2[] _barrelOffsetTLm = {new Vec2(16f, 2.5f), new Vec2(14f, 6f)};
        private readonly string[] _fireSoundm = {"sounds/1.wav", "littleGun"};
        private readonly float[] _loseAccuracym = {.025f, 0f};
        private readonly float[] _maxAccuracyLostm = {.15f, 0f};
        private readonly int[] _numBulletsPerFirem = {1, 16};
        private bool _switched;
        private bool _changed;

        public PMR(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new AT9mm
            {
                range = 215f,
                accuracy = 0.875f,
                penetration = 1f
            };
            _numBulletsPerFire = 1;
            _type = "gun";
            //graphic = new Sprite(GetPath("PMR30"));
            _graphicm[0] = new Sprite(GetPath("PMR301"));
            _graphicm[1] = new Sprite(GetPath("PMR302"));
            _graphicm[2] = new Sprite(GetPath("PMR30"));
            center = new Vec2(8f, 5f);
            collisionOffset = new Vec2(-8f, -5f);
            collisionSize = new Vec2(16f, 10f);
            _barrelOffsetTL = new Vec2(16f, 2.5f);
            _holdOffset = new Vec2(0f, 2f);
            _fireSound = GetPath("sounds/1.wav");
            _fireSoundm[0] = _fireSound;
            _fullAuto = false;
            _fireWait = 0.5f;
            _kickForce = 0.55f;
            loseAccuracy = 0.025f;
            maxAccuracyLost = 0.15f;
            _editorName = "PMR30 With SG";
			weight = 1f;
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
            _numBulletsPerFire = _numBulletsPerFirem[_mode];
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
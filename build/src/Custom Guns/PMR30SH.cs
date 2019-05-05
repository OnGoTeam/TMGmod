using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Custom_Guns
{
    [EditorGroup("TMG|Handgun|Custom")]
    // ReSharper disable once InconsistentNaming
    public class PMR : BaseGun, IAmHg
    {
        public int Mode;
        public StateBinding ModeBinding = new StateBinding(nameof(Mode));
        public int Ammom0 = 30;
        public int Ammom1 = 1;
        public int[] Ammom
        {
            get => new[] {Ammom0, Ammom1};
            set {
                Ammom0 = value[0];
                Ammom1 = value[1];
            }
        }
        public StateBinding Ammom0Binding = new StateBinding(nameof(Ammom0));
        public StateBinding Ammom1Binding = new StateBinding(nameof(Ammom1));
        private readonly AmmoType[] _ammoTypem =
        {
            new AT9mm
            {
                range = 125f,
                accuracy = 0.75f,
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
        private readonly float[] _loseAccuracym = {.1f, 0f};
        private readonly float[] _maxAccuracyLostm = {.55f, 0f};
        private readonly int[] _numBulletsPerFirem = {1, 16};
        private bool _switched;

        public PMR(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new AT9mm
            {
                range = 125f,
                accuracy = 0.75f,
                penetration = 1f
            };
            _numBulletsPerFire = 1;
            _type = "gun";
            //graphic = new Sprite(GetPath("PMR30"));
            _graphicm[0] = new Sprite(GetPath("PMR301"));
            _graphicm[1] = new Sprite(GetPath("PMR302"));
            _graphicm[2] = new Sprite(GetPath("PMR30"));
            _center = new Vec2(8f, 5f);
            _collisionOffset = new Vec2(-8f, -5f);
            _collisionSize = new Vec2(16f, 10f);
            _barrelOffsetTL = new Vec2(16f, 2.5f);
            _holdOffset = new Vec2(0f, 2f);
            _fireSound = GetPath("sounds/1.wav");
            _fireSoundm[0] = _fireSound;
            _fullAuto = false;
            _fireWait = 0.5f;
            _kickForce = 0.55f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.55f;
            _editorName = "PMR30 With SG";
			_weight = 1f;
            _graphic = _graphicm[_switched ? Mode : 2];
        }

        private void UpdateMode()
        {
            graphic = _graphicm[_switched ? Mode : 2];
            _ammoType = _ammoTypem[Mode];
            _barrelOffsetTL = _barrelOffsetTLm[Mode];
            _fireSound = _fireSoundm[Mode];
            loseAccuracy = _loseAccuracym[Mode];
            maxAccuracyLost = _maxAccuracyLostm[Mode];
            _numBulletsPerFire = _numBulletsPerFirem[Mode];
        }

        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                Mode = 1 - Mode;
                _switched = true;
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }
            UpdateMode();
            base.Update();
		}
        public override void Fire()
        {
            ammo = Ammom[Mode];
            base.Fire();
            if (Mode == 0)
            {
                Ammom0 = ammo;
            }
            else
            {
                Ammom1 = ammo;
            }
            ammo = Ammom[0] + Ammom[1];
        }		
	}
}
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    [PublicAPI]
    // ReSharper disable once InconsistentNaming
    public class ScarGL : Gun, IAmAr
    {
        public int Mode;
        public StateBinding ModeBinding = new StateBinding(nameof(Mode));
        public int Ammom0 = 20;
        public int Ammom1 = 1;
        public int[] Ammom
        {
            get => new[] { Ammom0, Ammom1 };
            set
            {
                Ammom0 = value[0];
                Ammom1 = value[1];
            }
        }
        public StateBinding Ammom0Binding = new StateBinding(nameof(Ammom0));
        public StateBinding Ammom1Binding = new StateBinding(nameof(Ammom1));
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
            _center = new Vec2(16.5f, 5f);
            _collisionOffset = new Vec2(-16.5f, -5f);
            _collisionSize = new Vec2(33f, 11f);
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
			_weight = 6f;
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
        }

        public override void Update()
        {
            if (infiniteAmmoVal) Ammom[0] = 99;
            if (duck != null)
            {
                if (duck.inputProfile.Pressed("QUACK"))
                {
                    Mode = 1 - Mode;
                    _switched = true;
                    SFX.Play(GetPath("sounds/tuduc.wav"));
                }
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
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic|Custom")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class ScarGL : BaseGun, IAmAr
    {
        private readonly AmmoType[] _ammoTypem =
        {
            new AT556NATO
            {
                range = 400f,
                accuracy = 0.9f,
                penetration = 1f,
                bulletSpeed = 35f,
                barrelAngleDegrees = 0f,
            },
            new ATGrenade
            {
                range = 2500f,
                accuracy = 1f,
                bulletSpeed = 18f,
                barrelAngleDegrees = -7.5f,
            },
        };

        private readonly Vec2[] _barrelOffsetTLm = { new Vec2(33f, 3f), new Vec2(30f, 6.5f) };
        private readonly string[] _fireSoundm = { "sounds/1.wav", "deepMachineGun" };
        private readonly SpriteMap[] _flarem;

        private readonly Sprite[] _graphicm = { new Sprite(), new Sprite(), new Sprite() };
        private readonly float[] _loseAccuracym = { .1f, 0f };
        private readonly float[] _maxAccuracyLostm = { .45f, 0f };
        private bool _switched;

        public int Ammom0 = 20;

        [UsedImplicitly] public StateBinding Ammom0Binding = new StateBinding(nameof(Ammom0));

        public int Ammom1 = 1;

        [UsedImplicitly] public StateBinding Ammom1Binding = new StateBinding(nameof(Ammom1));

        public int Mode;

        [UsedImplicitly] public StateBinding ModeBinding = new StateBinding(nameof(Mode));

        public ScarGL(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 20;
            _ammoType = new AT556NATO
            {
                range = 400f,
                accuracy = 0.9f,
                bulletSpeed = 35f,
                barrelAngleDegrees = 0f,
            };
            MaxAccuracy = 0.9f;
            _type = "gun";
            _graphicm[0] = new Sprite(GetPath("scargl1"));
            _graphicm[1] = new Sprite(GetPath("scargl2"));
            _graphicm[2] = new Sprite(GetPath("scargl"));
            _center = new Vec2(16.5f, 5f);
            _collisionOffset = new Vec2(-16.5f, -5f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 3f);
            _flarem = new[]
            {
                new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
                {
                    center = new Vec2(0.0f, 5f),
                },
                new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
                {
                    center = new Vec2(0.0f, 5f),
                },
            };
            _flare = _flarem[Mode];
            _holdOffset = new Vec2(2f, 0f);
            _fireSound = GetPath("sounds/scar.wav");
            _fireSoundm[0] = _fireSound;
            _fullAuto = true;
            _fireWait = 1f;
            _kickForce = 2.6f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.45f;
            _editorName = "SCAR-H With GL";
            _weight = 6f;
            _graphic = _graphicm[_switched ? Mode : 2];
        }

        private int[] Ammom => new[] { Ammom0, Ammom1 };

        private void UpdateMode()
        {
            graphic = _graphicm[_switched ? Mode : 2];
            _ammoType = _ammoTypem[Mode];
            _barrelOffsetTL = _barrelOffsetTLm[Mode];
            _fireSound = _fireSoundm[Mode];
            loseAccuracy = _loseAccuracym[Mode];
            maxAccuracyLost = _maxAccuracyLostm[Mode];
            _flare = _flarem[Mode];
        }

        public override void Update()
        {
            if (infiniteAmmoVal) Ammom0 = 99;
            if (duck != null)
                if (duck.inputProfile.Pressed("QUACK"))
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
                Ammom0 = ammo;
            else
                Ammom1 = ammo;

            ammo = Ammom[0] + Ammom[1];
        }
    }
}

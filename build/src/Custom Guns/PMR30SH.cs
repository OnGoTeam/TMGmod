using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod.Custom_Guns
{
    [EditorGroup("TMG|Handgun|Custom")]
    // ReSharper disable once InconsistentNaming
    public class PMR : BaseGun, IAmHg
    {
        private readonly AmmoType[] _ammoTypem =
        {
            new ATPMR30(),
            new AT12Gauge
            {
                range = 110f,
                accuracy = 0.35f,
                penetration = 2f,
                bulletSpeed = 50f,
                BulletDamage = 15f
            }
        };

        private readonly Vec2[] _barrelOffsetTLm = { new Vec2(16f, 2f), new Vec2(14f, 6f) };
        private readonly string[] _fireSoundm = { "sounds/1.wav", "littleGun" };

        private readonly Sprite[] _graphicm = { new Sprite(), new Sprite(), new Sprite() };
        private readonly float[] _loseAccuracym = { .1f, 0f };
        private readonly float[] _maxAccuracyLostm = { .55f, 0f };
        private readonly int[] _numBulletsPerFirem = { 1, 16 };
        private bool _switched;
        [UsedImplicitly] public int Ammom0 = 30;
        [UsedImplicitly] public StateBinding Ammom0Binding = new StateBinding(nameof(Ammom0));
        [UsedImplicitly] public int Ammom1 = 1;
        [UsedImplicitly] public StateBinding Ammom1Binding = new StateBinding(nameof(Ammom1));
        [UsedImplicitly] public int Mode;
        [UsedImplicitly] public StateBinding ModeBinding = new StateBinding(nameof(Mode));

        public PMR(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new ATPMR30();
            _numBulletsPerFire = 1;
            _type = "gun";
            _graphicm[0] = new Sprite(GetPath("PMR301"));
            _graphicm[1] = new Sprite(GetPath("PMR302"));
            _graphicm[2] = new Sprite(GetPath("PMR300"));
            _center = new Vec2(8f, 5f);
            _collisionOffset = new Vec2(-8f, -5f);
            _collisionSize = new Vec2(16f, 10f);
            _barrelOffsetTL = new Vec2(16f, 2f);
            _holdOffset = new Vec2(0f, 2f);
            ShellOffset = new Vec2(-2f, -3f);
            _fireSound = GetPath("sounds/1.wav");
            _fireSoundm[0] = _fireSound;
            _fullAuto = false;
            _fireWait = 0.5f;
            _kickForce = 1.67f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.5f;
            _editorName = "PMR30 Shotgunned";
            _weight = 1f;
            _graphic = _graphicm[_switched ? Mode : 2];
        }

        [UsedImplicitly]
        public int[] Ammom
        {
            get => new[] { Ammom0, Ammom1 };
            set
            {
                Ammom0 = value[0];
                Ammom1 = value[1];
            }
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
                Ammom0 = ammo;
            else
                Ammom1 = ammo;
            ammo = Ammom[0] + Ammom[1];
        }
    }
}

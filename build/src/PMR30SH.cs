using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Custom")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class PMR : BaseGun, IAmHg, IHaveAllowedSkins
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
                DamageMean = 15f,
            },
        };

        private const int NonSkinFrames = 3;
        private readonly SpriteMap _sprite;

        private readonly Vec2[] _barrelOffsetTLm = { new Vec2(16f, 2f), new Vec2(14f, 6f) };
        private readonly string[] _fireSoundm = { "sounds/1.wav", "littleGun" };

        private readonly float[] _loseAccuracym = { .1f, 0f };
        private readonly float[] _maxAccuracyLostm = { .55f, 0f };
        private readonly int[] _numBulletsPerFirem = { 1, 16 };

        public PMR(float xval, float yval)
            : base(xval, yval)
        {
            _ammoType = _ammoTypem[0];
            _numBulletsPerFire = 1;

            _sprite = new SpriteMap(GetPath("PMR30Custom"), 16, 10);
            _graphic = _sprite;
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
            Compose(
                new SwitchingModes(
                    this,
                    new[] { 30, 1 },
                    mode =>
                    {
                        FrameId = FrameId % 10 + 10 * (1 + mode);
                        _ammoType = _ammoTypem[mode];
                        _barrelOffsetTL = _barrelOffsetTLm[mode];
                        _fireSound = _fireSoundm[mode];
                        loseAccuracy = _loseAccuracym[mode];
                        maxAccuracyLost = _maxAccuracyLostm[mode];
                        _numBulletsPerFire = _numBulletsPerFirem[mode];
                    },
                    () => FrameId %= 10
                )
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}

using System;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class ScarGL : BaseGun, IAmAr
    {
        public ScarGL(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "SCAR-H With GL";
            NonSkinFrames = 3;
            Smap = new SpriteMap(GetPath("scargl"), 33, 10);
            _center = new Vec2(17f, 5f);
            _collisionOffset = new Vec2(-17f, -5f);
            _collisionSize = new Vec2(33f, 11f);
            _holdOffset = new Vec2(2f, 0f);
            ShellOffset = new Vec2(-2f, -1f);
            var barrelOffsetTLm = new[] { new Vec2(33f, 3f), new Vec2(30f, 6.5f) };
            var flarem = new[]
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

            var ammom = new[] { 20, 1 };
            var ammoTypem = new AmmoType[]
            {
                new AT556NATO
                {
                    range = 340f,
                    accuracy = 0.9f,
                    penetration = 1f,
                    bulletSpeed = 35f,
                },
                new ATScarGrenade(),
            };
            _fullAuto = true;
            _fireWait = 1f;
            var loseAccuracym = new[] { .1f, 0f };
            var maxAccuracyLostm = new[] { .45f, 0f };
            _kickForce = 2.6f;
            _weight = 6f;

            var fireSoundm = new[] { GetPath("sounds/new/scar.wav"), "deepMachineGun" };

            Compose(
                new SwitchingAmmoModes(
                    this,
                    ammom,
                    mode =>
                    {
                        NonSkin = 1 + mode;
                        SetAmmoType(ammoTypem[mode]);
                        _barrelOffsetTL = barrelOffsetTLm[mode];
                        _fireSound = fireSoundm[mode];
                        loseAccuracy = loseAccuracym[mode];
                        maxAccuracyLost = maxAccuracyLostm[mode];
                        _flare = flarem[mode];
                        handOffset = mode * new Vec2(6f, 0f);
                    },
                    () => NonSkin = 0
                ).Animated(
                    (frames, mode) =>
                        handOffset = Math.Abs(mode - frames / (float)FramesToSwitchTo(mode)) * new Vec2(6f, 0f),
                    FramesToSwitchTo
                )
            );
        }

        private static int FramesToSwitchTo(int mode) => 10;
    }
}

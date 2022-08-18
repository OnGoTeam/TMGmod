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
        public PMR(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "PMR30 Shotgunned";

            NonSkinFrames = 3;
            Smap = new SpriteMap(GetPath("PMR30Custom"), 16, 10);
            _center = new Vec2(8f, 5f);
            _collisionOffset = new Vec2(-8f, -5f);
            _collisionSize = new Vec2(16f, 10f);
            _holdOffset = new Vec2(0f, 2f);
            var barrelOffsetTLm = new[] { new Vec2(16f, 2f), new Vec2(14f, 6f) };
            ShellOffset = new Vec2(-5f, -2f);

            var ammom = new[] { 30, 1 };
            var ammoTypem = new AmmoType[]
            {
                new ATPMR30(),
                new ATPMR30SH(),
            };
            var numBulletsPerFirem = new[] { 1, 16 };
            _fireWait = 0.5f;
            var loseAccuracym = new[] { .1f, 0f };
            var maxAccuracyLostm = new[] { .55f, 0f };
            _kickForce = 1.67f;
            _weight = 1f;

            var fireSoundm = new[] { GetPath("sounds/new/Alep30.wav"), "littleGun" };

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
                        _numBulletsPerFire = numBulletsPerFirem[mode];
                    },
                    () => NonSkin = 0
                )
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 5, 9 });
    }
}

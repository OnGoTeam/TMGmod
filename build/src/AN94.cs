using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Kforce;
using TMGmod.Core.Modifiers.Syncing;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Burst")]
    // ReSharper disable once InconsistentNaming
    public class AN94 : BaseGun, IAmAr, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public AN94(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "AN94";
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("AN94"), 33, 9);
            _center = new Vec2(16f, 5f);
            _collisionOffset = new Vec2(-15f, -5f);
            _collisionSize = new Vec2(33f, 9f);
            _barrelOffsetTL = new Vec2(33f, 2.5f);
            _flare = FrameUtils.FlareOnePixel1();
            _holdOffset = new Vec2(2f, 2f);
            ShellOffset = new Vec2(0f, -3f);
            ammo = 30;
            SetAmmoType<AT545NATO>(.87f);
            _fireSound = GetPath("sounds/new/AutomaticRifle-1.wav");
            _fullAuto = false;
            _fireWait = 1.5f;
            _kickForce = 0.5f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.45f;
            _weight = 4.5f;
            laserSight = false;
            _laserOffsetTL = new Vec2(24f, 1.5f);
            var laserProperty = new SynchronizedProperty<bool>(
                () => laserSight,
                (old, value) =>
                {
                    if (value != old)
                        SFX.Play(GetPath("sounds/tuduc.wav"));
                    loseAccuracy = value ? .1f : .15f;
                    _fireWait = value ? 2.5f : 1.5f;
                    maxAccuracyLost = value ? .1f : .45f;
                    NonSkin = value ? 1 : 0;
                    laserSight = value;
                }
            );
            Compose(
                new HSpeedKforce(this, hspeed => hspeed > .1f, kforce => kforce + 1.5f),
                laserProperty,
                new Quacking(this, true, true, laserProperty.Flip, "laser", () => laserOffset)
            );
            ComposeSimpleBurst(2, .07f);
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 280f;
            _ammoType.bulletSpeed = 60f;
            base.OnInitialize();
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 6, 7 });
    }
}

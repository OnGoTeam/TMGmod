using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Modifiers.Kforce;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Burst")]
    // ReSharper disable once InconsistentNaming
    public class AN94 : BaseGun, IAmAr, IHaveAllowedSkins
    {
        [UsedImplicitly] public StateBinding StockBinding = new StateBinding(nameof(Laserrod));

        public AN94(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "AN94";
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("AN94"), 33, 9);
            _center = new Vec2(16f, 5f);
            _collisionOffset = new Vec2(-15f, -5f);
            _collisionSize = new Vec2(33f, 9f);
            _barrelOffsetTL = new Vec2(33f, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(1f, 2f);
            ShellOffset = new Vec2(0f, -3f);
            ammo = 30;
            SetAmmoType<AT545NATO>(.87f);
            _fireSound = "deepMachineGun2";
            _fullAuto = false;
            _fireWait = 1.5f;
            _kickForce = 0.5f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.45f;
            _weight = 4.5f;
            laserSight = false;
            _laserOffsetTL = new Vec2(30f, 2.5f);
            Compose(
                new HSpeedKforce(this, hspeed => hspeed > .1f, kforce => kforce + 1.5f)
            );
            ComposeSimpleBurst(2, .07f);
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 260f;
            _ammoType.bulletSpeed = 60f;
            base.OnInitialize();
        }

        [UsedImplicitly]
        public bool Laserrod
        {
            get => NonSkin == 0;
            set
            {
                if (value)
                {
                    loseAccuracy = 0.15f;
                    _fireWait = 1.5f;
                    maxAccuracyLost = 0.45f;
                    NonSkin = 0;
                    laserSight = false;
                }
                else
                {
                    loseAccuracy = 0.1f;
                    _fireWait = 2.5f;
                    maxAccuracyLost = 0.1f;
                    NonSkin = 1;
                    laserSight = true;
                }
            }
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 6, 7 });

        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                Laserrod = !Laserrod;
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }

            base.Update();
        }
    }
}

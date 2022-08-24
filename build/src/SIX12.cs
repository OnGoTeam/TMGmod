using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun|Semi-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class SIX12 : BaseGun, IHaveAllowedSkins, IAmSg, I5
    {
        public SIX12(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "SIX12";
            ammo = 6;
            SetAmmoType<AT12Gauge>(.87f);
            _numBulletsPerFire = 14;
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("SIX12"), 29, 10);
            _center = new Vec2(19f, 5f);
            _collisionOffset = new Vec2(-19f, -5f);
            _collisionSize = new Vec2(29f, 10f);
            _barrelOffsetTL = new Vec2(29f, 3.5f);
            _fireSound = GetPath("sounds/new/SIX12.wav");
            _flare = new SpriteMap(GetPath("FlareBase2"), 13, 10)
            {
                center = new Vec2(0f, 4.5f),
            };
            _fullAuto = false;
            _fireWait = 2f;
            _kickForce = 5f;
            loseAccuracy = 0.3f;
            maxAccuracyLost = 0.4f;
            laserSight = false;
            _laserOffsetTL = new Vec2(24f, 7.5f);
            _holdOffset = new Vec2(1f, 0f);
            _weight = 4f;
            Compose(new Quacking(this, true, true, () => LaserSight = !LaserSight));
        }

        protected override Vec2 HintOffset() => laserOffset;

        public override string HintMessage => "laser";

        protected override void OnInitialize()
        {
            _ammoType.range = 165f;
            _ammoType.penetration = 1f;
            _ammoType.bulletThickness = .5f;
            base.OnInitialize();
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 3, 4, 5, 6, 7 });

        public bool LaserSight
        {
            get => laserSight;
            set
            {
                if (value != LaserSight)
                    SFX.Play(GetPath("sounds/tuduc.wav"));
                laserSight = value;
                NonSkin = value ? 1 : 0;
                loseAccuracy = value ? .5f : .3f;
                maxAccuracyLost = value ? .5f : .4f;
            }
        }
        [UsedImplicitly] public StateBinding LaserBinding = new StateBinding(nameof(LaserSight));

        public override void Reload(bool shell = true) => base.Reload(false);
    }
}

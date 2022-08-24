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
    public class SIX12S : BaseGun, IHaveAllowedSkins, IAmSg, I5
    {
        public SIX12S(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "SIX12 Silenced";
            ammo = 6;
            SetAmmoType<ATSIX12S>();
            _numBulletsPerFire = 14;
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("SIX12S"), 29, 10);
            _center = new Vec2(19f, 5f);
            _collisionOffset = new Vec2(-19f, -5f);
            _collisionSize = new Vec2(29f, 10f);
            _barrelOffsetTL = new Vec2(29f, 4f);
            _fireSound = GetPath("sounds/new/SIX12-Sv2.wav");
            _fullAuto = false;
            _fireWait = 2f;
            _kickForce = 5.5f;
            loseAccuracy = 0.3f;
            maxAccuracyLost = 0.5f;
            laserSight = false;
            _laserOffsetTL = new Vec2(24f, 7.5f);
            _holdOffset = new Vec2(1f, 0f);
            _weight = 4f;
            Compose(new Quacking(this, true, true, () => LaserSight = !LaserSight));
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 9 });

        public bool LaserSight
        {
            get => laserSight;
            set
            {
                if (value != LaserSight)
                    SFX.Play(GetPath("sounds/tuduc.wav"));
                laserSight = value;
                NonSkin = value ? 1 : 0;
                loseAccuracy = value ? .45f : .3f;
            }
        }
        [UsedImplicitly] public StateBinding LaserBinding = new StateBinding(nameof(LaserSight));

        public override void Reload(bool shell = true) => base.Reload(false);
    }
}

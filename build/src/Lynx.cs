using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Semi-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class Lynx : BaseGun, IAmDmr, IHaveAllowedSkins, I5
    {
        private readonly LoseAccuracy _loseAccuracy = new LoseAccuracy(0.6f, 0.01f, 1f);

        [UsedImplicitly]
        public Lynx(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Gepard Lynx";
            ammo = 6;
            SetAmmoType<ATLynx>();
            MinAccuracy = 0.3f;
            NonSkinFrames = 4;
            Smap = new SpriteMap(GetPath("Lynx"), 31, 11);
            _center = new Vec2(15f, 6f);
            _collisionOffset = new Vec2(-15f, -6f);
            _collisionSize = new Vec2(31f, 11f);
            _barrelOffsetTL = new Vec2(31f, 4.5f);
            _flare = FrameUtils.FlareOnePixel3();
            _fireSound = GetPath("sounds/new/HeavySniper.wav");
            _fullAuto = false;
            _fireWait = 4f;
            _kickForce = 5.8f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(0f, 1f);
            ShellOffset = new Vec2(-18f, -1f);
            laserSight = false;
            _laserOffsetTL = new Vec2(22f, 3.5f);
            _weight = 6f;
            Compose(
                _loseAccuracy,
                new SpeedAccuracy(this, 0f, 1f, 0f),
                new WithBipods(
                    this,
                    GetPath("sounds/beepods1"),
                    GetPath("sounds/beepods2"),
                    1f / 8f,
                    state =>
                    {
                        _kickForce = state.Deployed ? 0f : 5.8f;
                        _ammoType.range = state.Deployed ? 2400f : 1200f;
                        _ammoType.bulletSpeed = state.Deployed ? 150f : 48f;
                        _fireWait = state.Deployed ? 1.5f : 4f;
                        loseAccuracy = state.Deployed ? 0 : 0.1f;
                        maxAccuracyLost = state.Deployed ? 0 : 0.3f;
                        _loseAccuracy.Drain = state.Deployed ? .3f : .6f;
                        _loseAccuracy.Regen = state.Deployed ? .02f : .01f;
                        _loseAccuracy.Max = state.Deployed ? .5f : 1.0f;
                        NonSkin = state.Deployed ? 3 : state.Folded ? 0 : state.State < 0.5f ? 1 : 2;
                    }
                ).Disableable(new Vec2(6.5f, 0f))
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 3, 5 });
    }
}

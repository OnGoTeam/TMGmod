using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Semi-Automatic")]
    public class FnFcar : BaseDmr, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public FnFcar(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Belguria Fcar";
            ammo = 14;
            SetAmmoType<ATFCAR>();
            MinAccuracy = 0.67f;
            RegenAccuracyDmr = 0.01f;
            DrainAccuracyDmr = 0.1f;
            NonSkinFrames = 5;
            Smap = new SpriteMap(GetPath("FCAR"), 36, 15);
            _center = new Vec2(18f, 8f);
            _collisionOffset = new Vec2(-18f, -8f);
            _collisionSize = new Vec2(36f, 15f);
            _barrelOffsetTL = new Vec2(36f, 5.5f);
            _flare = FrameUtils.FlareOnePixel2();
            _holdOffset = new Vec2(3f, 1f);
            ShellOffset = new Vec2(-3f, -3f);
            _fireSound = GetPath("sounds/new/scar.wav");
            _fullAuto = false;
            _fireWait = 0.75f;
            _kickForce = 2.4f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            laserSight = false;
            _laserOffsetTL = new Vec2(19f, 4f);
            _weight = 7f;
            Compose(
                new WithBipods(
                    this,
                    GetPath("sounds/beepods1"),
                    GetPath("sounds/beepods2"),
                    1f / 22f,
                    state =>
                    {
                        _kickForce = state.Deployed ? 0f : 2.4f;
                        _ammoType.bulletSpeed = state.Deployed ? 72f : 36f;
                        _fireWait = state.Deployed ? 0.25f : 0.75f;
                        MaxAccuracy = state.Deployed ? 1f : 0.94f;
                        MinAccuracy = state.Deployed ? 0.95f : 0.67f;
                        NonSkin = state.Deployed ? 4 :
                            state.Folded ? 0 :
                            state.State < 0.33f ? 1 :
                            state.State < 0.67f ? 2 :
                            3;
                    }
                ).Disableable(new Vec2(8.5f, -1f))
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 7 });
    }
}

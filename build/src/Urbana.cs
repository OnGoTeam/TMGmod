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
    [EditorGroup("TMG|Sniper|Bolt-Action")]
    // ReSharper disable once InconsistentNaming
    public class Urbana : BaseBolt, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public Urbana(float xval, float yval) : base(xval, yval)
        {
            _editorName = "Urbana";
            NonSkinFrames = 4;
            Smap = new SpriteMap(GetPath("Urbana"), 53, 15);
            _center = new Vec2(27f, 8f);
            _collisionOffset = new Vec2(-27f, -8f);
            _collisionSize = new Vec2(53f, 15f);
            _barrelOffsetTL = new Vec2(53f, 5.5f);
            _flare = FrameUtils.FlareOnePixel3();
            ammo = 6;
            _fireSound = GetPath("sounds/new/HeavySniper.wav");
            _fullAuto = false;
            _kickForce = 5.25f;
            laserSight = false;
            _laserOffsetTL = new Vec2(31f, 9f);
            _holdOffset = new Vec2(9f, 1f);
            _weight = 5.6f;
            ShellOffset = new Vec2(-9f, -2f);
            SetAmmoType<ATBoltAction>();
            _bipods = new WithBipods(
                this,
                GetPath("sounds/beepods1"),
                GetPath("sounds/beepods2"),
                1f / 20f,
                state =>
                {
                    _kickForce = state.Deployed ? 0f : 5f;
                    _ammoType.range = state.Deployed ? 1800f : 1200f;
                    _ammoType.bulletSpeed = state.Deployed ? 150f : 75f;
                    NonSkin = state.Deployed ? 3 : state.Folded ? 0 : state.State < .5f ? 1 : 2;
                }
            );
            Compose(
                _bipods.Disableable()
            );
        }

        private readonly WithBipods _bipods;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        protected override void OnInitialize()
        {
            _ammoType.bulletSpeed = 75f;
            _ammoType.range = 1200f;
            _ammoType.penetration = 2f;
            base.OnInitialize();
        }

        protected override bool HasLaser() => false;

        protected override float MaxAngle() => _bipods.Deployed() ? .05f : .15f;

        protected override float MaxOffset() => 4.0f;

        protected override float ReloadSpeed() => _bipods.Deployed() ? 1f : .75f;
    }
}

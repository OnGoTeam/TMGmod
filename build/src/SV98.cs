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
    public class SV98 : BaseBolt, IHaveAllowedSkins, I5
    {
        [UsedImplicitly]
        public SV98(float xval, float yval) : base(xval, yval)
        {
            _editorName = "SV-98";
            NonSkinFrames = 3;
            Smap = new SpriteMap(GetPath("SV98"), 33, 11);
            _center = new Vec2(17f, 6f);
            _collisionOffset = new Vec2(-17f, -6f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 4.5f);
            _flare = FrameUtils.FlareOnePixel3();
            ammo = 5;
            _fireSound = GetPath("sounds/new/SV98.wav");
            _kickForce = 4.25f;
            _holdOffset = new Vec2(3f, 1f);
            _weight = 4.5f;
            _laserOffsetTL = new Vec2(21f, 4.5f);
            ShellOffset = new Vec2(-5f, -1f);
            SetAmmoType<ATBoltAction>();
            _bipods = new WithBipods(
                this,
                GetPath("sounds/beepods1"),
                GetPath("sounds/beepods2"),
                1f / 7f,
                state =>
                {
                    _kickForce = state.Deployed ? 0f : 4.67f;
                    _ammoType.range = state.Deployed ? 2500f : 1250f;
                    _ammoType.bulletSpeed = state.Deployed ? 150f : 50f;
                    NonSkin = state.Deployed ? 2 : state.Folded ? 0 : 1;
                }
            );
            Compose(
                _bipods.Disableable()
            );
        }

        private readonly WithBipods _bipods;

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 5, 8 });

        protected override void OnInitialize()
        {
            _ammoType.range = 950f;
            base.OnInitialize();
        }


        protected override bool HasLaser() => true;

        protected override float MaxAngle() => _bipods.Deployed() ? .05f : .15f;

        protected override float MaxOffset() => 4.0f;

        protected override float ReloadSpeed() => _bipods.Deployed() ? 1.5f : 1f;
    }
}

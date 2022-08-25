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
    public class AWS : BaseBolt, IHaveAllowedSkins, I5
    {
        // Amazon Web Services

        [UsedImplicitly]
        public AWS(float xval, float yval)
            : base(xval, yval)

        {
            _editorName = "AWS";
            NonSkinFrames = 3;
            Smap = new SpriteMap(GetPath("AWS"), 33, 11);
            _center = new Vec2(17f, 6f);
            _collisionOffset = new Vec2(-17f, -6f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 4f);
            ammo = 6;
            _flare = FrameUtils.FlareSilencer();
            _fireSound = GetPath("sounds/new/AWS.wav");
            _kickForce = 3.8f;
            _holdOffset = new Vec2(2f, 1f);
            _weight = 5f;
            ShellOffset = new Vec2(-2f, -2f);
            SetAmmoType<AT50SniperS>();
            _bipods = new WithBipods(
                this,
                GetPath("sounds/beepods1"),
                GetPath("sounds/beepods2"),
                1f / 10f,
                state =>
                {
                    _kickForce = state.Deployed ? 0f : 4.75f;
                    MaxAccuracy = state.Deployed ? 1f : .97f;
                    _ammoType.range = state.Deployed ? 1100f : 550f;
                    _ammoType.bulletSpeed = state.Deployed ? 100f : 35f;
                    NonSkin = state.Deployed ? 2 : state.Folded ? 0 : 1;
                }
            );
            Compose(
                _bipods.Disableable()
            );
        }

        private readonly WithBipods _bipods;

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 4, 5, 6, 7, 8, 9 });

        protected override void OnInitialize()
        {
            _ammoType.range = 550f;
            base.OnInitialize();
        }


        protected override bool HasLaser() => false;

        protected override float MaxAngle() => _bipods.Deployed() ? .05f : .25f;

        protected override float MaxOffset() => 3.0f;

        protected override float ReloadSpeed() => _bipods.Deployed() ? 1.5f : 1f;
    }
}

#if FEATURES_1_2
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|NOTRELEASEDYET")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class DTSRM44 : BaseBolt, IHaveAllowedSkins
    {
        public DTSRM44(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "DT SRM-44";
            Smap = new SpriteMap(GetPath("DT SRM-44"), 37, 12);
            _center = new Vec2(19f, 6f);
            _collisionOffset = new Vec2(-19f, -6f);
            _collisionSize = new Vec2(37f, 12f);
            _barrelOffsetTL = new Vec2(37f, 5f);
            ammo = 7;
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = GetPath("sounds/Silenced1.wav");
            _kickForce = 4.6f;
            _holdOffset = new Vec2(2f, 0f);
            _weight = 4.5f;
            _laserOffsetTL = new Vec2(30f, 7.5f);
            ShellOffset = new Vec2(-12f, -2f);
            SetAmmoType<AT762NATO>(.97f);
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        protected override void OnInitialize()
        {
            _ammoType.range = 889f;
            base.OnInitialize();
        }

        protected override bool HasLaser() => true;

        protected override float MaxAngle() => 0.1f;

        protected override float MaxOffset() => 4.0f;

        protected override float ReloadSpeed() => .5f;
    }
}
#endif

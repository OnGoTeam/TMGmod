using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Bolt-Action")]
    public class BarretM98 : BaseBolt, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public BarretM98(float xval, float yval) : base(xval, yval)
        {
            _editorName = "Barrett M98";
            Smap = new SpriteMap(GetPath("BarretM98"), 50, 13);
            _center = new Vec2(25f, 7f);
            _collisionOffset = new Vec2(-25f, -7f);
            _collisionSize = new Vec2(50f, 13f);
            _barrelOffsetTL = new Vec2(50f, 5f);
            _flare = new SpriteMap(GetPath("FlareOnePixel3"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            ammo = 7;
            _fireSound = GetPath("sounds/new/SniperRifle1.wav");
            _kickForce = 12f;
            _holdOffset = new Vec2(7f, 0f);
            _weight = 7f;
            ShellOffset = new Vec2(-4f, -1f);
            SetAmmoType<ATBoltAction>();
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 8 });

        protected override void OnInitialize()
        {
            _ammoType.penetration = 4f;
            _ammoType.range = 1000f;
            base.OnInitialize();
        }

        protected override bool HasLaser() => false;

        protected override float MaxAngle() => 0.1f;

        protected override float MaxOffset() => 4.0f;

        protected override float ReloadSpeed() => .4f;
    }
}

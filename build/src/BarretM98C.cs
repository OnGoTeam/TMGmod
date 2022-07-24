using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Bolt-Action")]
    public class BarretM98C : BaseBolt, IHaveAllowedSkins
    {
        public BarretM98C(float xval, float yval) : base(xval, yval)
        {
            _editorName = "Barrett M98 Shorty";
            Smap = new SpriteMap(GetPath("BarretM98SHORT"), 32, 13);
            _center = new Vec2(16f, 7f);
            _collisionOffset = new Vec2(-16f, -7f);
            _collisionSize = new Vec2(32f, 13f);
            _barrelOffsetTL = new Vec2(32f, 5f);
            _flare = new SpriteMap(GetPath("FlareOnePixel3"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            ammo = 7;
            _fireSound = GetPath("sounds/HeavySniper.wav");
            _kickForce = 9f;
            _holdOffset = new Vec2(-2f, 0f);
            _weight = 4.5f;
            ShellOffset = new Vec2(4f, -2f);
            SetAmmoType<ATBoltAction>(.9f);
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 8 });

        protected override void OnInitialize()
        {
            _ammoType.penetration = 4f;
            _ammoType.range = 500f;
            base.OnInitialize();
        }

        protected override bool HasLaser()
        {
            return false;
        }

        protected override float MaxAngle()
        {
            return 0.1f;
        }

        protected override float MaxOffset()
        {
            return 4.0f;
        }

        protected override float ReloadSpeed()
        {
            return .5f;
        }
    }
}

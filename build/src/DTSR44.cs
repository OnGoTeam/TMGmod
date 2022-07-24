using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Bolt-Action")]
    // ReSharper disable once InconsistentNaming
    public class DTSR44 : BaseBolt, IHaveAllowedSkins
    {
        public DTSR44(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "DT SR-44";
            Smap = new SpriteMap(GetPath("DT SR-44"), 29, 12);
            _center = new Vec2(15f, 6f);
            _collisionOffset = new Vec2(-15f, -6f);
            _collisionSize = new Vec2(29f, 12f);
            _barrelOffsetTL = new Vec2(29f, 5f);
            ammo = 6;
            _flare = new SpriteMap(GetPath("FlareBase2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = GetPath("sounds/Silenced1.wav");
            _kickForce = 3.1f;
            _holdOffset = new Vec2(-2f, 0f);
            _weight = 3.5f;
            ShellOffset = new Vec2(-8f, -2f);
            SetAmmoType<AT50SniperS>(.92f);
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 8 });

        protected override void OnInitialize()
        {
            _ammoType.range = 713f;
            base.OnInitialize();
        }

        protected override bool HasLaser() => false;

        protected override float MaxAngle() => 0.1f;

        protected override float MaxOffset() => -4.0f;

        protected override float ReloadSpeed() => 1f;
    }
}

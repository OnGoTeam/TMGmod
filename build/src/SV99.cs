using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Bolt-Action")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class SV99 : BaseBolt, I5, IHaveAllowedSkins
    {
        public SV99(float xval, float yval) : base(xval, yval)
        {
            _editorName = "SV-99";
            Smap = new SpriteMap(GetPath("SV99"), 27, 9);
            _center = new Vec2(13f, 5f);
            _collisionOffset = new Vec2(-13f, -5f);
            _collisionSize = new Vec2(27f, 9f);
            _barrelOffsetTL = new Vec2(27f, 4f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            ammo = 6;
            SetAmmoType<ATSV99>();
            _fireSound = GetPath("sounds/Silenced3.wav");
            _kickForce = 1.8f;
            loseAccuracy = 0.5f;
            maxAccuracyLost = 1.5f;
            _holdOffset = new Vec2(-1f, 0f);
            _weight = 2f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 3, 5, 8 });

        protected override bool HasLaser() => false;

        protected override float MaxAngle() => 0.3f;

        protected override float MaxOffset() => 2.0f;

        protected override float ReloadSpeed() => 2f;
    }
}

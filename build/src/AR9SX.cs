#if FEATURES_1_2
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Fully-Automatic")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class AR9SX : BaseGun, IAmSmg, IHaveAllowedSkins
    {
        public AR9SX(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 17;
            SetAmmoType<ATAR9SX>();
            
            Smap = new SpriteMap(GetPath("AR9SX"), 36, 10);
            _center = new Vec2(18f, 5f);
            _collisionOffset = new Vec2(-18f, -5f);
            _collisionSize = new Vec2(36f, 10f);
            _barrelOffsetTL = new Vec2(36f, 3f);
            _fireSound = GetPath("sounds/smg.wav");
            _flare = new SpriteMap(GetPath("FlareOnePixel0"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fullAuto = true;
            _fireWait = 0.45f;
            _kickForce = 1.3f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.25f;
            _holdOffset = new Vec2(3f, 0f);
            ShellOffset = new Vec2(-4f, -2f);
            _editorName = "AR9XS";
            laserSight = true;
            _laserOffsetTL = new Vec2(23f, 5f);
            _weight = 3f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
    }
}
#endif

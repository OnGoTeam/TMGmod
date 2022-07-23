using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Semi-Automatic")]
    [UsedImplicitly]
    public class BersaMagnum : BaseGun, IAmHg, IHaveAllowedSkins
    {
        public BersaMagnum(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 7;
            SetAmmoType<ATBersaMagnum>();
            
            Smap = new SpriteMap(GetPath("BersaMagnum"), 13, 8);
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(13f, 8f);
            _barrelOffsetTL = new Vec2(13f, 1.5f);
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _fireSound = "magnum";
            _fullAuto = false;
            _fireWait = 1.2f;
            _kickForce = 2.1f;
            loseAccuracy = 0.3f;
            maxAccuracyLost = 0.7f;
            _holdOffset = new Vec2(0f, 2f);
            ShellOffset = new Vec2(0f, 0f);
            _editorName = "Bersa Magnum";
            _laserOffsetTL = new Vec2(12f, 0f);
            laserSight = true;
            _weight = 2.4f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
    }
}

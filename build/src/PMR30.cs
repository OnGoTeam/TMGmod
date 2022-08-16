using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Semi-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class PMRC : BaseGun, IAmHg, IHaveAllowedSkins
    {
        public PMRC(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "PMR-30";
            ammo = 30;
            SetAmmoType<ATPMR30>();
            Smap = new SpriteMap(GetPath("PMR30"), 16, 10);
            _center = new Vec2(8f, 5f);
            _collisionOffset = new Vec2(-8f, -5f);
            _collisionSize = new Vec2(16f, 10f);
            _barrelOffsetTL = new Vec2(16f, 2f);
            _holdOffset = new Vec2(0f, 2f);
            ShellOffset = new Vec2(-5f, -2f);
            _fireSound = GetPath("sounds/1.wav");
            _fullAuto = false;
            _fireWait = 0.5f;
            _kickForce = 0.5f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.3f;
            _weight = 2.5f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 5, 9 });
    }
}

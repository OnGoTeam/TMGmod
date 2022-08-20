#if DEBUG
using System.Collections.Generic;
using DuckGame;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod.Useless_or_deleted_Guns
{
    [BaggedProperty("isInDemo", true)]
    [EditorGroup("TMG|DEBUG")]

    // ReSharper disable once InconsistentNaming
    public class PPShC : BaseGun, IHaveAllowedSkins, IAmSmg, I5
    {
        public PPShC(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 35;
            SetAmmoType<AT9mmParabellum>();
            _type = "gun";
            Smap = new SpriteMap(GetPath("PPShC"), 48, 16);
            _center = new Vec2(23f, 5.5f);
            _collisionOffset = new Vec2(-23f, -4.5f);
            _collisionSize = new Vec2(46f, 11f);
            _barrelOffsetTL = new Vec2(47f, 4f);
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.25f;
            _kickForce = 0.5f;
            _holdOffset = new Vec2(7f, -1f);
            loseAccuracy = 0.05f;
            maxAccuracyLost = 0.2f;
            _editorName = "PPSh";
            _weight = 5.5f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 5, 6, 7 });
    }
}
#endif

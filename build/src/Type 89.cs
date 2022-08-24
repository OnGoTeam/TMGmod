using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    public class Type89 : BaseAr, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public Type89(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Type 89";
            ammo = 26;
            SetAmmoType<ATType89>();
            Smap = new SpriteMap(GetPath("Type 89"), 30, 12);
            _center = new Vec2(15f, 6f);
            _collisionOffset = new Vec2(-15f, -6f);
            _collisionSize = new Vec2(30f, 12f);
            _barrelOffsetTL = new Vec2(30f, 4f);
            _flare = FrameUtils.FlareOnePixel1();
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.71f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.25f;
            _holdOffset = new Vec2(1f, 0f);
            ShellOffset = new Vec2(-2f, -1f);
            _weight = 4.6f;
            _kickForce = 1.1f;
            KforceDelta = .4f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 7 });
    }
}

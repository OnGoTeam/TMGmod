using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class AUGA3 : BaseAr, IHaveAllowedSkins
    {
        public AUGA3(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new ATAUGA3();
            IntrinsicAccuracy = true;
            
            Smap = new SpriteMap(GetPath("AUGA3"), 30, 12);
            _center = new Vec2(15f, 6f);
            _collisionOffset = new Vec2(-15f, -6f);
            _collisionSize = new Vec2(30f, 12f);
            _barrelOffsetTL = new Vec2(30f, 4f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(-3f, 0f);
            ShellOffset = new Vec2(-10f, -2f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 0.8f;
            loseAccuracy = 0.08f;
            maxAccuracyLost = 0.2f;
            _editorName = "AUG A3";
            _weight = 5f;
            _kickForce = .07f;
            KforceDelta = .63f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 8 });
    }
}

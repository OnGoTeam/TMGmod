#if FEATURES_1_3
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [UsedImplicitly]
    [EditorGroup("TMG|Rifle|DMR")]
    // ReSharper disable once InconsistentNaming
    public class Yava6 : BaseGun, IAmDmr, IHaveAllowedSkins
    {
        public Yava6(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Yava 6";
            Smap = new SpriteMap(GetPath("Yava 6"), 37, 13);
            _center = new Vec2(19f, 7f);
            _collisionOffset = new Vec2(-19f, -7f);
            _collisionSize = new Vec2(37f, 13f);
            _weight = 5f;
            _holdOffset = new Vec2(3f, 1f);
            ammo = 15;
            _numBulletsPerFire = 3;
            _barrelOffsetTL = new Vec2(37f, 4f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = GetPath("sounds/new/MarksmanRifle-1.wav");
            _fullAuto = true;
            _fireWait = 3.2f;
            _kickForce = 4f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.25f;
            SetAmmoType<ATYava6>();
            MinAccuracy = 0.5f;
            ShellOffset = new Vec2(-2f, -3f);
            Compose(new FocusingAccuracy(this, 1f, .05f));
        }
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
    }
}
#endif
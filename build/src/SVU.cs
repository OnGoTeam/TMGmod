using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Fully-Automatic")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class SVU : BaseGun, IAmDmr, IHaveAllowedSkins
    {
        public SVU(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "SVU";
            ammo = 12;
            SetAmmoType<ATSVU>();
            MinAccuracy = 0.2f;
            Smap = new SpriteMap(GetPath("SVU"), 37, 11);
            _flare = FrameUtils.FlareSilencer();
            _center = new Vec2(18f, 6f);
            _collisionOffset = new Vec2(-18f, -6f);
            _collisionSize = new Vec2(37f, 11f);
            _barrelOffsetTL = new Vec2(37f, 5f);
            _fireSound = GetPath("sounds/new/HeavyRifle-Silenced.wav");
            _fullAuto = true;
            _fireWait = 1.2f;
            _kickForce = 2.8f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.1f;
            _holdOffset = new Vec2(0f, 0f);
            ShellOffset = new Vec2(-11f, 0f);
            _weight = 5.7f;
            Compose(
                new LoseAccuracy(.02f, .017f, 1f),
                new SpeedAccuracy(this, 0f, 1f, 0f)
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 5, 7, 8, 9 });
    }
}

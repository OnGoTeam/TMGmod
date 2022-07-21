using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|DMR")]
    // ReSharper disable once InconsistentNaming
    public class Yava6 : BaseGun, IAmDmr, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        private readonly SpriteMap _sprite;

        public Yava6(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Yava 6";
            _graphic = _sprite = new SpriteMap(GetPath("Yava 6"), 37, 13) { frame = 0 };
            _center = new Vec2(19f, 7f);
            _collisionOffset = new Vec2(-19f, -7f);
            _collisionSize = new Vec2(37f, 13f);
            _weight = 5f;
            _holdOffset = new Vec2(4f, 1f);
            ammo = 15;
            _numBulletsPerFire = 3;
            _barrelOffsetTL = new Vec2(37f, 5f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 3.2f;
            _kickForce = 4f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.25f;
            _ammoType = new ATYava6();
            MaxAccuracy = _ammoType.accuracy;
            MinAccuracy = 0.5f;
            ShellOffset = new Vec2(-13f, -4f);
            Compose(new FocusingAccuracy(this, 1f, .05f));
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}

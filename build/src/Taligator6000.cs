using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [UsedImplicitly]
    [EditorGroup("TMG|Shotgun|Fully-Automatic")]
    public class Taligator6000 : BaseGun, IAmSg, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 3;
        private readonly SpriteMap _sprite;

        public Taligator6000(float xval, float yval) : base(xval, yval)
        {
            ammo = 11;
            _ammoType = new ATTG6000();
            _numBulletsPerFire = 13;
            
            _sprite = new SpriteMap(GetPath("Taligator 6000 SX"), 31, 12);
            _graphic = _sprite;
            _center = new Vec2(16f, 6f);
            _collisionOffset = new Vec2(-16f, -6f);
            _collisionSize = new Vec2(31f, 12f);
            _barrelOffsetTL = new Vec2(31f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(3f, 2f);
            _fireSound = "shotgunFire2";
            _kickForce = 4.5f;
            loseAccuracy = 0.6f;
            maxAccuracyLost = 0.9f;
            _fullAuto = true;
            _fireWait = 2.75f;
            _editorName = "Taligator 6000 SX";
            ShellOffset = new Vec2(-6f, -1f);
            FrameId = 0;
            _weight = 3f;
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

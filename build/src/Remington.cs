using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun|Pump-Action")]
    public class Remington : BasePumpAction, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        private readonly SpriteMap _sprite;

        public Remington(float xval, float yval) : base(xval, yval)
        {
            ammo = 6;
            _ammoType = new ATRemington();
            _numBulletsPerFire = 5;
            
            _sprite = new SpriteMap(GetPath("Fabarm FP-6"), 33, 9);
            _graphic = _sprite;
            LoaderSprite = new SpriteMap(GetPath("Fabarm FP-6Pump"), 9, 4)
            {
                center = new Vec2(5f, 2f),
            };
            FrameId = 0;
            _center = new Vec2(17f, 5f);
            _collisionOffset = new Vec2(-17f, -5f);
            _collisionSize = new Vec2(33f, 9f);
            _barrelOffsetTL = new Vec2(33f, 1f);
            _flare = new SpriteMap(GetPath("FlareBase2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(1f, 2f);
            _fireSound = "shotgunFire";
            _kickForce = 3f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.8f;
            _manualLoad = true;
            _fireWait = 3f;
            _editorName = "Fabarm FP-6";
            ShellOffset = new Vec2(0f, -3f);
            LoaderVec2 = new Vec2(9f, -1f);
            Loaddx = 3f;
            LoadSpeed = 15;
            _weight = 3.2f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 4 });

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        public int FrameId
        {
            get => _sprite.frame;
            set
            {
                SetSpriteMapFrameId(_sprite, value, 10 * NonSkinFrames);
                SetSpriteMapFrameId(LoaderSprite, value, 10);
            }
        }
    }
}

using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|DMR")]
    // ReSharper disable once InconsistentNaming
    public class HK417 : BaseDmr, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        private readonly SpriteMap _sprite;

        public HK417(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 11;
            _ammoType = new ATHk417();
            MaxAccuracy = 0.93f;
            MinAccuracy = 0.75f;
            RegenAccuracyDmr = 0.006f;
            DrainAccuracyDmr = 0.1f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Hk417"), 30, 10);
            _graphic = _sprite;
            _sprite.frame = SkinValue = 4;
            _center = new Vec2(15f, 5f);
            _collisionOffset = new Vec2(-15f, -5f);
            _collisionSize = new Vec2(30f, 10f);
            _barrelOffsetTL = new Vec2(30f, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(0f, 1f);
            ShellOffset = new Vec2(-3f, -2f);
            _fireSound = GetPath("sounds/HeavyRifle.wav");
            _fullAuto = false;
            _fireWait = 0.8f;
            _kickForce = 2.1f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.15f;
            _editorName = "Hk 417C";
            _weight = 3.5f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 4, 1, 7 });

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}

using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class M4A1 : BaseAr, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        private readonly SpriteMap _sprite;

        public M4A1(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new AT545NATO
            {
                range = 330f,
                accuracy = 0.86f,
            };
            IntrinsicAccuracy = true;
            
            _sprite = new SpriteMap(GetPath("M4A1"), 30, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(15f, 6f);
            _collisionOffset = new Vec2(-15f, -6f);
            _collisionSize = new Vec2(30f, 11f);
            _barrelOffsetTL = new Vec2(30f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.75f;
            loseAccuracy = 0.07f;
            maxAccuracyLost = 0.21f;
            _holdOffset = new Vec2(2f, 1f);
            ShellOffset = new Vec2(-2f, -2f);
            _editorName = "M4A1";
            _weight = 4f;
            _kickForce = .5f;
            KforceDelta = .5f;
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

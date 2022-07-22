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
    [EditorGroup("TMG|Handgun|Burst")]
    public class Glock18 : BaseGun, IAmHg, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        private readonly SpriteMap _sprite;

        public Glock18(float xval, float yval)
            : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("Anyx PR5"), 12, 8);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(6f, 4f);
            _collisionOffset = new Vec2(-6f, -4f);
            _collisionSize = new Vec2(12f, 8f);
            ammo = 21;
            _ammoType = new ATPR5();
            
            _barrelOffsetTL = new Vec2(12f, 1f);
            _flare = new SpriteMap(GetPath("FlareOnePixel0"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = GetPath("sounds/2.wav");
            _fullAuto = false;
            _fireWait = 0.5f;
            _kickForce = 0.5f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.6f;
            ShellOffset = new Vec2(-1f, -2f);
            _holdOffset = new Vec2(-1f, 2f);
            _editorName = "Anyx PR5";
            _weight = 1.7f;
            ComposeSimpleBurst(3, .6f);
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 3, 4 });

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}

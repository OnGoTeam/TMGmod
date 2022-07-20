using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Fully-Automatic")]
    public class Alep30 : BaseGun, IAmHg, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        private readonly SpriteMap _sprite;

        public Alep30(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 18;
            _ammoType = new ATAlep30();
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Alep30"), 16, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _center = new Vec2(8f, 5f);
            _collisionOffset = new Vec2(-8f, -5f);
            _collisionSize = new Vec2(16f, 9f);
            _barrelOffsetTL = new Vec2(16f, 2f);
            _fireSound = GetPath("sounds/smg.wav");
            _fullAuto = true;
            _fireWait = 0.45f;
            _kickForce = 1f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.8f;
            _holdOffset = new Vec2(1f, 2f);
            ShellOffset = new Vec2(-3f, -3f);
            _editorName = "Alep 30";
            _weight = 2.3f;
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

using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Modifiers.Firing;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Burst")]
    // ReSharper disable once InconsistentNaming
    public class Vista : BaseGun, IHaveAllowedSkins, I5
    {
        private const int NonSkinFrames = 1;
        private readonly SpriteMap _sprite;

        public Vista(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Vista";

            _graphic = _sprite = new SpriteMap(GetPath("Vista"), 16, 14) { frame = 0 };
            _center = new Vec2(6f, 7f);
            _collisionOffset = new Vec2(-8f, -7f);
            _collisionSize = new Vec2(16f, 14f);
            _holdOffset = new Vec2(-1f, 3f);
            _barrelOffsetTL = new Vec2(16f, 2f);
            ShellOffset = new Vec2(-4f, -3f);

            _weight = 2f;
            ammo = 30;
            _fullAuto = false;
            _fireWait = 0.36f;
            _kickForce = 1.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.44f;
            SetFirstAccuracy<ATVista>(1f, 30);

            _fireSound = GetPath("sounds/2.wav");

            Compose(
                new Burst(this, true, null) { Num = 3, Wait = .1f }
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 5 });

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}

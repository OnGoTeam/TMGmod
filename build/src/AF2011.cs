using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Semi-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class AF2011 : BaseGun, IAmHg, IHaveAllowedSkins, I5
    {
        private const int NonSkinFrames = 1;

        private readonly SpriteMap _sprite;

        public AF2011(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 10;
            _ammoType = new ATAF2011();
            _numBulletsPerFire = 2;
            _sprite = new SpriteMap(GetPath("AF2011"), 16, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-8f, -4f);
            _collisionSize = new Vec2(16f, 9f);
            _barrelOffsetTL = new Vec2(16f, 1f);
            _fireSound = "pistolFire";
            _fireWait = 0.6f;
            _kickForce = 1.7f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.4f;
            _holdOffset = new Vec2(-1f, 1f);
            ShellOffset = new Vec2(0f, 0f);
            _editorName = "AF-2011";
            _weight = 2.5f;
            MaxAccuracy = .95f;
            Compose(new LoseAccuracy(0.05f, 0.003f, 1f));
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 3, 5 });

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        protected override void PopBaseShell()
        {
            base.PopBaseShell();
            base.PopBaseShell();
        }
    }
}

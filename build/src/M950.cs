using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [BaggedProperty("isInDemo", true)]
    [EditorGroup("TMG|SMG|Fully-Automatic")]
    public class M950 : BaseGun, IAmSmg, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        private readonly SpriteMap _sprite;

        // ReSharper disable once MemberCanBePrivate.Global		
        public M950(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 50;
            _ammoType = new ATCalico
            {
                range = Rando.Float(10f, 70f),
            };
            MaxAccuracy = 0.4f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("M950A"), 23, 7);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(11f, 4f);
            _collisionOffset = new Vec2(-11f, -4f);
            _collisionSize = new Vec2(23f, 7f);
            _barrelOffsetTL = new Vec2(23f, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel0"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = "smg";
            _fullAuto = true;
            _fireWait = 0.2f;
            _kickForce = 0.3f;
            _holdOffset = new Vec2(2.5f, 1.5f);
            ShellOffset = new Vec2(0f, 1f);
            loseAccuracy = 0.01f;
            maxAccuracyLost = 0.05f;
            _editorName = "Calico M950A";
            _weight = 1f;
            handAngle = 0f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void OnHoldAction()
        {
            _ammoType.range = Rando.Float(0f, Rando.Float(0f, 45f));
            handAngle = Rando.Float(-0.15f, 0.15f);
            base.OnHoldAction();
        }

        public override void OnReleaseAction()
        {
            handAngle = 0f;
            base.OnReleaseAction();
        }
    }
}

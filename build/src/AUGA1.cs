using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class AUGA1 : BaseAr, IHaveAllowedSkins, I5
    {
        private const int NonSkinFrames = 2;
        private readonly SpriteMap _sprite;

        [UsedImplicitly] public StateBinding GripBinding = new StateBinding(nameof(Grip));

        public AUGA1(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 42;
            _ammoType = new ATAUGA1();
            IntrinsicAccuracy = true;
            
            _sprite = new SpriteMap(GetPath("AUGA1"), 30, 12);
            _graphic = _sprite;
            _sprite.frame = SkinValue = 8;
            _center = new Vec2(15f, 6f);
            _collisionOffset = new Vec2(-15f, -6f);
            _collisionSize = new Vec2(30f, 12f);
            _barrelOffsetTL = new Vec2(30f, 4f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(-2f, 1f);
            ShellOffset = new Vec2(-10f, -2f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 0.8f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.2f;
            _editorName = "AUG A1";
            _weight = 5.5f;
            _kickForce = .07f;
            KforceDelta = .63f;
        }

        protected override string HintMessage => "foregrip";

        [UsedImplicitly]
        public bool Grip
        {
            get => maxAccuracyLost < 0.2f;
            set
            {
                FrameId = FrameId % 10 + 10 * (value ? 1 : 0);
                maxAccuracyLost = value ? .1f : .2f;
                _ammoType.accuracy = value ? .97f : .80f;
            }
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 4, 5, 6, 8 });

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void Update()
        {
            if (isServerForObject && duck?.inputProfile.Pressed("QUACK") == true)
            {
                Grip = !Grip;
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }

            base.Update();
        }
    }
}

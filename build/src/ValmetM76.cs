using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.Modifiers.Firing;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Combined")]
    public class ValmetM76 : BaseGun, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 2;
        private readonly SpriteMap _sprite;

        public ValmetM76(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 20;
            _ammoType = new ATM76();
            MaxAccuracy = 0.89f;
            MinAccuracy = 0.5f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Valmet M76"), 33, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(17f, 5f);
            _collisionOffset = new Vec2(-17f, -5f);
            _collisionSize = new Vec2(33f, 10f);
            _barrelOffsetTL = new Vec2(33f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(4f, 1f);
            ShellOffset = new Vec2(-2f, -2f);
            _fireSound = GetPath("sounds/HeavyRifle.wav");
            _fullAuto = false;
            _fireWait = 0.7f;
            _kickForce = 3f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.25f; //0.4f
            _editorName = "Valmet M76";
            _weight = 4f;
            var lose = new LoseAccuracy(0.15f, 0.02f, 1f);
            Compose(
                lose,
                new Burst(
                    this,
                    false,
                    burst => {
                        _fireWait = burst ? 1.4f : 0.7f;
                        FrameId = FrameId % 10 + (burst ? 10 : 0);
                        loseAccuracy = burst ? 0f : 0.15f;
                        _kickForce = burst ? 6.5f : 3f;
                        MaxAccuracy = burst ? 1f : 0.89f;
                        lose.Regen = burst ? 0f : 0.02f;
                        lose.Drain = burst ? 0f : 0.15f;
                        MaxAccuracy = burst ? 1f : 0.89f;
                    }
                )
                {
                    Num = 2,
                    Wait = .15f,
                    SwitchOnQuack = true,
                }
            );
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

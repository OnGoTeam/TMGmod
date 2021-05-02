using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Bolt-Action")]
    [BaggedProperty("isSuperWeapon", true)]
    public sealed class X3X : BaseGun
    {
        [UsedImplicitly]
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % 3;
        }

        private readonly SpriteMap _sprite;
        public X3X (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 6;
            _ammoType = new ATx3x();
            _type = "gun";
            _sprite = new SpriteMap(GetPath("X3X"), 27, 14);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(14f, 9f);
            _collisionOffset = new Vec2(-11.5f, -9f);
            _collisionSize = new Vec2(27f, 14f);
            _barrelOffsetTL = new Vec2(27f, 5f);
            _fireSound = GetPath("sounds/x3xshsnd.wav");
            _fullAuto = false;
            _fireWait = 2f;
            _kickForce = 8f;
            loseAccuracy = 1.7f;
            maxAccuracyLost = 1.7f;
            _holdOffset = new Vec2(0f, 2f);
            ShellOffset = new Vec2(-4f, -1f);
            _editorName = "Experimental X3X";
            _bio = "ammo = 1337";
			_weight = 5.5f;
        }

        public override void OnPressAction()
        {
            if ((ammo > 0) & (_sprite.frame == 0))
            {
                Fire();
                _sprite.frame = ammo < 1 ? 2 : 1;
            }
            else if (_sprite.frame == 1)
            {
                _sprite.frame = 0;
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }
            if (!((ammo < 1) & (_sprite.frame == 0))) return;
            SFX.Play(GetPath("sounds/tuduc.wav"));
        }
    }
}
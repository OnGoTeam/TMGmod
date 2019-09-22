using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
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
            ammo = 5;
            _ammoType = new ATx3x();
            _type = "gun";
            //this.graphic = new Sprite(GetPath("X3X"));
            _sprite = new SpriteMap(GetPath("X3X"), 27, 14);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(14f, 9f);
            _collisionOffset = new Vec2(-11.5f, -9f);
            _collisionSize = new Vec2(27f, 14f);
            _barrelOffsetTL = new Vec2(27f, 5f);
            _fireSound = "deepMachineGun2";
            _fullAuto = false;
            _fireWait = 2f;
            _kickForce = 10f;
            loseAccuracy = 1.9f;
            maxAccuracyLost = 2.5f;
            _holdOffset = new Vec2(0f, 2f);
            ShellOffset = new Vec2(-4f, -1f);
            _editorName = "Experimental X3X";
            _bio = "ammo = 5";
			_weight = 5.5f;
        }
        public override void Fire()
        {
            base.Fire();
        }

        public override void OnPressAction()
        {
            if ((ammo > 0) & (_sprite.frame == 0))
            {
                _sprite.frame = 1;
                Fire();
            }
            else if (_sprite.frame == 1)
            {
                _sprite.frame = 0;
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }
            if ((ammo < 2) & (_sprite.frame == 0))
            {
                _sprite.frame = 2;
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }
        }
    }
}
using DuckGame;
using System.Collections.Generic;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Bolt-Action")]
    [BaggedProperty("isSuperWeapon", true)]
    public sealed class X3X : BaseGun, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 3;
        private readonly SpriteMap _sprite;

        public X3X(float xval, float yval)
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

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void OnPressAction()
        {
            if ((ammo > 0) & (_sprite.frame / 10 == 0))
            {
                Fire();
                _sprite.frame += ammo < 1 ? 20 : 10;
            }
            else if (_sprite.frame / 10 == 1)
            {
                _sprite.frame -= 10;
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }

            if (!((ammo < 1) & (_sprite.frame / 10 == 0))) return;
            SFX.Play(GetPath("sounds/tuduc.wav"));
        }
    }
}

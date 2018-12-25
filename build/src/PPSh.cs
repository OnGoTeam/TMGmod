using DuckGame;
using TMGmod.Core;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{

    [BaggedProperty("isInDemo", true), EditorGroup("TMG|SMG")]
    // ReSharper disable once InconsistentNaming
    public class PPSh : Gun, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public PPSh(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 71;
            _ammoType = new AT9mm
            {
                range = 300f,
                accuracy = 0.9f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("PPShpattern"), 48, 16);
            graphic = _sprite;
            _sprite.frame = 0;
            center = new Vec2(23f, 5.5f);
            collisionOffset = new Vec2(-23f, -4.5f);
            collisionSize = new Vec2(46f, 11f);
            _barrelOffsetTL = new Vec2(47f, 4f);
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.25f;
            _kickForce = 0.5f;
            _holdOffset = new Vec2(7f, -1f);
            loseAccuracy = 0.05f;
            maxAccuracyLost = 0.2f;
            _editorName = "PPSh";
			weight = 5.5f;
        }

        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public StateBinding FrameIdBinding => new StateBinding(nameof(FrameId));
    }
}
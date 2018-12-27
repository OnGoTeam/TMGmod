using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{

    [EditorGroup("TMG|SMG")]
    // ReSharper disable once InconsistentNaming
    public class MP5 : BaseBurst, IFirstKforce, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        public bool Rate;
        public StateBinding RateBinding = new StateBinding(nameof(Rate));
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public MP5(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new AT9mm
            {
                range = 215f,
                accuracy = 0.7f
            };
            BaseAccuracy = 0.7f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("MP5pattern"), 27, 12);
            graphic = _sprite;
            _sprite.frame = 10;
            center = new Vec2(13.5f, 6f);
            collisionOffset = new Vec2(-13.5f, -6f);
            collisionSize = new Vec2(27f, 12f);
            _barrelOffsetTL = new Vec2(27f, 3f);
            _fireSound = "deepMachineGun";
            _fullAuto = false;
            _fireWait = 1.3f;
            _kickForce = 0.5f;
            _holdOffset = new Vec2(-1f, 2f);
            _editorName = "MP5";
			weight = 3f;
            KforceDSmg = 2f;
            MaxDelaySmg = 50;
            DeltaWait = 0.65f;
            BurstNum = 3;
        }
        public override void Update()
        {
            if (owner != null)
            {
                if (duck.inputProfile.Pressed("QUACK"))
                {
                    if (Rate)
                    {
                        Rate = false;
                        BurstNum = 3;
                        _fireWait = 1.3f;
                        _sprite.frame += 10;
                    }
                    else
                    {
                        Rate = true;
                        BurstNum = 1;
                        _fireWait = 0.3f;
                        _sprite.frame -= 10;
                    }
                }
            }
            base.Update();
        }
        public float KforceDSmg { get; }
        public int CurrDelaySmg { get; set; }
        public int MaxDelaySmg { get; set; }
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}

using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun")]
    [BaggedProperty("canSpawn", false)]
    public class SkeetGun:Gun, IHaveSkin, IAmSg
    {

        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public SkeetGun(float xval, float yval) : base(xval, yval)
        {
            ammo = 2;
            _ammoType = new ATShotgun
            {
                accuracy = 0.9f,
                bulletColor = new Color(255, 0, 0),
                range = 500f,
                bulletSpeed = 50f
            };
            _numBulletsPerFire = 10;
            _sprite = new SpriteMap(GetPath("SkeetDoublepattern"), 41, 7);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(20.5f, 3.5f);
            _collisionOffset = new Vec2(-20.5f, -3.5f);
            _collisionSize = new Vec2(41f, 7f);
            _fireSound = "shotgunFire";
            _barrelOffsetTL = new Vec2(43f, 1f);
            _fireWait = 0.5f;
            _kickForce = 6.55f;
            _editorName = "Virtual Double";
            _holdOffset = new Vec2(6f, 2f);
        }

        public override void Update()
        {
            base.Update();
            _barrelOffsetTL = ammo % 2 == 0 ? new Vec2(43f, 1f) : new Vec2(43f, 3f);
            if (duck != null)
            {
                if (duck.sliding || duck.crouch)
                {
                    handAngle = 0f;
                    return;
                }
                if (duck.inputProfile.Down("UP") && !_raised)
                {
                    if (offDir < 0)
                    {
                        handAngle = 0.5f;
                    }
                    else
                    {
                        handAngle = -0.5f;
                    }

                    return;
                }
            }

            handAngle = 0f;
        }

        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}
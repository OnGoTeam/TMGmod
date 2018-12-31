using DuckGame;
using TMGmod.Core;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun")]
    [BaggedProperty("canSpawn", false)]
    public class SkeetGun:Gun, IHaveSkin
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
            graphic = _sprite;
            _sprite.frame = 0;
            center = new Vec2(20.5f, 3.5f);
            collisionOffset = new Vec2(-20.5f, -3.5f);
            collisionSize = new Vec2(41f, 7f);
            _fireSound = "shotgunFire";
            _barrelOffsetTL = new Vec2(43f, 1f);
            _fireWait = 0.5f;
            _kickForce = 4.78f;
            _editorName = "Virtual Double";
            _holdOffset = new Vec2(6f, 2f);
        }

        public override void Update()
        {
            base.Update();
            if (ammo % 2 == 0)
            {
                _barrelOffsetTL = new Vec2(43f, 1f);
                //ammo = 2;
            }
            else
            {
                _barrelOffsetTL = new Vec2(43f, 3f);
            }
            /*if (duck != null)
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
                        //this._angleOffset = 0.5f;
                        handAngle = 0.5f;
                    }
                    else
                    {
                        //this._angleOffset = -0.5f;
                        handAngle = -0.5f;
                    }

                    return;
                }
            }*/

            handAngle = 0f;
        }

        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}
using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|AutoPistol")]
    public class Vag : BaseGun, IAmHg
    {
        public float Mode = 1f;
        public StateBinding ModeBinding = new StateBinding(nameof(Mode));

        public Vag(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 48;
            _ammoType = new AT9mm
            {
                range = 175f,
                accuracy = 0.81f,
                penetration = 1f,
                bulletSpeed = 12f,

            };
            _type = "gun";
            _graphic = new Sprite(GetPath("VAG731"));
            _center = new Vec2(8f, 3f);
            _collisionOffset = new Vec2(-7.5f, -3.5f);
            _collisionSize = new Vec2(16f, 11f);
            _barrelOffsetTL = new Vec2(16f, 1f);
            _holdOffset = new Vec2(-2f, -3.5f);
            _fireSound = GetPath("sounds/2.wav");
            _fullAuto = true;
            _fireWait = 0.3f;
            _kickForce = 0.1f;
            loseAccuracy = 0.075f;
            maxAccuracyLost = 0.225f;
            _editorName = "Dominator";
			_weight = 2f;
        }
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (Mode > 0f && Mode < 2f)
                {
                    graphic = new Sprite(GetPath("VAG732"));
                    _fireWait = 0.6f;
                    Mode = 2f;
                }
                else if (Mode > 1f && Mode < 3f)
                {
                    graphic = new Sprite(GetPath("VAG733"));
                    _fireWait = 0.9f;
                    Mode = 3f;
                }
                else if (Mode > 2f && Mode < 4f)
                {
                    graphic = new Sprite(GetPath("VAG731"));
                    _fireWait = 0.3f;
                    Mode = 1f;
                }
            }
            base.Update();
		}

        public override void Reload(bool shell = true)
        {
            if (ammo != 0)
            {
                --ammo;
            }
            loaded = true;
        }
    }
}
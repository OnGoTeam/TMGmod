using DuckGame;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun")]
    [BaggedProperty("canSpawn", false)]
    public sealed class SkeetGun:Gun
    {
        public SkeetGun(float xval, float yval) : base(xval, yval)
        {
            ammo = 2;
            _ammoType = new ATShotgun()
            {
                accuracy = 0.9f,
                bulletColor = new Color(200, 190, 150),
                range = 500f,
                bulletSpeed = 50f
            };
            _numBulletsPerFire = 10;
            graphic = new Sprite(GetPath("SkeetDouble"));
            center = new Vec2(20.5f, 3.5f);
            collisionOffset = new Vec2(-20.5f, -3.5f);
            collisionSize = new Vec2(41f, 7f);
            _fireSound = "shotgunFire";
            _barrelOffsetTL = new Vec2(43f, 1f);
            _fireWait = 0.5f;
            _kickForce = 0f;
            _editorName = "Virtual Double";
            _holdOffset = new Vec2(6f, 2f);
        }

        public override void Update()
        {
            base.Update();
            if (ammo % 2 == 0)
            {
                _barrelOffsetTL = new Vec2(43f, 1f);
                ammo = 2;
            }
            else
            {
                _barrelOffsetTL = new Vec2(43f, 3f);
            }
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
            }

            handAngle = 0f;
        }
    }
}
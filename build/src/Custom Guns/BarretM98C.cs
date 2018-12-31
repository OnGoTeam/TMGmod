using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Custom_Guns
{
    [EditorGroup("TMG|Sniper|Custom")]
    public class BarretM98C : Sniper
    {
        public BarretM98C(float xval, float yval) : base(xval, yval)
        {
            graphic = new Sprite(GetPath("BarretM98short"));
            center = new Vec2(17f, 10f);
            collisionOffset = new Vec2(-25f, -10f);
            collisionSize = new Vec2(50f, 13f);
            _barrelOffsetTL = new Vec2(39f, 6f);
            ammo = 8;
            _ammoType = new ATSniper {accuracy = 0.9f};
            _fireSound = GetPath("sounds/HeavySniper.wav");
            _fullAuto = false;
            _kickForce = 2.5f;
            laserSight = false;
            _laserOffsetTL = new Vec2(31f, 9f);
            _holdOffset = new Vec2(-4f, 2f);
            _editorName = "Barrett M98C";
			weight = 6.5f;
        }

        public override void Draw()
        {
            var ang = angle;
            if (offDir <= 0)
            {
                angle = angle + _angleOffset;
            }
            else
            {
                angle = angle - _angleOffset;
            }
            base.Draw();
            angle = ang;
            laserSight = false;
        }

        public override void OnPressAction()
        {
            if (loaded)
            {
                base.OnPressAction();
                return;
            }
            if (ammo > 0 && _loadState == -1)
            {
                _loadState = 0;
                _loadAnimation = 0;
            }
        }

        public override void Update()
        {
            base.Update();
            if (_loadState > -1)
            {
                if (owner == null)
                {
                    if (_loadState == 3)
                    {
                        loaded = true;
                    }
                    _loadState = -1;
                    _angleOffset = 0f;
                    handOffset = Vec2.Zero;
                }
                if (_loadState == 0)
                {
                    if (!Network.isActive)
                    {
                        SFX.Play("loadSniper");
                    }
                    else if (isServerForObject)
                    {
                        _netLoad.Play();
                    }
                    Sniper sniper = this;
                    sniper._loadState = sniper._loadState + 1;
                }
                else if (_loadState == 1)
                {
                    if (_angleOffset >= 0.1f)
                    {
                        Sniper sniper1 = this;
                        sniper1._loadState = sniper1._loadState + 1;
                    }
                    else
                    {
                        _angleOffset = _angleOffset + 0.003f;
                    }
                }
                else if (_loadState == 2)
                {
                    handOffset.x = handOffset.x - 0.2f;
                    if (handOffset.x > 4f)
                    {
                        Sniper sniper2 = this;
                        sniper2._loadState = sniper2._loadState + 1;
                        Reload();
                        loaded = false;
                    }
                }
                else if (_loadState == 3)
                {
                    handOffset.x = handOffset.x + 0.2f;
                    if (handOffset.x <= 0f)
                    {
                        Sniper sniper3 = this;
                        sniper3._loadState = sniper3._loadState + 1;
                        handOffset.x = 0f;
                    }
                }
                else if (_loadState == 4)
                {
                    if (_angleOffset <= 0.03f)
                    {
                        _loadState = -1;
                        loaded = true;
                        _angleOffset = 0f;
                    }
                    else
                    {
                        _angleOffset = MathHelper.Lerp(_angleOffset, 0f, 0.15f);
                    }
                }
            }
            laserSight = false;
        }
    }
}
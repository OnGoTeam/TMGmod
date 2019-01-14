﻿using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Sniper")]
    // ReSharper disable once InconsistentNaming
    public class SV98 : Sniper
    {
		
		public SV98(float xval, float yval) : base(xval, yval)
        {
            graphic = new Sprite(GetPath("SV98"));
            center = new Vec2(16.5f, 4.5f);
            collisionOffset = new Vec2(-16.5f, -4.5f);
            collisionSize = new Vec2(33f, 9f);
            _barrelOffsetTL = new Vec2(34f, 5f);
            ammo = 5;
            _ammoType = new ATSniper();
            _fireSound = "sniper";
            _fullAuto = false;
            _kickForce = 1.75f;
            _holdOffset = new Vec2(2f, 0f);
            _editorName = "SV-98";
			weight = 4.5f;
            laserSight = true;
            _laserOffsetTL = new Vec2(18f, 3f);
			
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
            laserSight = true;
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
                    _loadState++;
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
                        _loadState++;
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
            laserSight = true;
            if (duck != null && duck.height < 17f)
            {
                _kickForce = 0f;
				graphic = new Sprite(GetPath("SV98bipods"));
            }
            else
            {
                _kickForce = 1.75f;
				graphic = new Sprite(GetPath("SV98"));
            }
            OnHoldAction();
        }
	}
}
﻿using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.WClasses;
using TMGmod.Core;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Bolt-Action")]
    // ReSharper disable once InconsistentNaming
    public class SV98 : Sniper, IAmSr, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public readonly EditorProperty<int> Skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 5});
        public SV98(float xval, float yval) : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            _sprite = new SpriteMap(GetPath("SV98pattern"), 33, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(16.5f, 5.5f);
            _collisionOffset = new Vec2(-16.5f, -5.5f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(34f, 5f);
            ammo = 5;
            _ammoType = new ATSniper
            {
                penetration = 1f,
                range = 1250f,
                accuracy = 1f
            };
            _fireSound = "sniper";
            _fullAuto = false;
            _kickForce = 4.67f;
            _holdOffset = new Vec2(2f, 0f);
            _editorName = "SV-98";
			_weight = 4.5f;
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

            if (ammo <= 0 || _loadState != -1) return;
            //else
            _loadState = 0;
            _loadAnimation = 0;
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

                switch (_loadState)
                {
                    case 0:
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
                        break;
                    }
                    case 1 when _angleOffset >= 0.1f:
                    {
                        Sniper sniper1 = this;
                        sniper1._loadState = sniper1._loadState + 1;
                        break;
                    }
                    case 1:
                        _angleOffset = _angleOffset + 0.003f;
                        break;
                    case 2:
                    {
                        handOffset.x = handOffset.x - 0.2f;
                        if (handOffset.x > 4f)
                        {
                            _loadState++;
                            Reload();
                            loaded = false;
                        }

                        break;
                    }
                    case 3:
                    {
                        handOffset.x = handOffset.x + 0.2f;
                        if (handOffset.x <= 0f)
                        {
                            Sniper sniper3 = this;
                            sniper3._loadState = sniper3._loadState + 1;
                            handOffset.x = 0f;
                        }

                        break;
                    }
                    case 4 when _angleOffset <= 0.03f:
                        _loadState = -1;
                        loaded = true;
                        _angleOffset = 0f;
                        break;
                    case 4:
                        _angleOffset = MathHelper.Lerp(_angleOffset, 0f, 0.15f);
                        break;
                }
            }
            laserSight = true;
            if (duck != null && duck.height < 17f)
            {
                _kickForce = 0f;
                if ((_sprite.frame > -1) && (_sprite.frame < 10)) _sprite.frame += 10;
            }
            else
            {
                _kickForce = 4.67f;
                if ((_sprite.frame > 9) && (_sprite.frame < 20)) _sprite.frame -= 10;
            }
            OnHoldAction();
        }
        private void UpdateSkin()
        {
            var bublic= Skin.value;
            while (!Allowedlst.Contains(bublic))
            {
                bublic = Rando.Int(0, 9);
            }
            _sprite.frame = bublic;
        }

        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}
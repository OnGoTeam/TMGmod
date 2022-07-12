using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.NY
{
    [EditorGroup("TMG|Misc|Holiday")]
    // ReSharper disable once InconsistentNaming
    public class Icer : Sniper, IAmSr
    {
        public Icer(float xval, float yval) : base(xval, yval)
        {
            _graphic = new SpriteMap(GetPath("Holiday/Icer"), 53, 13);
            _center = new Vec2(27f, 8f);
            _collisionOffset = new Vec2(-27f, -8f);
            _collisionSize = new Vec2(53f, 15f);
            _barrelOffsetTL = new Vec2(53f, 5f);
            _flare = new SpriteMap(GetPath("Holiday/IcerFlare"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            ammo = 4;
            _ammoType = new ATIcer();
            _fireSound = GetPath("sounds/RifleOrMG.wav");
            _fullAuto = false;
            _kickForce = 5f;
            _laserOffsetTL = new Vec2(31f, 9f);
            _holdOffset = new Vec2(9f, 1f);
            _editorName = "Icer Urbana";
            _weight = 5.6f;
        }
        public override void Draw()
        {
            var ang = angle;
            if (offDir <= 0)
            {
                angle += _angleOffset;
            }
            else
            {
                angle -= _angleOffset;
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

            if (ammo <= 0 || _loadState != -1) return;
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

                // ReSharper disable once SwitchStatementMissingSomeCases
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
                            Sniper sniper = this;
                            sniper._loadState += 1;
                            break;
                        }
                    case 1 when _angleOffset >= 0.1f:
                        {
                            Sniper sniper1 = this;
                            sniper1._loadState += 1;
                            break;
                        }
                    case 1:
                        _angleOffset += 0.003f;
                        break;
                    case 2:
                        {
                            handOffset.x -= 0.2f;
                            if (handOffset.x > 4f)
                            {
                                Sniper sniper2 = this;
                                sniper2._loadState += 1;
                                Reload();
                                loaded = false;
                            }

                            break;
                        }
                    case 3:
                        {
                            handOffset.x += 0.2f;
                            if (handOffset.x <= 0f)
                            {
                                Sniper sniper3 = this;
                                sniper3._loadState += 1;
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
            laserSight = false;
        }
    }
}
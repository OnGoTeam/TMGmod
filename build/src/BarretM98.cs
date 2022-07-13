using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Bolt-Action")]
    public class BarretM98 : Sniper, IAmSr, IHaveSkin
    {
        private const int NonSkinFrames = 1;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 8 });
        private readonly Vec2 _fakeshelloffset = new Vec2(-6f, -3f);
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public BarretM98(float xval, float yval) : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            _sprite = new SpriteMap(GetPath("BarretM98"), 50, 13);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(25f, 7f);
            _collisionOffset = new Vec2(-25f, -7f);
            _collisionSize = new Vec2(50f, 13f);
            _barrelOffsetTL = new Vec2(50f, 5f);
            _flare = new SpriteMap(GetPath("FlareOnePixel3"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            ammo = 8;
            _ammoType = new ATBoltAction { penetration = 4f, range = 850f };
            _fireSound = GetPath("sounds/HeavySniper.wav");
            _fullAuto = false;
            _kickForce = 6f;
            laserSight = false;
            //_laserOffsetTL = new Vec2(31f, 9f);
            _holdOffset = new Vec2(7f, 0f);
            _editorName = "Barrett M98";
            _weight = 7f;
        }

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void Reload(bool shell = true)
        {
            if (ammo != 0)
            {
                if (shell) _ammoType.PopShell(Offset(_fakeshelloffset).x, Offset(_fakeshelloffset).y, -offDir);
                --ammo;
            }

            loaded = true;
        }

        public override void Draw()
        {
            var ang = angle;
            if (offDir <= 0)
                angle += _angleOffset;
            else
                angle -= _angleOffset;
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
                    if (_loadState == 3) loaded = true;
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
                            SFX.Play("loadSniper");
                        else if (isServerForObject) _netLoad.Play();
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

        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic)) bublic = Rando.Int(0, 9);
            _sprite.frame = bublic;
        }

        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}

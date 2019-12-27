using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.WClasses;
using TMGmod.Core;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Bolt-Action")]
    // ReSharper disable once InconsistentNaming
    public class SV98 : Sniper, IAmSr, IHaveSkin, I5, IHaveBipods
    {
        private Vec2 fakeshelloffset = new Vec2(-1f, -2f);
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 3;
        [UsedImplicitly]
        public NetSoundEffect BipOn = new NetSoundEffect(Mod.GetPath<Core.TMGmod>("sounds/beepods1"));
        [UsedImplicitly]
        public NetSoundEffect BipOff = new NetSoundEffect(Mod.GetPath<Core.TMGmod>("sounds/beepods2"));
        [UsedImplicitly]
        public StateBinding BipOnBinding = new NetSoundBinding(nameof(BipOn));
        [UsedImplicitly]
        public StateBinding BipOffBinding = new NetSoundBinding(nameof(BipOff));
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 5, 8 });
        public SV98(float xval, float yval) : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            _sprite = new SpriteMap(GetPath("SV98"), 33, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(17f, 6f);
            _collisionOffset = new Vec2(-17f, -6f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 4f);
            _flare = new SpriteMap(GetPath("FlareOnePixel3"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
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
            _holdOffset = new Vec2(3f, 1f);
            _editorName = "SV-98";
			_weight = 4.5f;
            laserSight = true;
            _laserOffsetTL = new Vec2(18f, 2f);

        }
        public override void Reload(bool shell = true)
        {
            if (ammo != 0)
            {
                if (shell)
                {
                    _ammoType.PopShell(Offset(fakeshelloffset).x, Offset(fakeshelloffset).y, -offDir);
                }
                --ammo;
            }
            loaded = true;
        }
        public bool Bipods
        {
            get => BaseGun.BipodsQ(this);
            set
            {
                var bipodsstate = BipodsState;
                if (isServerForObject)
                    BipodsState += 1f / 10 * (value ? 1 : -1);
                var nobipods = BipodsState < 0.01f;
                var bipods = BipodsState > 0.99f;
                _kickForce = bipods ? 0 : 4.67f;
                _ammoType.range = bipods ? 2500f : 1250f;
                _ammoType.bulletSpeed = bipods ? 150f : 48f;
                FrameId = FrameId % 10 + 10 * (bipods ? 2 : nobipods ? 0 : 1);
                if (isServerForObject && bipods && bipodsstate <= 0.99f)
                    BipOn.Play();
                if (isServerForObject && nobipods && bipodsstate >= 0.01f)
                    BipOff.Play();
            }
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
            Bipods = Bipods;
            if (duck == null) BipodsDisabled = false;
            else if (!BaseGun.BipodsQ(this, true)) BipodsDisabled = false;
            else if (duck.inputProfile.Pressed("QUACK")) BipodsDisabled = !BipodsDisabled;
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
                            _loadState++;
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
            laserSight = true;
            OnHoldAction();
        }

        public override void Fire()
        {
            if ((FrameId + 10) % (10 * NonSkinFrames) >= 20) return;
            base.Fire();
        }
        [UsedImplicitly]
        public float BipodsState
        {
            get => duck != null ? _bipodsstate : 0;
            set
            {
                value = Math.Max(value, 0f);
                value = Math.Min(value, 1f);
                _bipodsstate = value;
            }
        }

        private float _bipodsstate;
        [UsedImplicitly]
        public BitBuffer BipodsBuffer
        {
            get
            {
                var b = new BitBuffer();
                b.Write(Bipods);
                return b;
            }
            set => Bipods = value.ReadBool();
        }

        public StateBinding BipodsBinding { get; } = new StateBinding(nameof(BipodsBuffer));
        public bool BipodsDisabled { get; private set; }

        [UsedImplicitly]
        public StateBinding BsBinding { get; } = new StateBinding(nameof(BipodsState));
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
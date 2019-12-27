using DuckGame;
using JetBrains.Annotations;
using System.Collections.Generic;
using TMGmod.Core.WClasses;
using TMGmod.Core;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Bolt-Action")]
    // ReSharper disable once InconsistentNaming
    public class MSR : Sniper, IAmSr, IHaveSkin, IHaveBipods
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 9 });
        public MSR(float xval, float yval) : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            _sprite = new SpriteMap(GetPath("MSR"), 47, 12);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(24f, 6f);
            _collisionOffset = new Vec2(-24f, -6f);
            _collisionSize = new Vec2(47f, 12f);
            _barrelOffsetTL = new Vec2(47f, 4f);
            _flare = new SpriteMap(GetPath("FlareOnePixel3"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            ammo = 5;
            _ammoType = new ATSniper
            {
                bulletSpeed = 85f,
                range = 1200f,
                penetration = 2f,
                accuracy = 1f
            };
            _fireSound = GetPath("sounds/RifleOrMG.wav");
            _fullAuto = false;
            _kickForce = 5.5f;
            laserSight = false;
            _laserOffsetTL = new Vec2(31f, 9f);
            _holdOffset = new Vec2(14f, 0f);
            _editorName = "MSR";
			_weight = 4.65f;
        }
        public bool Bipods
        {
            get => BaseGun.HandleQ(this);
            set => _kickForce = value ? 1f : 5.5f;
        }
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
        public bool BipodsDisabled => false;

        [UsedImplicitly]

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
            Bipods = Bipods;
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
            var bublic= Skin.value;
            while (!Allowedlst.Contains(bublic))
            {
                bublic = Rando.Int(0, 9);
            }
            _sprite.frame = bublic;
        }
        [UsedImplicitly]
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
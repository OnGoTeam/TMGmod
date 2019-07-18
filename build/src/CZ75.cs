using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Semi-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class CZ75 : BaseGun, IAmHg, IHaveSkin
    {
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0 });
        private readonly SpriteMap _sprite;
        private int _fdelay;
        public CZ75(float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 24;
            _ammoType = new AT9mm
            {
                range = 80f,
                accuracy = 0.75f,
                penetration = 0.4f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("CZ75"), 12, 8);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(6f, 4f);
            _collisionOffset = new Vec2(-6f, -4f);
            _collisionSize = new Vec2(12f, 8f);
            _barrelOffsetTL = new Vec2(12f, 1f);
            _flare = new SpriteMap(GetPath("FlareOnePixel0"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _holdOffset = new Vec2(-1f, -2f);
            _fireSound = GetPath("sounds/1.wav");
            _fullAuto = false;
            _fireWait = 0.75f;
            _kickForce = 0.9f;
            loseAccuracy = 0.3f;
            maxAccuracyLost = 0.5f;
            _editorName = "CZ-75";
			_weight = 1f;
        }

        public override void OnPressAction()
        {
            if ((ammo > 0 && _sprite.frame == 10 || ammo > 12 && _sprite.frame == 0) && _fdelay == 0)
            {
                Fire();
            }
            else switch (ammo)
            {
                case 0:
                    DoAmmoClick();
                    break;
                case 12 when _sprite.frame == 0:
                    SFX.Play("click");
                    if (_raised)
                        Level.Add(new Czmag(x, y + 1));
                    else if (offDir < 0)
                        Level.Add(new Czmag(x + 5, y));
                    else
                        Level.Add(new Czmag(x - 5, y));
                    _sprite.frame = 10;
                    _fdelay = 40;
                    break;
                default:
                    DoAmmoClick();
                    break;
            }            
        }
        public override void Update()
        {
            base.Update();
            if (_fdelay > 1)
            {
                _fdelay -= 1;
            }
            else if (_fdelay == 1)
            {
                SFX.Play("click");
                _fdelay -= 1;
            }
        }
        private void UpdateSkin()
        {
            var bublic = Skin.value;
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
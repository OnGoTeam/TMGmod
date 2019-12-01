using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Combined")]
    public class Rfb : BaseAr, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 7 });
        public Rfb (float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 20;
            _ammoType = new ATMagnum
            {
                range = 380f,
                accuracy = 0.9f,
                penetration = 1f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("RFB"), 33, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(17f, 5f);
            _collisionOffset = new Vec2(-17f, -5f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _holdOffset = new Vec2(0f, 1f);
            ShellOffset = new Vec2(9f, -3f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = false;
            _fireWait = 0.46f;
            _kickForce = 1.7f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _editorName = "RFB";
			_weight = 5.5f;
		    Kforce2Ar = 0.7f;
        }
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (_fullAuto)
                {
                    _fullAuto = false;
                    _sprite.frame -= 10;
                    _fireWait = 0.46f;
                    maxAccuracyLost = 0.3f;
                    _ammoType.accuracy = 0.9f;
                }
                else
                {
                    _fullAuto = true;
                    _sprite.frame += 10;
                    _fireWait = 0.79f;
                    maxAccuracyLost = 0.45f;
                    _ammoType.accuracy = 0.6f;
                }
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }
            base.Update();
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
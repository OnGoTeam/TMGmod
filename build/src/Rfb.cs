using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Combined")]
    public class Rfb : BaseAr, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public readonly EditorProperty<int> Skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 7 });
        public Rfb (float xval, float yval)
          : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 20;
            _ammoType = new ATMagnum
            {
                range = 380f,
                accuracy = 0.89f,
                penetration = 1f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("RFBpattern"), 33, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(16.5f, 5.5f);
            _collisionOffset = new Vec2(-16.5f, -5.5f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 4f);
            _holdOffset = new Vec2(0f, 1f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = false;
            _fireWait = 0.46f;
            _kickForce = 1.7f;
            loseAccuracy = 0.08f;
            maxAccuracyLost = 0.3f;
            _editorName = "RFB";
			_weight = 6f;
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
                }
                else
                {
                    _fullAuto = true;
                    _sprite.frame += 10;
                    _fireWait = 0.79f;
                    maxAccuracyLost = 0.4f;
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
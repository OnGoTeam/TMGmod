using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper")]
    public class FnFcar: BaseAr, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public readonly EditorProperty<int> Skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 7 });
        public FnFcar (float xval, float yval)
          : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 14;
            _ammoType = new ATMagnum
            {
                range = 800f,
                accuracy = 1f,
                penetration = 1f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("FCARpattern"), 36, 15);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(18f, 7.5f);
            _collisionOffset = new Vec2(-18f, -7.5f);
            _collisionSize = new Vec2(36f, 15f);
            _barrelOffsetTL = new Vec2(37f, 6f);
            _holdOffset = new Vec2(3f, 0f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = false;
            _fireWait = 0.775f;
            _kickForce = 2.4f;
            Kforce2Ar = 0.9f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.2f;
            _editorName = "FN FCAR";
            laserSight = true;
            _laserOffsetTL = new Vec2(19f, 4f);
			_weight = 7f;
        }

        public override void Update()
        {
            if (duck != null && duck.height < 17f)
            {
                _kickForce = 0f;
				loseAccuracy = 0f;
                maxAccuracyLost = 0f;
                if ((_sprite.frame > -1) && (_sprite.frame < 10)) _sprite.frame += 10;
            }
            else
            {
                _kickForce = 2.4f;
                loseAccuracy = 0.15f;
                maxAccuracyLost = 0.2f;
                if ((_sprite.frame > 9) && (_sprite.frame < 20)) _sprite.frame -= 10;
            }
            base.Update();
        }
        public override void OnHoldAction()
        {
            if (_kickForce > 0f && _ammoType.accuracy > 0.1f) { _ammoType.accuracy -= 0.02f; } else { _ammoType.accuracy -= 0.0005f; }

            base.OnHoldAction();
        }
        public override void OnReleaseAction()
        {
            if (_ammoType.accuracy < 1f) _ammoType.accuracy += 0.1f;
            base.OnReleaseAction();
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
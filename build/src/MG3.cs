using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|LMG")]
    // ReSharper disable once InconsistentNaming
    public class MG3 : Gun, IAmLmg, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 3;
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public readonly EditorProperty<int> Skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0 });
		
		public MG3 (float xval, float yval)
          : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 80;
            _ammoType = new ATMagnum
            {
                range = 600f,
                accuracy = 0.8f,
                penetration = 1.5f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("mg3pattern"), 39, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(19.5f, 5.5f);
            _collisionOffset = new Vec2(-19.5f, -5.5f);
            _collisionSize = new Vec2(39f, 11f);
            _barrelOffsetTL = new Vec2(39f, 3f);
            _fireSound = GetPath("sounds/RifleOrMG.wav");
            _fullAuto = true;
            _fireWait = 0.5f;
            _kickForce = 0.95f;
            loseAccuracy = 0.025f;
            maxAccuracyLost = 0.1f;
            _holdOffset = new Vec2(4f, 1.5f);
            _editorName = "MG3";
			_weight = 7f;
        }
        public override void Update()
        {
            if (duck != null && duck.height < 17f)
            {
                _kickForce = 0f;
			    loseAccuracy = 0f;
                maxAccuracyLost = 0f;
                _sprite.frame %= 20;
                _sprite.frame += 20;
            }
            else
            {
                _kickForce = 0.95f;
                loseAccuracy = 0.025f;
                maxAccuracyLost = 0.1f;
                _sprite.frame %= 20;
            }
            base.Update();
		    if (ammo == 0 && ((_sprite.frame >= 0 && _sprite.frame < 10) || (_sprite.frame >= 20 && _sprite.frame < 30))) _sprite.frame += 10;
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
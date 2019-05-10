using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Burst")]
    public class M93R : BaseBurst, IAmHg, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        public EditorProperty<int> Skin { get; }
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 5 });

        public M93R(float xval, float yval)
            : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            _sprite = new SpriteMap(GetPath("M93Rpattern"), 12, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(6f, 2f);
            _collisionOffset = new Vec2(-6f, -2f);
            _collisionSize = new Vec2(12f, 9f);
            _barrelOffsetTL = new Vec2(13f, 2f);
            _holdOffset = new Vec2(-2f, 0f);
            ammo = 15;
            _ammoType = new ATMagnum {range = 70f, accuracy = 0.7f, penetration = 0.4f};
            _fireSound = GetPath("sounds/1.wav");
            _fullAuto = false;
            _fireWait = 0.9f;
            _kickForce = 0.24f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.6f;
            _editorName = "M93R";
			_weight = 2f;
            DeltaWait = 0.3f;
            BurstNum = 3;
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
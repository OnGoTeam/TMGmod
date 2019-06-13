using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class M14 : BaseGun, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        public EditorProperty<int> Skin { get; }
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0 });
        public M14(float xval, float yval)
          : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 10;
            _ammoType = new ATMagnum
            {
                range = 540f,
                accuracy = 0.85f
            };
            BaseAccuracy = 0.85f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("M14"), 46, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(23f, 6f);
            _collisionOffset = new Vec2(-23f, -6f);
            _collisionSize = new Vec2(46f, 11f);
            _barrelOffsetTL = new Vec2(46f, 3f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 0.85f;
            _kickForce = 3.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.25f;
            _holdOffset = new Vec2(6f, 1f);
            _editorName = "M14";
            _weight = 4.75f;
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
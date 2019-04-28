using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|DMR")]
    public class Arx200 : Gun, IHaveSkin, IAmDmr
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public readonly EditorProperty<int> Skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 6, 7 });
        public Arx200 (float xval, float yval)
          : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 20;
            _ammoType = new ATMagnum
            {
                range = 750f,
                accuracy = 0.95f,
                penetration = 1f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("ARX200pattern"), 33, 14);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(16f, 7f);
            _collisionOffset = new Vec2(-16.5f, -7f);
            _collisionSize = new Vec2(33f, 14f);
            _barrelOffsetTL = new Vec2(33f, 6f);
            _holdOffset = new Vec2(1f, -1f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = false;
            _fireWait = 0.65f;
            _kickForce = 1.1f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.15f;
            _editorName = "Beretta ARX-200";
			_weight = 6f;
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
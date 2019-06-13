using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{

    [EditorGroup("TMG|SMG|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class PPK42 : BaseSmg, IHaveSkin, IAmSmg
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        public EditorProperty<int> Skin { get; }
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 1, 2, 3, 4, 5, 6, 7 });
        public PPK42(float xval, float yval)
            : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 30;
            _ammoType = new AT9mm
            {
                range = 180f,
                accuracy = 0.8f,
                penetration = 1f
            };
            BaseAccuracy = 0.8f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("PPK42"), 25, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(13f, 5f);
            _collisionOffset = new Vec2(-13f, -5f);
            _collisionSize = new Vec2(25f, 11f);
            _barrelOffsetTL = new Vec2(25f, 2f);
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.45f;
            _kickForce = 1.2f;
            KforceDSmg = 2f;
            MaxDelaySmg = 20;
            _holdOffset = new Vec2(5f, 2f);
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.5f;
            _editorName = "PPK 42";
			_weight = 3f;
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
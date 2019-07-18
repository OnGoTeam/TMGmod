using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;
using JetBrains.Annotations;

namespace TMGmod
{

    [BaggedProperty("isInDemo", true), EditorGroup("TMG|SMG|Fully-Automatic")]
    [PublicAPI]
    public class M900 : BaseGun, IAmSmg, IHaveSkin
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
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0 });
        public M900(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 30;
            _ammoType = new AT9mm
            {
                range = Rando.Float(0f, 70f),
                accuracy = 0.4f,
                penetration = 0.4f
            };
            BaseAccuracy = 0.4f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("M900"), 27, 7);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(18f, 4f);
            _collisionOffset = new Vec2(-18f, -4f);
            _collisionSize = new Vec2(27f, 7f);
            _barrelOffsetTL = new Vec2(27f, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel0"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _fireSound = "smg";
            _fullAuto = true;
            _fireWait = 0.3f;
            _kickForce = 0.3f;
            _holdOffset = new Vec2(7.5f, 1.5f);
            loseAccuracy = 0.01f;
            maxAccuracyLost = 0.05f;
            _editorName = "Calico M900";
			_weight = 1f;
            handAngle = 0f;
        }
        public override void OnHoldAction()
        {
            _ammoType.range = Rando.Float(0f, Rando.Float(0f, 67f)) + 5f;
            handAngle = Rando.Float(-0.1f, 0.1f);
            base.OnHoldAction();
        }
        public override void OnReleaseAction()
        {
            handAngle = 0f;
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
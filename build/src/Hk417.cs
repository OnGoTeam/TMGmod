using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    /// <inheritdoc cref="BaseBurst"/>
    /// <inheritdoc cref="IAmDmr"/>
    /// <inheritdoc cref="IHaveSkin"/>
    [EditorGroup("TMG|Rifle|DMR")]
    // ReSharper disable once InconsistentNaming
    public class HK417 : BaseGun, IAmDmr, IHaveSkin
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
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 1, 4, 7 });

        /// <inheritdoc />
        public HK417 (float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(4, this, -1f, 9f, 0.5f);
            ammo = 10;
            _ammoType = new ATMagnum
            {
                range = 300f,
                accuracy = 0.9f,
                penetration = 1f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Hk417"), 30, 10);
            _graphic = _sprite;
            _sprite.frame = 4;
            _center = new Vec2(15f, 5f);
            _collisionOffset = new Vec2(-14.5f, -5f);
            _collisionSize = new Vec2(30f, 10f);
            _barrelOffsetTL = new Vec2(31f, 3f);
            _holdOffset = new Vec2(0f, 1f);
            _fireSound = GetPath("sounds/HeavyRifle.wav");
            _fullAuto = false;
            _fireWait = 0.8f;
            _kickForce = 2.7f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.5f;
            _editorName = "Hk-417C";
			_weight = 3.5f;
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

        /// <inheritdoc />
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
        /// <summary>
        /// Updates skin when Skin's changed
        /// </summary>
        /// <param name="property"></param>
        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}
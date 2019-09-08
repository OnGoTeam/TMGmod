using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;


namespace TMGmod.Custom_Guns
{
    /// <inheritdoc cref="BaseGun"/>
    /// <inheritdoc cref="IHaveSkin"/>
    /// <inheritdoc cref="IAmLmg"/>
    [EditorGroup("TMG|LMG|Custom")]
    // ReSharper disable once InconsistentNaming
    public class MG44C : BaseGun, IHaveSkin, IAmLmg
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        /// <inheritdoc />
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        private static readonly List<int> Allowedlst = new List<int>(new[] { 0 });

        /// <inheritdoc />
        public MG44C(float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 30;
            _ammoType = new ATMagnum
            {
                range = 750f,
                accuracy = 0.75f,
                penetration = 1.5f
            };
            BaseAccuracy = 0.75f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("mg44reqNATO"), 39, 12);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(19.5f, 6f);
            _collisionOffset = new Vec2(-19.5f, -6f);
            _collisionSize = new Vec2(39f, 12f);
            _barrelOffsetTL = new Vec2(39f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.9f;
            _kickForce = 1.8f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.6f;
            _holdOffset = new Vec2(4f, 0f);
            ShellOffset = new Vec2(-5f, -2f);
            _editorName = "Magnium NATOmag";
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

        /// <inheritdoc />
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        /// <inheritdoc />
        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}
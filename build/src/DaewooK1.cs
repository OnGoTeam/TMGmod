using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|PDW")]
    public class DaewooK1 : BaseSmg, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        public bool Stock;
        public StateBinding StockBinding = new StateBinding(nameof(Stock));
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 1, 2, 3, 4, 7 });
        public DaewooK1 (float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 32;
            _ammoType = new ATMagnum
            {
                range = 245f,
                accuracy = 0.83f,
                penetration = 1f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("DaewooK1"), 28, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(14f, 5f);
            _collisionOffset = new Vec2(-14f, -5f);
            _collisionSize = new Vec2(28f, 11f);
            _barrelOffsetTL = new Vec2(28f, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _holdOffset = new Vec2(-2f, 1f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 0.86f;
            _kickForce = 0.5f;
            KforceDSmg = 2.5f;
            MaxDelaySmg = 50;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.24f;
            _editorName = "Daewoo K1";
			_weight = 4.5f;
        }

        

        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (Stock)
                {
                    FrameId -= 10;
                    loseAccuracy = 0.1f;
                    maxAccuracyLost = 0.24f;
                    weight = 4.5f;
                    Stock = false;
                }
                else
                {
                    FrameId += 10;
                    loseAccuracy = 0.2f;
                    maxAccuracyLost = 0.36f;
                    weight = 3f;
                    Stock = true;
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
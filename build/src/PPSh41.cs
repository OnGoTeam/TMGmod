using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{

    [EditorGroup("TMG|SMG|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class PPSh41 : BaseSmg, IHaveSkin, IAmSmg
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
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        public PPSh41(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 71;
            _ammoType = new AT9mm
            {
                range = 200f,
                accuracy = 0.73f,
                penetration = 0.4f
            };
            BaseAccuracy = 0.73f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("PPSH41"), 30, 8);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(15f, 4f);
            _collisionOffset = new Vec2(-15f, -4f);
            _collisionSize = new Vec2(30f, 8f);
            _barrelOffsetTL = new Vec2(30f, 2f);
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.25f;
            _kickForce = 1.2f;
            KforceDSmg = 4f;
            MaxDelaySmg = 80;
            _holdOffset = new Vec2(3f, 1f);
            loseAccuracy = 0.05f;
            maxAccuracyLost = 0.4f;
            _editorName = "PPSh 41";
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
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Fully-Automatic")]
    public class Vintorez : BaseAr, ISpeedAccuracy, IHaveSkin
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
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 1, 7 });
        public Vintorez(float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 10;
            _ammoType = new AT9mmS
            {
                range = 550f,
                accuracy = 0.9f,
                bulletSpeed = 25f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Vintorez"), 33, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(16f, 6f);
            _collisionOffset = new Vec2(-16f, -6f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(34f, 5f);
            _holdOffset = new Vec2(3f, 0f);
            _fireSound = GetPath("sounds/Silenced1.wav");
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _fullAuto = true;
            _fireWait = 0.7f;
            _kickForce = 2.85f;
            loseAccuracy = 0.08f;
            maxAccuracyLost = 0.15f;
            _editorName = "Vintorez";
			_weight = 4.7f;
            MinAccuracy = 0f;
            BaseAccuracy = 0.9f;
            Kforce1Ar = 0.4f;
            Kforce2Ar = 0.85f;
            MuAccuracySr = 1f;
            LambdaAccuracySr = 0.5f;
        }
        public float MuAccuracySr { get; }
        public float LambdaAccuracySr { get; }
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
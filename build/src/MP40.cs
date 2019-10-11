using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{

    [BaggedProperty("isInDemo", true), EditorGroup("TMG|SMG|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class MP40 : BaseSmg, IFirstPrecise, IHaveSkin, I5
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
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 5, 7, 8 });
        public MP40(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 32;
            _ammoType = new AT9mm
            {
                range = 190f,
                accuracy = 0.5f,
                penetration = 0.7f
            };
            BaseAccuracy = 0.5f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("MP40"), 23, 14);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(12f, 7f);
            _collisionOffset = new Vec2(-12f, -7f);
            _collisionSize = new Vec2(23f, 14f);
            _barrelOffsetTL = new Vec2(23f, 1f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.5f;
            _kickForce = 0.5f;
            _holdOffset = new Vec2(4f, 4f);
            ShellOffset = new Vec2(-1f, -6f);
            _editorName = "MP40";
			_weight = 3f;
            KforceDSmg = 2.5f;
            MaxDelaySmg = 20;
            MaxAccuracy = 1f;
            MaxDelayFp = 10;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.4f;
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
        public int CurrDelay { get; set; }
        public int MaxDelayFp { get; }
        public float MaxAccuracy { get; }
    }
}

using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Combined")]
    // ReSharper disable once InconsistentNaming
    public class MP5SD : BaseBurst, IFirstKforce, IFirstPrecise, IHaveSkin, IAmSmg
    {

        private readonly SpriteMap _sprite;
        [UsedImplicitly]
        public bool NonAuto = true;
        [UsedImplicitly]
        public StateBinding NonAutoBinding = new StateBinding(nameof(NonAuto));
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 2, 3, 4, 6, 7 });
        public MP5SD(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 30;
            _ammoType = new AT9mmS
            {
                range = 235f,
                accuracy = 0.8f
            };
            BaseAccuracy = 0.8f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("MP5SD"), 31, 12);
            _graphic = _sprite;
            _sprite.frame = 0;
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _center = new Vec2(15.5f, 6f);
            _collisionOffset = new Vec2(-15.5f, -6f);
            _collisionSize = new Vec2(31f, 12f);
            _barrelOffsetTL = new Vec2(31f, 2f);
            _fireSound = GetPath("sounds/Silenced2.wav");
            _fullAuto = false;
            _fireWait = 0.5f;
            _kickForce = 0.5f;
            _holdOffset = new Vec2(0f, 2f);
            ShellOffset = new Vec2(0f, 0f);
            _editorName = "MP5SD";
			_weight = 3f;
            KforceDSmg = 2f;
            MaxAccuracy = 0.9f;
            MaxDelayFp = 10;
            MaxDelaySmg = 50;
            DeltaWait = 0.45f;
            BurstNum = 1;
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
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (NonAuto)
                {
                    NonAuto = false;
                    BurstNum = 3;
                    _fireWait = 1.8f;
                    _sprite.frame %= 10;
                    _sprite.frame += 10;
                }
                else
                {
                    NonAuto = true;
                    BurstNum = 1;
                    _fireWait = 0.5f;
                    _sprite.frame %= 10;
                }
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }
            base.Update();
        }
        public float KforceDSmg { get; }
        public int CurrDelaySmg { get; set; }
        public int CurrDelay { get; set; }
        public int MaxDelayFp { get; }
        public int MaxDelaySmg { get; }
        public float MaxAccuracy { get; }
        [UsedImplicitly]
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

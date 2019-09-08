using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Fully-Automatic")]
    public class SpectreM4 : BaseSmg, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        public bool Silencer;
        public StateBinding StockBinding = new StateBinding(nameof(Silencer));
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 6 });
        public SpectreM4(float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 30;
            _ammoType = new AT9mm
            {
                range = 145f,
                accuracy = 0.76f,
                penetration = 1f,
                bulletSpeed = 16f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("SpectreM4"), 19, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(9.5f, 5f);
            _collisionOffset = new Vec2(-9.5f, -5f);
            _collisionSize = new Vec2(19f, 10f);
            _barrelOffsetTL = new Vec2(13f, 1f);
            _fireSound = GetPath("sounds/smg.wav");
            _fullAuto = true;
            _fireWait = 0.31f;
            _kickForce = 0.8f;
            KforceDSmg = 2.7f;
            MaxDelaySmg = 50;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.34f;
            _holdOffset = new Vec2(3f, 3f);
            ShellOffset = new Vec2(0f, 0f);
            _editorName = "Spectre M4";
            _weight = 3.3f;
        }
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (Silencer)
                {
                    FrameId -= 10;
                    _ammoType = new AT9mm
                    {
                        range = 145f,
                        accuracy = 0.76f,
                        penetration = 1f,
                        bulletSpeed = 16f
                    };
                    _barrelOffsetTL = new Vec2(13f, 1f);
                    loseAccuracy = 0.1f;
                    maxAccuracyLost = 0.34f;
                    weight = 3.3f;
                    _fireSound = GetPath("sounds/smg.wav");
                    _flare = new SpriteMap("smallFlare", 11, 10)
                    {
                        center = new Vec2(0.0f, 5f)
                    };
                    Silencer = false;
                }
                //TODO: botl
                else
                {
                    FrameId += 10;
                    _ammoType = new AT9mmS
                    {
                        range = 167f,
                        accuracy = 0.92f,
                        penetration = 1f
                    };
                    _barrelOffsetTL = new Vec2(16f, 1f);
                    loseAccuracy = 0.07f;
                    maxAccuracyLost = 0.3f;
                    weight = 3.8f;
                    _fireSound = GetPath("sounds/SilencedPistol.wav");
                    _flare = new SpriteMap(GetPath("takezis"), 4, 4);
                    Silencer = true;
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
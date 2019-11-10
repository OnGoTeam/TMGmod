using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Semi-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class USP : BaseGun, IAmHg, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        [UsedImplicitly]
        public bool Silencer
        {
            get => _fireSound == GetPath("sounds/SilencedPistol.wav");
            set
            {
                if (value)
                {
                    _sprite.frame %= 10;
                    _sprite.frame += 10;
                    _fireSound = GetPath("sounds/SilencedPistol.wav");
                    _flare = new SpriteMap(GetPath("takezis"), 4, 4);
                    _ammoType = new AT9mmS
                    {
                        range = 130f,
                        accuracy = 0.9f
                    };
                    _barrelOffsetTL = new Vec2(23f, 2f);
                }
                else
                {
                    _sprite.frame %= 10;
                    _flare = new SpriteMap(GetPath("FlareOnePixel0"), 13, 10)
                    {
                        center = new Vec2(0.0f, 5f)
                    };
                    _fireSound = GetPath("sounds/1.wav");
                    _ammoType = new AT9mm
                    {
                        range = 100f,
                        accuracy = 0.8f
                    };
                    _barrelOffsetTL = new Vec2(14f, 2f);
                }
            }
        }
        [UsedImplicitly]
        public StateBinding SilencerBinding = new StateBinding(nameof(Silencer));
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 2, 3, 4, 7 });
        public USP(float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 13;
            _ammoType = new AT9mm
            {
                range = 100f,
                accuracy = 0.8f,
                penetration = 1f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("USP"), 23, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(8f, 3f);
            _collisionOffset = new Vec2(-7.5f, -3.5f);
            _collisionSize = new Vec2(23f, 9f);
            _barrelOffsetTL = new Vec2(14f, 3f);
            _fireSound = GetPath("sounds/1.wav");
            _fullAuto = false;
            _fireWait = 0.75f;
            _kickForce = 1f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.4f;
            ShellOffset = new Vec2(-3f, -1f);
            _editorName = "USP-S";
			_weight = 1f;
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
                SFX.Play(Silencer ? GetPath("sounds/silencer_off.wav") : GetPath("sounds/silencer_on.wav"));
                Silencer = !Silencer;
            }
            base.Update();
        }
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
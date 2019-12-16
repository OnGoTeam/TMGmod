using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|DMR")]
    // ReSharper disable once InconsistentNaming
    public class TC12 : BaseGun, IAmDmr, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 2;
        [UsedImplicitly]
        public bool Silencer;
        [UsedImplicitly]
        public StateBinding SilencerBinding = new StateBinding(nameof(Silencer));
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 3 });

        public TC12 (float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 10;
            _ammoType = new AT9mm
            {
                range = 270f,
                accuracy = 0.91f,
                penetration = 1f
            };
            BaseAccuracy = 0.91f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("TC-12"), 39, 12);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(19.5f, 5f);
            _collisionOffset = new Vec2(-19.5f, -5f);
            _collisionSize = new Vec2(39f, 12f);
            _barrelOffsetTL = new Vec2(28f, 3f);
            _flare = new SpriteMap(GetPath("FlareTC12"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _holdOffset = new Vec2(5f, 0f);
            ShellOffset = new Vec2(-14f, -1f);
            _fireSound = "deepMachineGun2";
            _fullAuto = false;
            _fireWait = 1.03f;
            _kickForce = 5.3f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            laserSight = true;
            _laserOffsetTL = new Vec2(26f, 5.5f);
            _editorName = "TC-12";
			_weight = 4.5f;
        }
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (Silencer)
                {
                    FrameId -= 10;
                    _fireSound = "deepMachineGun2";
                    _ammoType = new AT9mm
                    {
                        range = 270f,
                        accuracy = 0.91f,
                        penetration = 1f
                    };
                    _kickForce = 5.3f;
                    loseAccuracy = 0.1f;
                    _weight = 4.5f;
                    _barrelOffsetTL = new Vec2(28f, 3f);
                    _flare = new SpriteMap(GetPath("FlareTC12"), 13, 10)
                    {
                        center = new Vec2(0.0f, 5f)
                    };
                }
                else
                {
                    FrameId += 10;
                    _fireSound = GetPath("sounds/Silenced3.wav");
                    _ammoType = new AT9mmS
                    {
                        range = 330f,
                        accuracy = 0.97f
                    };
                    _kickForce = 4.5f;
                    loseAccuracy = 0f;
                    _weight = 6.3f;
                    _barrelOffsetTL = new Vec2(39f, 3f);
                    _flare = new SpriteMap(GetPath("FlareSilencer"), 13, 10)
                    {
                        center = new Vec2(0.0f, 5f)
                    };
                }
                SFX.Play(Silencer ? GetPath("sounds/silencer_off.wav") : GetPath("sounds/silencer_on.wav"));
                Silencer = !Silencer;
                SFX.Play("quack", -1);
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
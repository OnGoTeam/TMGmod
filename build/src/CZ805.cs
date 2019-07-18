using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class CZ805 : BaseAr, IHaveSkin
    {

        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 10;
        public bool Silencer;
        public StateBinding SilencerBinding = new StateBinding(nameof(Silencer));
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        /// <inheritdoc />
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 3, 4, 5, 7 });
        public CZ805 (float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 30;
		    _ammoType = new AT9mm
		    {
		        range = 330f,
		        accuracy = 0.87f,
		        penetration = 1f
		    };
		    _type = "gun";
            _sprite = new SpriteMap(GetPath("CZ805Bren"), 41, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(20.5f, 5.5f);
            _collisionOffset = new Vec2(-20.5f, -5.5f);
            _collisionSize = new Vec2(41f, 11f);
            _barrelOffsetTL = new Vec2(39f, 3.5f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _holdOffset = new Vec2(5f, 1f);
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.9f;
            _kickForce = 2.76f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _editorName = "CZ-805 BREN";
			_weight = 5f;
		    Kforce2Ar = 0.7f;
        }
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (Silencer)
                {
                    FrameId -= 50;
                    _fireSound = "deepMachineGun2";
                    _ammoType = new AT9mm
                    {
                        range = 330f,
                        accuracy = 0.87f
                    };
                    loseAccuracy = 0.15f;
                    maxAccuracyLost = 0.35f;
                    _barrelOffsetTL = new Vec2(39f, 4f);
                    Silencer = false;
                    _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
                    {
                        center = new Vec2(0.0f, 5f)
                    };
                }
                else
                {
                    FrameId += 50;
                    _fireSound = GetPath("sounds/Silenced2.wav");
                    _ammoType = new AT9mmS
                    {
                        range = 380f,
                        accuracy = 0.95f
                    };
                    loseAccuracy = 0.1f;
                    maxAccuracyLost = 0.3f;
                    _barrelOffsetTL = new Vec2(42.5f, 4f);
                    Silencer = true;
                    _flare = new SpriteMap(GetPath("takezis"), 4, 4);
                }
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }

            if (ammo > 20 && ammo <= 26 && FrameId / 10 % 5 != 1) _sprite.frame += 10;
            if (ammo > 12 && ammo <= 20 && FrameId / 10 % 5 != 2) _sprite.frame += 10;
            if (ammo > 5 && ammo <= 12 && FrameId / 10 % 5 != 3) _sprite.frame += 10;
            if (ammo > 0 && ammo <= 5 && FrameId / 10 % 5 != 4) _sprite.frame += 10;
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
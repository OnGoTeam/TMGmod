using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|DMR")]
    // ReSharper disable once InconsistentNaming
    public class CZC2 : BaseAr, IAmDmr, IHaveSkin
    {

        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 2;
        public bool Silencer;
        public StateBinding SilencerBinding = new StateBinding(nameof(Silencer));
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public readonly EditorProperty<int> Skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0 });
        public CZC2(float xval, float yval)
          : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 23;
            _ammoType = new AT9mm
            {
                range = 330f,
                accuracy = 0.87f,
                penetration = 1f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("CZC2pattern"), 41, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(20.5f, 5.5f);
            _collisionOffset = new Vec2(-20.5f, -5.5f);
            _collisionSize = new Vec2(41f, 11f);
            _barrelOffsetTL = new Vec2(39f, 3.5f);
            _holdOffset = new Vec2(5f, 1f);
            _fireSound = "deepMachineGun2";
            _fullAuto = false;
            _fireWait = 0.9f;
            _kickForce = 1.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _editorName = "CZ C2-SAR";
            _weight = 4.4f;
            Kforce2Ar = 0.7f;
        }
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (Silencer)
                {
                    Silencer = false;
                    FrameId -= 10;
                    _fireSound = "deepMachineGun2";
                    _ammoType = new AT9mm
                    {
                        range = 310f,
                        accuracy = 0.87f
                    };
                    loseAccuracy = 0.1f;
                    maxAccuracyLost = 0.3f;
                    _barrelOffsetTL = new Vec2(39f, 4f);
                    _flare = new SpriteMap("smallFlare", 11, 10)
                    {
                        center = new Vec2(0.0f, 5f)
                    };
                }
                else
                {
                    Silencer = true;
                    FrameId += 10;
                    _fireSound = GetPath("sounds/Silenced2.wav");
                    _ammoType = new AT9mmS
                    {
                        range = 360f,
                        accuracy = 0.95f
                    };
                    loseAccuracy = 0.15f;
                    maxAccuracyLost = 0.28f;
                    _barrelOffsetTL = new Vec2(42.5f, 4f);
                    _flare = new SpriteMap(GetPath("takezis"), 4, 4);
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
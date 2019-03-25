using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    public class SpectreM4 : BaseSmg, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        public bool Silencer;
        public StateBinding StockBinding = new StateBinding(nameof(Silencer));
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public readonly EditorProperty<int> Skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0 });
        public SpectreM4(float xval, float yval)
          : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 30;
            _ammoType = new AT9mm
            {
                range = 145f,
                accuracy = 0.83f,
                penetration = 1f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("SpectreM4pattern"), 19, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(9.5f, 5f);
            _collisionOffset = new Vec2(-9.5f, -5f);
            _collisionSize = new Vec2(19f, 10f);
            _barrelOffsetTL = new Vec2(14f, 2f);
            _holdOffset = new Vec2(3f, 0f);
            _fireSound = GetPath("sounds/smg.wav");
            _fullAuto = true;
            _fireWait = 0.31f;
            _kickForce = 0.8f;
            KforceDSmg = 2.7f;
            MaxDelaySmg = 50;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.34f;
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
                        accuracy = 0.83f,
                        penetration = 1f
                    };
                    loseAccuracy = 0.1f;
                    maxAccuracyLost = 0.34f;
                    weight = 3.3f;
                    Silencer = false;
                }
                else
                {
                    FrameId += 10;
                    _ammoType = new AT9mmS
                    {
                        range = 167f,
                        accuracy = 0.92f,
                        penetration = 1f
                    };
                    loseAccuracy = 0.07f;
                    maxAccuracyLost = 0.3f;
                    weight = 3.8f;
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
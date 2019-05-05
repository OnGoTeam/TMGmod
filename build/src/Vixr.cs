using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|PDW")]
    [PublicAPI]
    public class Vixr : BaseGun, IAmAr, IHaveSkin
    {
		public bool Stockngrip = true;
        public StateBinding StockBinding = new StateBinding(nameof(Stockngrip));
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public readonly EditorProperty<int> Skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 6 });

        public Vixr(float xval, float yval)
          : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 25;
            _ammoType = new AT9mmS
            {
               range = 300f,
               accuracy = 0.88f,
               bulletSpeed = 21f
            };
            BaseAccuracy = 0.88f;
            _type = "gun";
            //I'M BLUE DARUDE SANDSTORM DA DUBAI
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _sprite = new SpriteMap(GetPath("Vixrpattern"), 33, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(16.5f, 4.5f);
            _collisionOffset = new Vec2(-16.5f, -4.5f);
            _collisionSize = new Vec2(33f, 9f);
            _barrelOffsetTL = new Vec2(34f, 3.5f);
            _holdOffset = new Vec2(3f, 0f);
            _fireSound = GetPath("sounds/Silenced1.wav");
            _fullAuto = true;
            _fireWait = 0.65f;
            _kickForce = 1.8f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.2f;
            _editorName = "VIXR";
			_weight = 3.9f;
            handAngle = 0f;
        }
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (Stockngrip)
                {
                    _sprite.frame += 10;
                    loseAccuracy = 0.13f;
                    maxAccuracyLost = 0.4f;
                    Stockngrip = false;
                    weight = 2f;
                }
                else
                {
                    _sprite.frame -= 10;
                    loseAccuracy = 0.099f;
                    maxAccuracyLost = 0.17f;
                    Stockngrip = true;
                    weight = 3.9f;
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
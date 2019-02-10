using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    public class DaewooK1 : BaseSmg, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        public bool Stock;
        public StateBinding StockBinding = new StateBinding(nameof(Stock));
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public DaewooK1 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 40;
            _ammoType = new ATMagnum
            {
                range = 345f,
                accuracy = 0.83f,
                penetration = 1f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("DaewooK1pattern"), 28, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(14f, 5f);
            _collisionOffset = new Vec2(-14f, -5f);
            _collisionSize = new Vec2(28f, 11f);
            _barrelOffsetTL = new Vec2(28f, 3f);
            _holdOffset = new Vec2(-2f, 2f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 0.86f;
            _kickForce = 0.5f;
            KforceDSmg = 2.5f;
            MaxDelaySmg = 50;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.24f;
            _editorName = "Daewoo K1";
			_weight = 4.5f;
        }

        

        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (Stock)
                {
                    FrameId -= 10;
                    loseAccuracy = 0.1f;
                    maxAccuracyLost = 0.24f;
                    weight = 4.5f;
                    Stock = false;
                }
                else
                {
                    FrameId += 10;
                    loseAccuracy = 0.2f;
                    maxAccuracyLost = 0.36f;
                    weight = 3f;
                    Stock = true;
                }
            }
            base.Update();
		}

        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}
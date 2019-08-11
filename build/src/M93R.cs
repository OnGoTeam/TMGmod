using DuckGame;


// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Pistol")]
    public class M93R : Gun
    {
        private int _burstNumB;
        private readonly int _burstValue;
        private float _bw = 5.1f;
		private readonly SpriteMap _sprite;
        private readonly int _teksturka;

        public M93R(float xval, float yval)
            : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("M93Rpatterns"), 12, 9);
            _graphic = _sprite;
            _teksturka = Rando.Int(0, 4);
            _sprite.frame = _teksturka;
            _center = new Vec2(6f, 2f);
            _collisionOffset = new Vec2(-6f, -2f);
            _collisionSize = new Vec2(12f, 9f);
            _barrelOffsetTL = new Vec2(13f, 2f);
            _holdOffset = new Vec2(-2f, 0f);
            ammo = 15;
            _ammoType = new ATMagnum();
            _fireSound = GetPath("sounds/1.wav");
            _fullAuto = true;
            _ammoType.range = 108f;
            _fireWait = 0.27f;
            _kickForce = 0.24f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.2f;
            _ammoType.range = 110f;
            _editorName = "M93R";
            _burstValue = 3;
			_weight = 2f;
        }

        public override void Fire()
        {
           // base.Fire();
        }
        public override void Update()
        {
            //object obj;

            if (_burstNumB > 0 && _bw > 0.1f)
            {
                base.Fire();
                _burstNumB -= 1;
                _bw = 0;
            }
            else
            {
                _bw += 0.1f;
            }
            base.Update();
        }
        public override void OnPressAction()
        {
            if (!(_bw > 1f)) return;
            _bw = 0.3f;
            _burstNumB = _burstValue;
        }
		public override void Draw()
        {
            _sprite.frame = _teksturka;
            base.Draw();
        }
	}
}
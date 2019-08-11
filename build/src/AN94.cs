using DuckGame;
using JetBrains.Annotations;


// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Machinegun")]
    // ReSharper disable once InconsistentNaming
    public class AN94 : Gun
    {
        private readonly SpriteMap _sprite;

        private int _burstNumB;
        private readonly int _burstValue;
        private float _bw = 5.1f;

        private bool _stock;
        [UsedImplicitly]
		public EditorProperty<bool> Laser = new EditorProperty<bool>(false, null, 0f, 1f, 1f);

        public AN94(float xval, float yval)
            : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("AN94SM"), 33, 9);
            _graphic = _sprite;
            _center = new Vec2(16f, 5f);
            _collisionOffset = new Vec2(-15f, -5f);
            _collisionSize = new Vec2(33f, 9f);
            _barrelOffsetTL = new Vec2(34f, 3f);
            _holdOffset = new Vec2(2f, 2f);
            ammo = 30;
            _ammoType = new ATMagnum();
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.1f;
            _kickForce = 0.9f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.1f;
            _ammoType.range = 310f;
            _editorName = "AN94";
            _burstValue = 2;
			_weight = 5.5f;
            _laserOffsetTL = new Vec2(30f, 2.5f);
            _sprite.AddAnimation("base", 0f, false, 0);
            _sprite.AddAnimation("stock", 0f, false, 1);
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
			
            if (owner != null)
            {
                if (isServerForObject)
                {
                    if (duck.inputProfile.Pressed("QUACK"))
                    {
					    if (_stock)
					    {
                            loseAccuracy = 0.15f;
						    _weight = 5.5f;
					        _sprite.SetAnimation("base");
                            maxAccuracyLost = 0.1f;
						    _stock = false;
					    }
                        else
					    {
                            loseAccuracy = 0.2f;
				            _weight = 2.75f;
					        _sprite.SetAnimation("stock");
                            maxAccuracyLost = 0.3f;
						    _stock = true;
					    }
					}
				}
			}
            base.Update();
        }
        public override void OnPressAction()
        {
            if (_bw > 1f)
            {
                _bw = 0.2f;
                _burstNumB = _burstValue;
            }
        }
        public override void Initialize()
        {
			if (!(Level.current is Editor))
            {
                if (Laser.value)
                {
                    laserSight = true;
                }
            }
            base.Initialize();
        }
	}
}
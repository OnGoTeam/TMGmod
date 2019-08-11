using DuckGame;
using JetBrains.Annotations;


// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Misc|Custom Guns")]
    // ReSharper disable once InconsistentNaming
    public class AN94C : Gun
    {
        private int _burstNumB;
        private readonly int _burstValue;
        private float _bw = 5.1f;
        [UsedImplicitly]
        public EditorProperty<bool> Laser = new EditorProperty<bool>(false, null, 0f, 1f, 1f);

        public AN94C(float xval, float yval)
            : base(xval, yval)
        {
            _graphic = new Sprite(GetPath("AN94notfol"));
            _center = new Vec2(16f, 5f);
            _collisionOffset = new Vec2(-15f, -6f);
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
            _editorName = "AN94 Fixed Stock";
            _burstValue = 2;
			_weight = 5f;
            _laserOffsetTL = new Vec2(30f, 2.5f);
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
            _bw = 0.2f;
            _burstNumB = _burstValue;
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
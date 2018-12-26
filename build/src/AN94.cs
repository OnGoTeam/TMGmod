using DuckGame;
using TMGmod.Core.WClasses;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    // ReSharper disable once InconsistentNaming
    public class AN94 : BaseBurst
    {
        private readonly SpriteMap _sprite;

        public bool Stock;
        public StateBinding StockBinding = new StateBinding(nameof(Stock));
        // ReSharper disable once MemberCanBePrivate.Global
        public readonly EditorProperty<bool> Laser = new EditorProperty<bool>(false, null, 0f, 1f, 1f);

        public AN94(float xval, float yval)
            : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("AN94SM"), 33, 9);
            graphic = _sprite;
            center = new Vec2(16f, 5f);
            collisionOffset = new Vec2(-15f, -5f);
            collisionSize = new Vec2(33f, 9f);
            _barrelOffsetTL = new Vec2(34f, 3f);
            _holdOffset = new Vec2(2f, 2f);
            ammo = 30;
            _ammoType = new ATMagnum {range = 310f, bulletSpeed = 180f};
            _fireSound = "deepMachineGun2";
            _fullAuto = false;
            _fireWait = 0.1f;
            _kickForce = 0.9f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.1f;
            _editorName = "AN94";
			weight = 5.5f;
            _laserOffsetTL = new Vec2(30f, 2.5f);
            _sprite.AddAnimation("base", 0f, false, 0);
            _sprite.AddAnimation("stock", 0f, false, 1);
            DeltaWait = 0.07f;
            BurstNum = 2;
        }
        
        public override void Update()
        {
            if (duck != null)
            {
                if (duck.inputProfile.Pressed("QUACK"))
                {
                    if (Stock)
                    {
                        loseAccuracy = 0.15f;
                        weight = 5.5f;
                        _sprite.SetAnimation("base");
                        maxAccuracyLost = 0.1f;
                        Stock = false;
                    }
                    else
                    {
                        loseAccuracy = 0.2f;
                        weight = 2.75f;
                        _sprite.SetAnimation("stock");
                        maxAccuracyLost = 0.3f;
                        Stock = true;
                    }
                }
			}
            base.Update();
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
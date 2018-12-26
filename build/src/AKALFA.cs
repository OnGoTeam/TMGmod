using DuckGame;
using TMGmod.Core.WClasses;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    // ReSharper disable once InconsistentNaming
    public class AKALFA : DefaultAr
    {
        private readonly SpriteMap _sprite;
        public bool Stock;
        public StateBinding StockBinding = new StateBinding(nameof(Stock));

        public AKALFA (float xval, float yval)
          : base(xval, yval)
		{
            ammo = 20;
            _ammoType = new AT9mm
            {
                range = 425f,
                accuracy = 1f,
                penetration = 1.5f,
                bulletSpeed = 60f,
                bulletThickness = 0.87f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("ALFASM"), 38, 9);
		    graphic = _sprite;
            center = new Vec2(19f, 4.5f);
            collisionOffset = new Vec2(-19f, -4.5f);
            collisionSize = new Vec2(38f, 9f);
            _barrelOffsetTL = new Vec2(38f, 2.5f);
            _holdOffset = new Vec2(5f, 0f);
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.75f;
            _kickForce = 0.65f;
		    Kforce1Ar = 0.05f;
		    Kforce2Ar = 0.75f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0.3f;
            _editorName = "Alfa";
			weight = 5.5f;
            _laserOffsetTL = new Vec2(31f, 4f);
            laserSight = true;
		    _sprite.AddAnimation("base", 0f, false, 0);
		    _sprite.AddAnimation("stock", 0f, false, 1);
		}
        public override void Update()
        {
            if (duck != null)
            {
                if (duck.inputProfile.Pressed("QUACK"))
                {
                    if (Stock)
                    {
                        _sprite.SetAnimation("base");
                        _ammoType.accuracy = 1f;
                        loseAccuracy = 0f;
                        Stock = false;
                        weight = 5.5f;
                    }
                    else
                    {
                        _sprite.SetAnimation("stock");
                        _ammoType.accuracy = 0.92f;
                        loseAccuracy = 0.045f;
                        Stock = true;
                        weight = 3f;
                    }
                }
			}
		    base.Update();
		}
    }
}
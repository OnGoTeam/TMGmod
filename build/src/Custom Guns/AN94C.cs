using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Custom_Guns
{
    [EditorGroup("TMG|Machinegun|Custom")]
    // ReSharper disable once InconsistentNaming
    public class AN94C : BaseBurst
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public readonly EditorProperty<bool> Laser = new EditorProperty<bool>(false, null, 0f, 1f, 1f);

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
            _ammoType = new ATMagnum { range = 310f, bulletSpeed = 180f };
            _fireSound = "deepMachineGun2";
            _fullAuto = false;
            _fireWait = 0.1f;
            _kickForce = 0.9f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.1f;
            _editorName = "AN94 Fixed Stock";
			_weight = 5f;
            _laserOffsetTL = new Vec2(30f, 2.5f);
            DeltaWait = 0.07f;
            BurstNum = 2;
        }

        public override void Initialize()
        {
			if (!(Level.current is Editor) && Laser.value)
            {
                laserSight = true;
            }
            base.Initialize();
        }
	}
}
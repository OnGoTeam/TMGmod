using DuckGame;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Shotgun")]
    public class DragoShot : Gun
    {
        public DragoShot (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 16;
            _ammoType = new ATMagnum();
            _ammoType.range = 160f;
            _ammoType.accuracy = 0.7f;
            _ammoType.penetration = 2f;
            _numBulletsPerFire = 8;
            _ammoType.bulletThickness = 0.8f;
            _type = "gun";
            _graphic = new Sprite(GetPath("DragoShot"));
            _center = new Vec2(17f, 7f);
            _collisionOffset = new Vec2(-14f, -7f);
            _collisionSize = new Vec2(29f, 11f);
            _barrelOffsetTL = new Vec2(30f, 2.5f);
            _fireSound = "shotgunFire";
            _fullAuto = true;
            _fireWait = 2.2f;
            _kickForce = 1.2f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0.5f;
            laserSight = true;
            _laserOffsetTL = new Vec2(23f, 3f);
            _holdOffset = new Vec2(0f, 3f);
            _editorName = "DragoShot";
			_weight = 5f;
        }
    }
}

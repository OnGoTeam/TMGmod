using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun")]
    public class DragoShot : Gun
    {
        public DragoShot (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 16;
            _ammoType = new ATMagnum
            {
                range = 120f,
                accuracy = 0.7f,
                penetration = 2f,
                bulletThickness = 0.8f
            };
            _numBulletsPerFire = 8;
            _type = "gun";
            graphic = new Sprite(GetPath("DragoShot"));
            center = new Vec2(17f, 7f);
            collisionOffset = new Vec2(-14f, -7f);
            collisionSize = new Vec2(29f, 11f);
            _barrelOffsetTL = new Vec2(30f, 2.5f);
            _fireSound = "shotgunFire";
            _fullAuto = true;
            _fireWait = 1.6f;
            _kickForce = 5.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.4f;
            laserSight = true;
            _laserOffsetTL = new Vec2(23f, 3f);
            _holdOffset = new Vec2(0f, 3f);
            _editorName = "DragoShot";
			weight = 5f;
        }
    }
}

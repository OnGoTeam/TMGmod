using DuckGame;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun")]
    public class Deadly44 : Gun
    {
		public Deadly44 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 1;
            _ammoType = new ATMagnum
            {
                range = 150f,
                accuracy = 0.2f,
                penetration = 4f,
                bulletThickness = 2f
            };
            _numBulletsPerFire = 44;
            _type = "gun";
            graphic = new Sprite(GetPath("44db"));
            center = new Vec2(16.5f, 5f);
            collisionOffset = new Vec2(-16.5f, -5f);
            collisionSize = new Vec2(33f, 10f);
            _barrelOffsetTL = new Vec2(34f, 2.5f);
            _holdOffset = new Vec2(2f, 1f);
            _fireSound = "shotgun";
            _fullAuto = false;
            _fireWait = 4f;
            _kickForce = 3.4f;
            loseAccuracy = 0.25f;
            maxAccuracyLost = 0.5f;
            _editorName = "DeadlyGauge";
			weight = 4f;
        }
    }
}
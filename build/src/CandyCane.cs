using DuckGame;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    public class CandyCane:Gun
    {
        public CandyCane(float xval, float yval) : base(xval, yval)
        {
            ammo = 1;
            _ammoType = new ATMagnum
            {
                range = 150f,
                accuracy = 0.95f,
                penetration = 1f
            };
            _numBulletsPerFire = 2;
            _type = "gun";
            graphic = new Sprite(GetPath("candycane"));
            center = new Vec2(9f, 3.5f);
            collisionOffset = new Vec2(-9f, -3.5f);
            collisionSize = new Vec2(18f, 7f);
            _barrelOffsetTL = new Vec2(18f, 3.5f);
            _fireSound = "pistolFire";
            _fullAuto = false;
            _manualLoad = true;
            _fireWait = 1.2f;
            _kickForce = 0f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            _holdOffset = new Vec2(-1f, 1f);
            _editorName = "AF-2011";
            weight = 2.5f;
            
        }

        public override void Reload(bool shell = true)
        {
        }
    }
}
using DuckGame;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Custom_Guns
{
    [EditorGroup("TMG|Shotgun|Custom")]
    // ReSharper disable once InconsistentNaming
    public class SIX12C : Gun
    {
		
		public SIX12C (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 6;
            _ammoType = new ATMagnum
            {
                range = 225f,
                accuracy = 0.87f,
                penetration = 1f,
                bulletThickness = 0.5f
            };
            _numBulletsPerFire = 14;
            _type = "gun";
            graphic = new Sprite(GetPath("SIX12laser2"));
            center = new Vec2(19.5f, 5f);
            collisionOffset = new Vec2(-19.5f, -5f);
            collisionSize = new Vec2(29f, 10f);
            _barrelOffsetTL = new Vec2(30f, 4.5f);
            _fireSound = "shotgunFire";
            _fullAuto = false;
            _fireWait = 1.7f;
            _kickForce = 5.5f;
            loseAccuracy = 0.5f;
            maxAccuracyLost = 0.5f;
            laserSight = true;
            _laserOffsetTL = new Vec2(24f, 7f);
            _holdOffset = new Vec2(2f, 0f);
            _editorName = "SIX12 with Laser";
			weight = 4f;
        }
    }
}
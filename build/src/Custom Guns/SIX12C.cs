using DuckGame;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Misc|Custom Guns")]
    // ReSharper disable once InconsistentNaming
    public class SIX12C : Gun
    {
		
		public SIX12C (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 6;
            _ammoType = new ATMagnum {range = 225f, accuracy = 0.87f, penetration = 1f};
            _numBulletsPerFire = 14;
            _ammoType.bulletThickness = 0.5f;
            _type = "gun";
            _graphic = new Sprite(GetPath("SIX12laser2"));
            _center = new Vec2(19.5f, 5f);
            _collisionOffset = new Vec2(-19.5f, -5f);
            _collisionSize = new Vec2(29f, 10f);
            _barrelOffsetTL = new Vec2(30f, 4.5f);
            _fireSound = "shotgunFire";
            _fullAuto = false;
            _fireWait = 1.7f;
            _kickForce = 1.4f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0.5f;
            laserSight = true;
            _laserOffsetTL = new Vec2(24f, 7f);
            _holdOffset = new Vec2(2f, 0f);
            _editorName = "SIX12 with Laser";
			_weight = 4f;
        }
    }
}
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [UsedImplicitly]
    [EditorGroup("TMG|Shotgun|Pump-Action")]
    public class Remington : BasePumpAction
    {
	    public Remington(float xval, float yval) : base(xval, yval)
	    {
		    ammo = 12;
	        _ammoType = new AT9mm
	        {
	            range = 169f,
	            accuracy = 0.47f,
	            penetration = 1f,
	            bulletSpeed = 50f,
	            bulletThickness = 0.6f
	        };
            _numBulletsPerFire = 16;
            _type = "gun";
		    _graphic = new SpriteMap(GetPath("Remington1"), 46, 10);
		    _center = new Vec2(23f, 5f);
		    _collisionOffset = new Vec2(-23f, -5f);
		    _collisionSize = new Vec2(46f, 10f);
		    _barrelOffsetTL = new Vec2(46f, 1f);
            _flare = new SpriteMap(GetPath("FlareOnePixel3"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _holdOffset = new Vec2(2f, 2f);
            _fireSound = GetPath("sounds/shotgun.wav");
            _kickForce = 9f;
            loseAccuracy = 0.4f;
            maxAccuracyLost = 0.9f;
		    _manualLoad = true;
            _fireWait = 4f;
            _editorName = "ATAMAN XP-03";
            LoaderSprite = new SpriteMap(GetPath("Remington1p"), 16, 4)
            {
                    center = new Vec2(8f, 2f)
            };
            LoaderVec2 = new Vec2(12f, -1f);
            Loaddx = 5f;
        }
    }
}
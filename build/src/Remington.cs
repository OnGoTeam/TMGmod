using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [UsedImplicitly]
    [EditorGroup("TMG|Shotgun|Pump-Action")]
    public class Remington : BaseGun, IAmSg
    {
	    public Remington(float xval, float yval) : base(xval, yval)
	    {
		    ammo = 11;
	        _ammoType = new AT9mm
	        {
	            range = 145f,
	            accuracy = 0.47f,
	            penetration = 1f,
	            bulletSpeed = 16f,
	            bulletThickness = 0.6f
	        };
            _numBulletsPerFire = 17;
            _type = "gun";
		    _graphic = new SpriteMap(GetPath("Taligator 6000 SX"), 31, 12);
		    _center = new Vec2(16f, 6f);
		    _collisionOffset = new Vec2(-16f, -6f);
		    _collisionSize = new Vec2(31f, 12f);
		    _barrelOffsetTL = new Vec2(31f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _holdOffset = new Vec2(-1f, 2f);
		    _fireSound = "shotgunFire2";
		    _kickForce = 2.75f;
            _fullAuto = true;
//		    _manualLoad = true;
            _fireWait = 2f;
            _editorName = "Taligator 6000 SX";
//          LoaderSprite = new SpriteMap(GetPath("RemingtonPimp"), 6, 8)
//          {
//               center = new Vec2(3f, 4f)
//          };
//          LoaderVec2 = new Vec2(8f, 0f);
//          EpsilonA = 50;
//          EpsilonB = 100;
//          Loaddx = 2f;
        }
    }
}
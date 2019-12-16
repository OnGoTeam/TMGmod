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
		    ammo = 3;
	        _ammoType = new AT9mm
	        {
	            range = 115f,
	            accuracy = 0.57f,
	            penetration = 1f,
	            bulletSpeed = 25f,
	            bulletThickness = 0.6f
	        };
            _numBulletsPerFire = 5;
            _type = "gun";
		    _graphic = new Sprite(GetPath("Remington"));
		    _center = new Vec2(12f, 4f);
		    _collisionOffset = new Vec2(-12f, -4f);
		    _collisionSize = new Vec2(24f, 7f);
		    _barrelOffsetTL = new Vec2(24f, 1f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _holdOffset = new Vec2(-1f, 2f);
		    _fireSound = "shotgunFire2";
		    _kickForce = 2.75f;
		    _manualLoad = true;
            _fireWait = 5f;
            _editorName = "Remington";
            LoaderSprite = new SpriteMap(GetPath("RemingtonPimp"), 6, 8)
            {
                 center = new Vec2(3f, 4f)
            };
            LoaderVec2 = new Vec2(8f, 0f);
            EpsilonA = 50;
            EpsilonB = 100;
            Loaddx = 2f;
        }
    }
}
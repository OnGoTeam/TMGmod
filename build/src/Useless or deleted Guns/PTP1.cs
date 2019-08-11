using DuckGame;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace TMGMod.src
{
    
    [BaggedProperty("isInDemo", true)]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class PTP1 : Gun
	{
		public PTP1(float xval, float yval)
            : base(xval, yval)
		{
            ammo = 30;
            _ammoType = new PTPA();
            _type = "gun";
            _graphic = new Sprite(GetPath("PTP1"));
            _center = new Vec2(8f, 4f);
            _collisionOffset = new Vec2(-8f, -2f);
            _collisionSize = new Vec2(16f, 8f);
            _barrelOffsetTL = new Vec2(17f, 2f);
            _fireSound = "smg";
            _fullAuto = true;
            _fireWait = 2f;
            _kickForce = 1f;
            _holdOffset = new Vec2(-1f, 0f);
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.2f;
		}

		
	}
}

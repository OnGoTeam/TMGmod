using DuckGame;
using TMGmod.Core;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod.Useless_or_deleted_Guns
{
    
    [BaggedProperty("isInDemo", true)]
    // ReSharper disable once InconsistentNaming
	public class PTP1 : Gun
	{

		public PTP1(float xval, float yval)
            : base(xval, yval)
		{
            ammo = 30;
            _ammoType = new PTPA();
            _type = "gun";
            graphic = new Sprite(GetPath("PTP1"));
            center = new Vec2(8f, 4f);
            collisionOffset = new Vec2(-8f, -2f);
            collisionSize = new Vec2(16f, 8f);
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

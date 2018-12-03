using DuckGame;
using TMGmod.Core;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun")]
    // ReSharper disable once InconsistentNaming
    public class SIX12S : Gun
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public readonly EditorProperty<bool> Laser = new EditorProperty<bool>(false, null, 0f, 1f, 1f);
		
		public SIX12S (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 6;
            _ammoType = new AT9mmS
            {
                range = 225f,
                accuracy = 0.9f,
                penetration = 1f
            };
            _numBulletsPerFire = 14;
            _type = "gun";
            graphic = new Sprite(GetPath("SIX12S"));
            center = new Vec2(19.5f, 5f);
            collisionOffset = new Vec2(-19.5f, -5f);
            collisionSize = new Vec2(29f, 10f);
            _barrelOffsetTL = new Vec2(30f, 4.5f);
            _fireSound = "shotgunFire";
            _fullAuto = false;
            _fireWait = 1.7f;
            _kickForce = 2.3f;
            loseAccuracy = 0.3f;
            maxAccuracyLost = 0.5f;
            laserSight = false;
            _laserOffsetTL = new Vec2(24f, 7f);
            _holdOffset = new Vec2(2f, 0f);
            _editorName = "SIX12 Silenced";
			weight = 4f;
        }
        public override void Initialize()
        {
			if (!(Level.current is Editor))
            {
                if (Laser.value)
                {
                 laserSight = true;
				 graphic = new Sprite(GetPath("SIX12Slaser2"));
                }
            }
            base.Initialize();
        }
	}
}
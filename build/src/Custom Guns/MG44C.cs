using DuckGame;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Misc|Custom Guns")]
    // ReSharper disable once InconsistentNaming
    public class MG44C : Gun
    {
		
		
		public MG44C (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new ATMagnum {range = 750f, accuracy = 0.75f, penetration = 1.5f};
            _type = "gun";
            _graphic = new Sprite(GetPath("mg44reqnato2"));
            _center = new Vec2(19.5f, 6f);
            _collisionOffset = new Vec2(-19.5f, -6f);
            _collisionSize = new Vec2(39f, 12f);
            _barrelOffsetTL = new Vec2(40f, 4f);
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.9f;
            _kickForce = 0.3f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            _holdOffset = new Vec2(4f, 0f);
            _editorName = "Magnium with NATO Mag";
			_weight = 6f;
        }
    }
}
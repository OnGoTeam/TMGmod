using DuckGame;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Misc|Custom Guns")]
    [PublicAPI]
    public class Deadly44C : Gun
    {
		
        public Deadly44C (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 2;
            _ammoType = new ATMagnum {range = 150f, accuracy = 0.2f, penetration = 4f};
            _numBulletsPerFire = 44;
            _ammoType.bulletThickness = 2f;
            _type = "gun";
            _graphic = new Sprite(GetPath("44dbonemore"));
            _center = new Vec2(16.5f, 5f);
            _collisionOffset = new Vec2(-16.5f, -5f);
            _collisionSize = new Vec2(33f, 10f);
            _barrelOffsetTL = new Vec2(34f, 2.5f);
            _holdOffset = new Vec2(2f, 1f);
            _fireSound = "shotgun";
            _fullAuto = false;
            _fireWait = 4f;
            _kickForce = 1.8f;
            loseAccuracy = 0.25f;
            maxAccuracyLost = 0.5f;
            _editorName = "Deadly One More Gauge";
			_weight = 4.25f;
        }
		public override void Update()
        {
			if (ammo < 2) _graphic = new Sprite(GetPath("44dbnonemore"));
            base.Update();
	    } 
    }
}
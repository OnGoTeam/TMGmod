using DuckGame;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Shotgun")]
    public class Deadly44 : Gun
    {
		[UsedImplicitly]
		public EditorProperty<bool> Onemoreammo = new EditorProperty<bool>(false, null, 0f, 1f, 1f, "Second Bullet");
		
        public Deadly44 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 1;
            _ammoType = new ATMagnum {range = 150f, accuracy = 0.2f, penetration = 4f};
            _numBulletsPerFire = 44;
            _ammoType.bulletThickness = 2f;
            _type = "gun";
            _graphic = new Sprite(GetPath("44db"));
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
            _editorName = "DeadlyGauge";
			_weight = 4f;
        }
    }
}
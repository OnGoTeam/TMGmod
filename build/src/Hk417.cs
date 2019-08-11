using DuckGame;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Rifle")]
    // ReSharper disable once InconsistentNaming
    public class hk417 : Gun
    {
        public hk417 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 20;
            _ammoType = new ATMagnum {range = 700f, accuracy = 0.9f, penetration = 1f};
            _type = "gun";
            _graphic = new Sprite(GetPath("Hk417"));
            _center = new Vec2(15f, 5f);
            _collisionOffset = new Vec2(-14.5f, -5f);
            _collisionSize = new Vec2(30f, 10f);
            _barrelOffsetTL = new Vec2(31f, 3f);
            _fireSound = GetPath("sounds/HeavyRifle.wav");
            _fullAuto = false;
            _fireWait = 0.8f;
            _kickForce = 0.7f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _editorName = "Hk-417C";
			_weight = 3.5f;
        }
    }
}
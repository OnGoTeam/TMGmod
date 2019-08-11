using DuckGame;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Rifle")]
    // ReSharper disable once InconsistentNaming
    public class mk20 : Gun
    {
        public mk20 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 20;
            _ammoType = new ATMagnum {range = 800f, accuracy = 0.87f, penetration = 1f};
            _type = "gun";
            _graphic = new Sprite(GetPath("SCARMk1"));
            _center = new Vec2(16f, 7f);
            _collisionOffset = new Vec2(-16.5f, -7f);
            _collisionSize = new Vec2(33f, 14f);
            _barrelOffsetTL = new Vec2(33f, 5.5f);
            _holdOffset = new Vec2(1f, -1f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = false;
            _fireWait = 0.95f;
            _kickForce = 0.7f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _editorName = "SCAR Mk20";
			_weight = 6.5f;
        }
    }
}
using DuckGame;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Misc|Custom Guns")]
    // ReSharper disable once InconsistentNaming
    public class SVUC : Gun
    {
        public SVUC (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 5;
            _ammoType = new ATMagnum {range = 700f, accuracy = 0.925f, penetration = 1.5f};
            _type = "gun";
            _graphic = new Sprite(GetPath("SVUlmag"));
            _center = new Vec2(20f, 8f);
            _collisionOffset = new Vec2(-14.5f, -8f);
            _collisionSize = new Vec2(31f, 11f);
            _barrelOffsetTL = new Vec2(31f, 5f);
            _fireSound = GetPath("sounds/HeavyRifle.wav");
            _fullAuto = true;
            _fireWait = 0.75f;
            _kickForce = 1.5f;
            loseAccuracy = 0.05f;
            maxAccuracyLost = 0.25f;
            _holdOffset = new Vec2(1f, 2f);
            _editorName = "SVU with Low Mag";
			_weight = 5f;
        }
    }
}
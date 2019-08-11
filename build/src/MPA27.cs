using DuckGame;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|SMG")]
    // ReSharper disable once InconsistentNaming
    public class MPA27 : Gun
    {
        public MPA27(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new AT9mm {range = 200f, accuracy = 0.75f, penetration = 1f};
            _type = "gun";
            _graphic = new Sprite(GetPath("MPA27"));
            _center = new Vec2(6f, 7f);
            _collisionOffset = new Vec2(-8f, -7f);
            _collisionSize = new Vec2(16f, 14f);
            _barrelOffsetTL = new Vec2(16f, 3f);
            _fireSound = GetPath("sounds/2.wav");
            _fullAuto = true;
            _fireWait = 0.36f;
            _kickForce = 0f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.2f;
            _holdOffset = new Vec2(-1f, 3f);
            _editorName = "Vista";
			_weight = 2f;
        }


    }
}

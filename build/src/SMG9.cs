using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|SMG")]
    // ReSharper disable once InconsistentNaming
    public class SMG9 : BaseSmg
    {
        public SMG9(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 48;
            _ammoType = new AT9mm
            {
                range = 95f,
                accuracy = 0.6f,
                penetration = 0.4f
            };
            _type = "gun";
            _graphic = new Sprite(GetPath("SMG9"));
            _center = new Vec2(7f, 7f);
            _collisionOffset = new Vec2(-8f, -7f);
            _collisionSize = new Vec2(16f, 15f);
            _barrelOffsetTL = new Vec2(16f, 4f);
            _fireSound = GetPath("sounds/1.wav");
            _fullAuto = true;
            _fireWait = 0.35f;
            _kickForce = 0f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.33f;
            _holdOffset = new Vec2(-3f, 2f);
            _editorName = "SMG-9";
			_weight = 2.5f;
        }
    }
}

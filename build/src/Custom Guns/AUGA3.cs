using DuckGame;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Misc|Custom Guns")]
    [PublicAPI]
    // ReSharper disable once InconsistentNaming
    public class augC : Gun
    {
        public augC (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new ATMagnum {range = 680f, accuracy = 0.96f, penetration = 1f};
            _type = "gun";
            _graphic = new Sprite(GetPath("auga3"));
            _center = new Vec2(15f, 6f);
            _collisionOffset = new Vec2(-15f, -6f);
            _collisionSize = new Vec2(30f, 12f);
            _barrelOffsetTL = new Vec2(30f, 5f);
            _holdOffset = new Vec2(-3f, 0f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 0.8f;
            _kickForce = 0.7f;
            loseAccuracy = 0.025f;
            maxAccuracyLost = 0.1f;
            _editorName = "AUG A3";
			_weight = 5.5f;
        }
	}
}
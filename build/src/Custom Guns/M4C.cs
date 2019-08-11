using DuckGame;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Misc|Custom Guns")]
    [PublicAPI]
    public class M4A1C : Gun
    {
		
		public M4A1C (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new AT9mm {range = 300f, accuracy = 0.91f, penetration = 1.5f};
            _type = "gun";
            _graphic = new Sprite(GetPath("m4a1Custm"));
            _center = new Vec2(15f, 6f);
            _collisionOffset = new Vec2(-15f, -6f);
            _collisionSize = new Vec2(30f, 12f);
            _barrelOffsetTL = new Vec2(31f, 5f);
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.65f;
            _kickForce = 0f;
            loseAccuracy = 0.01f;
            maxAccuracyLost = 0.09f;
            _holdOffset = new Vec2(2f, 0f);
            _editorName = "M4A1 Custom";
			_weight = 4f;
        }
    }
}
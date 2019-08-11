using DuckGame;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{

    [BaggedProperty("isInDemo", true), EditorGroup("TMG|SMG")]
    // ReSharper disable once InconsistentNaming
    public class MP40 : Gun
    {

        public MP40(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 32;
            _ammoType = new AT9mm {range = 190f, accuracy = 0.9f};
            _type = "gun";
            _graphic = new Sprite(GetPath("MP40WW2"));
            _center = new Vec2(12f, 5f);
            _collisionOffset = new Vec2(-11f, -4f);
            _collisionSize = new Vec2(22f, 14f);
            _barrelOffsetTL = new Vec2(25f, 2f);
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.5f;
            _kickForce = 0.5f;
            _holdOffset = new Vec2(3f, 1f);
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.5f;
            _editorName = "MP40";
			_weight = 3f;
        }


    }
}

using DuckGame;
using JetBrains.Annotations;

namespace TMGmod
{
    [EditorGroup("TMG|SMG")]
    [PublicAPI]
    public class P90 : Gun
    {
        public readonly EditorProperty<bool> Elongated = new EditorProperty<bool>(false, null, 0f, 1f, 1f);
        
        public P90(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 50;
            _ammoType = new AT9mm
            {
                range = Rando.Float(15f, 60f),
                accuracy = 0.6f,
                penetration = 1f
            };
            _type = "gun";
            _graphic = new Sprite(GetPath("P90"));
            _center = new Vec2(9.5f, 3f);
            _collisionOffset = new Vec2(-8.5f, -3f);
            _collisionSize = new Vec2(19f, 6f);
            _barrelOffsetTL = new Vec2(19f, 3f);
            _fireSound = GetPath("sounds/2.wav");
            _fullAuto = true;
            _fireWait = 0.2f;
            _kickForce = 0.4f;
            loseAccuracy = 0.09f;
            maxAccuracyLost = 0.55f;
            _holdOffset = new Vec2(-3f, 0f);
            handOffset = new Vec2(2f, 0f);
            _editorName = "FN P90";
			_weight = 1.5f;
        }
        public override void Initialize()
        {
			if (!(Level.current is Editor))
            {
                if (Elongated.value)
                {
				 graphic = new Sprite(GetPath("p90long2"));
                }
            }
            base.Initialize();
        }
        public override void OnHoldAction()
        {
            if (Elongated.value)
            {
                _ammoType.accuracy = 0.7f;
                _ammoType.range = Rando.Float(-10f, 60f) + 25f;
            }
            else
            {
                _ammoType.accuracy = 0.6f;
                _ammoType.range = Rando.Float(15f, 60f);
            }
            base.OnHoldAction();
        }
    }
}
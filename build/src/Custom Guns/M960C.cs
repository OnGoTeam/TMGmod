using DuckGame;
using JetBrains.Annotations;

namespace TMGmod.Custom_Guns
{

    [BaggedProperty("isInDemo", true), EditorGroup("TMG|SMG")]
    [PublicAPI]
    public class M960Low : Gun
    {
        public readonly EditorProperty<bool> Limited = new EditorProperty<bool>(false, null, 0f, 1f, 1f);
		
        public M960Low(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new AT9mm
            {
                range = Rando.Float(0f, 70f),
                accuracy = 0.4f
            };
            _type = "gun";
            _graphic = new Sprite(GetPath("M960lowmag"));
            _center = new Vec2(13.5f, 3.5f);
            _collisionOffset = new Vec2(-11.5f, -3.5f);
            _collisionSize = new Vec2(23f, 7f);
            _barrelOffsetTL = new Vec2(19f, 2.5f);
            _fireSound = "smg";
            _fullAuto = true;
            _fireWait = 0.15f;
            _kickForce = 0.3f;
            _holdOffset = new Vec2(2.5f, 1.5f);
            loseAccuracy = 0.01f;
            maxAccuracyLost = 0.05f;
            _editorName = "Calico M960 Low Mag";
			_weight = 1f;
            handAngle = 0f;
        }
        public override void Initialize()
        {
			if (!(Level.current is Editor))
            {
                if (Limited.value)
                {
                 _fireWait = 0.6f;
                 _ammoType.accuracy = 0.5f;
                }
            }
            base.Initialize();
        }
        public override void OnHoldAction()
        {
            _ammoType.range = Rando.Float(0f, Rando.Float(0f, 45f)) + 5f;
            handAngle = Rando.Float(-0.1f, 0.1f);
            base.OnHoldAction();
        }
        public override void OnReleaseAction()
        {
            handAngle = 0f;
            base.OnReleaseAction();
        }
    }
}
using DuckGame;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{

    [BaggedProperty("isInDemo", true), EditorGroup("TMG|SMG")]
    public class M960 : Gun
    {
        private readonly EditorProperty<bool> limited = new EditorProperty<bool>(false, null, 0f, 1f, 1f);
		
        public M960(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 50;
            _ammoType = new AT9mm
            {
                range = Rando.Float(0f, 70f),
                accuracy = 0.4f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("M960"));
            center = new Vec2(13.5f, 3.5f);
            collisionOffset = new Vec2(-11.5f, -3.5f);
            collisionSize = new Vec2(23f, 7f);
            _barrelOffsetTL = new Vec2(19f, 2.5f);
            _fireSound = "smg";
            _fullAuto = true;
            _fireWait = 0.15f;
            _kickForce = 0.3f;
            _holdOffset = new Vec2(2.5f, 1.5f);
            loseAccuracy = 0.01f;
            maxAccuracyLost = 0.05f;
            _editorName = "Calico M960";
			weight = 1f;
            handAngle = 0f;
        }
        public override void Initialize()
        {
			if (!(Level.current is Editor))
            {
                if (limited.value)
                {
                 _fireWait = 0.5f;
                 _ammoType.accuracy = 0.95f;
                }
            }
            base.Initialize();
        }
        public override void OnHoldAction()
        {
            _ammoType.range = Rando.Float(0f, Rando.Float(0f, 60f));
            handAngle = Rando.Float(-0.05f, 0.05f);
            base.OnHoldAction();
        }
    }
}
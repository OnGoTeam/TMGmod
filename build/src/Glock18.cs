using DuckGame;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|AutoPistol")]
    public class Glock18 : Gun
    {
		
        public Glock18(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 17;
            _ammoType = new AT9mm
            {
                range = 100f,
                accuracy = 1f,
                penetration = 1f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("Glock17"));
            center = new Vec2(8f, 3f);
            collisionOffset = new Vec2(-7.5f, -3.5f);
            collisionSize = new Vec2(16f, 11f);
            _barrelOffsetTL = new Vec2(16f, 1f);
            _fireSound = GetPath("sounds/2.wav");
            _fullAuto = true;
            _fireWait = 0.4f;
            _kickForce = 0.1f;
            loseAccuracy = 0.05f;
            maxAccuracyLost = 0.2f;
            _editorName = "Glock 18";
			weight = 1.7f;
        }
        public override void OnHoldAction()
        {
            handAngle = Rando.Float(-0.08f, 0.08f);
            base.OnHoldAction();
        }
    }
}
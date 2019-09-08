using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Fully-Automatic")]
    public class Glock18 : BaseGun, IAmHg
    {
		
        public Glock18(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 17;
            _ammoType = new AT9mm
            {
                range = 100f,
                accuracy = 0.65f,
                penetration = 0.5f
            };
            _type = "gun";
            _graphic = new Sprite(GetPath("Glock17"));
            _center = new Vec2(8f, 3f);
            _collisionOffset = new Vec2(-7.5f, -3.5f);
            _collisionSize = new Vec2(16f, 11f);
            _barrelOffsetTL = new Vec2(16f, 1f);
            _fireSound = GetPath("sounds/2.wav");
            _fullAuto = true;
            _fireWait = 0.4f;
            _kickForce = 1.4f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.4f;
            ShellOffset = new Vec2(-1f, -2f);
            _editorName = "Glock 18";
			_weight = 1.7f;
        }
        public override void OnHoldAction()
        {
            handAngle = Rando.Float(-0.08f, 0.08f);
            base.OnHoldAction();
        }
    }
}
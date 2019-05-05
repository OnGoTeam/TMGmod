using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.Custom_Guns
{
    [EditorGroup("TMG|Handgun|Fully-Automatic")]
    public class Glock18C : BaseGun, IAmHg
    {
		
        public Glock18C(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 33;
            _ammoType = new AT9mm
            {
                range = 100f,
                accuracy = 1f,
                penetration = 1f
            };
            _type = "gun";
            _graphic = new Sprite(GetPath("Glock17lmg"));
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
            _editorName = "Glock 18 33 ammo";
			_weight = 2.1f;
        }
        public override void OnHoldAction()
        {
            handAngle = Rando.Float(-0.08f, 0.08f);
            base.OnHoldAction();
        }
    }
}
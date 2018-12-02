using DuckGame;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|LMG")]
    // ReSharper disable once InconsistentNaming
    public class MG44 : Gun
    {
        private float _raisestat;

		public MG44 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 80;
            _ammoType = new ATMagnum
            {
                range = 750f,
                accuracy = 0.75f,
                penetration = 1.5f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("mg44req"));
            center = new Vec2(19.5f, 6f);
            collisionOffset = new Vec2(-19.5f, -6f);
            collisionSize = new Vec2(39f, 12f);
            _barrelOffsetTL = new Vec2(40f, 4f);
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.9f;
            _kickForce = 0.6f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            _holdOffset = new Vec2(4f, 0f);
            _editorName = "Magnium";
			weight = 7.5f;
        }
		public override void Update()
		{
		    base.Update();
            switch (ammo)
            {
                case 1:
                    graphic = new Sprite(GetPath("mg44req1"));
                    break;
                case 0:
                    graphic = new Sprite(GetPath("mg44req2"));
                    break;
            }

		    if (_raisestat > .6f) _raisestat = .6f;
		    if (_raisestat > 0f)
		    {
		        var δα = -0.123607f - 0.1f / (_raisestat - 0.809017f);

		        if (offDir < 0)
		        {
		            handAngle = δα;
		        }
		        else
		        {
		            handAngle = -δα;
		        }
		    }
		    _raisestat -= .015f;
		    if (duck == null)
		    {
		        _raisestat = 0f;
		        handAngle = 0f;
		    }
		    else
		    {
		        if (duck.crouch || duck.sliding) _raisestat -= .015f;
		        _raisestat += 0.05f * duck.vSpeed;
		        if (!(_raisestat < 0f)) return;
		        _raisestat = 0f;
		        handAngle = 0f;
		    }
		}

        public override void Fire()
        {
            base.Fire();_raisestat += Rando.Float(0.03f, 0.08f);
        }
    }
}
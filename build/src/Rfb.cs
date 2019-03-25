using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    public class Rfb : BaseAr
    {
		
        public Rfb (float xval, float yval)
          : base(xval, yval)
		{
            ammo = 20;
            _ammoType = new ATMagnum
            {
                range = 580f,
                accuracy = 0.92f,
                penetration = 1f
            };
            _type = "gun";
            _graphic = new Sprite(GetPath("RFBSemi"));
            _center = new Vec2(16.5f, 5.5f);
            _collisionOffset = new Vec2(-16.5f, -5.5f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 4f);
            _holdOffset = new Vec2(3f, 1f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = false;
            _fireWait = 0.3f;
            _kickForce = 0.7f;
            loseAccuracy = 0.025f;
            maxAccuracyLost = 0.2f;
            _editorName = "RFB";
			_weight = 6f;
		    Kforce2Ar = 0.7f;
        }
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (_fullAuto)
                {
                    _fullAuto = false;
                    graphic = new Sprite(GetPath("RFBSemi"));
                    _fireWait = 0.3f;
                    maxAccuracyLost = 0.2f;
                }
                else
                {
                    _fullAuto = true;
                    graphic = new Sprite(GetPath("RFBauto"));
                    _fireWait = 0.79f;
                    maxAccuracyLost = 0.25f;
                }
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }
            base.Update();
		}			
	}
}
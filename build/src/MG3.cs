using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|LMG")]
    public class MG3 : Gun
    {
		bool bipodes = false;
		
		public MG3 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 95;
            _ammoType = new ATMagnum
            {
                range = 600f,
                accuracy = 0.8f,
                penetration = 1.5f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("mg3"));
            center = new Vec2(19.5f, 6f);
            collisionOffset = new Vec2(-19.5f, -6f);
            collisionSize = new Vec2(39f, 9f);
            _barrelOffsetTL = new Vec2(40f, 4f);
            _fireSound = GetPath("sounds/RifleOrMG.wav");
            _fullAuto = true;
            _fireWait = 0.5f;
            _kickForce = 0.95f;
            loseAccuracy = 0.025f;
            maxAccuracyLost = 0.1f;
            _holdOffset = new Vec2(4f, 2f);
            _editorName = "MG3";
			weight = 7f;
        }
          public override void Update()
        {
            if (_owner != null && _owner.height < 17f)
            {
                _kickForce = 0f;
				loseAccuracy = 0f;
                maxAccuracyLost = 0f;
				graphic = new Sprite(GetPath("mg3bipods"));
				bipodes = true;
            }
            else
            {
                _kickForce = 0.95f;
                loseAccuracy = 0.025f;
                maxAccuracyLost = 0.1f;
				graphic = new Sprite(GetPath("mg3"));
				bipodes = false;
            }
            base.Update();
			if ((ammo == 0) && (bipodes == false)) graphic = new Sprite(GetPath("mg31"), 0f, 0f);
			if ((ammo == 0) && (bipodes == true)) graphic = new Sprite(GetPath("mg3bipods1"), 0f, 0f);
        }
	}
}	
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Machinegun")]
    public class cz805 : Gun
    {
		float silencer = 0f;
		
        public cz805 (float xval, float yval)
          : base(xval, yval)
		{
            ammo = 30;
		    _ammoType = new AT9mm
		    {
		        range = 800f,
		        accuracy = 0.87f,
		        penetration = 1f
		    };
		    _type = "gun";
            graphic = new Sprite(GetPath("CZ805Bren"));
            center = new Vec2(20.5f, 5.5f);
            collisionOffset = new Vec2(-20.5f, -5.5f);
            collisionSize = new Vec2(41f, 11f);
            _barrelOffsetTL = new Vec2(39f, 3.5f);
            _holdOffset = new Vec2(5f, 1f);
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.9f;
            _kickForce = 0.7f;
            loseAccuracy = 0.025f;
            maxAccuracyLost = 0.2f;
            _editorName = "CZ-805 BREN";
			weight = 5f;
        }
        public override void Update()
        {
            if (owner != null)
            {
                if (isServerForObject)
                {
                    if (duck.inputProfile.Pressed("QUACK", false))
                    {
					    if ((silencer > 0f))
					    {
				            graphic = new Sprite(GetPath("CZ805Bren"));
                            _fireSound = "deepMachineGun2";
                            _ammoType = new AT9mm
                            {
                                range = 800f,
                                accuracy = 0.87f
                            };
                            loseAccuracy = 0.025f;
                            maxAccuracyLost = 0.2f;
                            _barrelOffsetTL = new Vec2(39f, 4f);
			                silencer = 0f;
					    }
                        else
					    {
				            graphic = new Sprite(GetPath("CZ805BrenS"));
                            _fireSound = GetPath("sounds/Silenced2.wav");
                            _ammoType = new AT9mmS
                            {
                                range = 870f,
                                accuracy = 0.95f
                            };
                            loseAccuracy = 0.02f;
                            maxAccuracyLost = 0.18f;
                            _barrelOffsetTL = new Vec2(42.5f, 4f);
			                silencer = 1f;
					    }
					}
				}
			}
		    base.Update();
			if ((ammo < 28) && (silencer == 0f)) graphic = new Sprite(GetPath("CZ805Bren1"), 0f, 0f);
			if ((ammo < 20) && (silencer == 0f)) graphic = new Sprite(GetPath("CZ805Bren2"), 0f, 0f);
			if ((ammo < 12) && (silencer == 0f)) graphic = new Sprite(GetPath("CZ805Bren3"), 0f, 0f);
			if ((ammo < 5) && (silencer == 0f)) graphic = new Sprite(GetPath("CZ805Bren4"), 0f, 0f);
		//silenced version
			if ((ammo < 28) && (silencer == 1f)) graphic = new Sprite(GetPath("CZ805BrenS1"), 0f, 0f);
			if ((ammo < 20) && (silencer == 1f)) graphic = new Sprite(GetPath("CZ805BrenS2"), 0f, 0f);
			if ((ammo < 12) && (silencer == 1f)) graphic = new Sprite(GetPath("CZ805BrenS3"), 0f, 0f);
			if ((ammo < 5) && (silencer == 1f)) graphic = new Sprite(GetPath("CZ805BrenS4"), 0f, 0f);
		}			
	}
}
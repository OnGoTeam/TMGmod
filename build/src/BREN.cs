using DuckGame;
using TMGmod.Core;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    // ReSharper disable once InconsistentNaming
    public class CZ805 : Gun
    {
        private bool _silencer;
		
        public CZ805 (float xval, float yval)
          : base(xval, yval)
		{
            ammo = 30;
		    _ammoType = new AT9mm
		    {
		        range = 500f,
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
            maxAccuracyLost = 0.32f;
            _editorName = "CZ-805 BREN";
			weight = 5f;
        }
        public override void Update()
        {
            if (owner != null)
            {
                if (duck.inputProfile.Pressed("QUACK"))
                {
					if (_silencer)
					{
				        graphic = new Sprite(GetPath("CZ805Bren"));
                        _fireSound = "deepMachineGun2";
                        _ammoType = new AT9mm
                        {
                            range = 500f,
                            accuracy = 0.87f
                        };
                        loseAccuracy = 0.025f;
                        maxAccuracyLost = 0.32f;
                        _barrelOffsetTL = new Vec2(39f, 4f);
			            _silencer = !_silencer;
					}
                    else
					{
				        graphic = new Sprite(GetPath("CZ805BrenS"));
                        _fireSound = GetPath("sounds/Silenced2.wav");
                        _ammoType = new AT9mmS
                        {
                            range = 570f,
                            accuracy = 0.95f
                        };
                        loseAccuracy = 0.02f;
                        maxAccuracyLost = 0.3f;
                        _barrelOffsetTL = new Vec2(42.5f, 4f);
			            _silencer = !_silencer;
					}
				}
			}
		    base.Update();
			if (ammo < 28 && !_silencer) graphic = new Sprite(GetPath("CZ805Bren1"));
			if (ammo < 20 && !_silencer) graphic = new Sprite(GetPath("CZ805Bren2"));
			if (ammo < 12 && !_silencer) graphic = new Sprite(GetPath("CZ805Bren3"));
			if (ammo < 5 && !_silencer) graphic = new Sprite(GetPath("CZ805Bren4"));
		//silenced version
			if (ammo < 28 && _silencer) graphic = new Sprite(GetPath("CZ805BrenS1"));
			if (ammo < 20 && _silencer) graphic = new Sprite(GetPath("CZ805BrenS2"));
			if (ammo < 12 && _silencer) graphic = new Sprite(GetPath("CZ805BrenS3"));
			if (ammo < 5 && _silencer) graphic = new Sprite(GetPath("CZ805BrenS4"));
		}			
	}
}
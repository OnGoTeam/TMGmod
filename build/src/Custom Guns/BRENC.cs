using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod.Custom_Guns
{
    [EditorGroup("TMG|Machinegun|Custom")]
    public class Bren : Gun, IAmDmr
    {
        public bool Silencer;
        public StateBinding SilencerBinding = new StateBinding(nameof(Silencer));

        public Bren (float xval, float yval)
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
            _graphic = new Sprite(GetPath("CZ805BrenZ"));
            _center = new Vec2(20.5f, 5.5f);
            _collisionOffset = new Vec2(-20.5f, -5.5f);
            _collisionSize = new Vec2(41f, 11f);
            _barrelOffsetTL = new Vec2(39f, 3.5f);
            _holdOffset = new Vec2(5f, 1f);
            _fireSound = "deepMachineGun2";
            _fullAuto = false;
            _fireWait = 1.45f;
            _kickForce = 0.7f;
            loseAccuracy = 0.025f;
            maxAccuracyLost = 0.2f;
            _editorName = "CZ-805 Civilian";
			_weight = 5f;
        }
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (Silencer)
                {
                    graphic = new Sprite(GetPath("CZ805BrenZ"));
                    _fireSound = "deepMachineGun2";
                    _ammoType = new AT9mm
                    {
                        range = 500f,
                        accuracy = 0.87f
                    };
                    loseAccuracy = 0.025f;
                    maxAccuracyLost = 0.2f;
                    _barrelOffsetTL = new Vec2(39f, 4f);
                    Silencer = false;
                }
                else
                {
                    graphic = new Sprite(GetPath("CZ805BrenZS"));
                    _fireSound = GetPath("sounds/Silenced2.wav");
                    _ammoType = new AT9mmS
                    {
                        range = 550f,
                        accuracy = 0.95f
                    };
                    loseAccuracy = 0.02f;
                    maxAccuracyLost = 0.18f;
                    _barrelOffsetTL = new Vec2(42.5f, 4f);
                    Silencer = true;
                }
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }
            base.Update();
		}			
	}
}
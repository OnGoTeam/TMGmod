using DuckGame;
using TMGmod.Core;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    // ReSharper disable once InconsistentNaming
    public class CZ805 : Gun, IHaveSkin
    {

        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 10;
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
            _sprite = new SpriteMap(GetPath("CZ805Brenpattern"), 41, 11);
            graphic = _sprite;
            _sprite.frame = 0;
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
                        _sprite.frame -= 50;
                        _fireSound = "deepMachineGun2";
                        _ammoType = new AT9mm
                        {
                            range = 400f,
                            accuracy = 0.87f
                        };
                        loseAccuracy = 0.025f;
                        maxAccuracyLost = 0.32f;
                        _barrelOffsetTL = new Vec2(39f, 4f);
			            _silencer = !_silencer;
                        _flare = new SpriteMap("smallFlare", 11, 10);
                    }
                    else
					{
                        _sprite.frame += 50;
                        _fireSound = GetPath("sounds/Silenced2.wav");
                        _ammoType = new AT9mmS
                        {
                            range = 470f,
                            accuracy = 0.95f
                        };
                        loseAccuracy = 0.02f;
                        maxAccuracyLost = 0.3f;
                        _barrelOffsetTL = new Vec2(42.5f, 4f);
			            _silencer = !_silencer;
                        _flare = new SpriteMap(GetPath("takezis"), 4, 4);
                    }
				}
            }
            if (ammo > 20 && ammo <= 26 && !((_sprite.frame >= 10 && _sprite.frame <= 19) || (_sprite.frame >= 60 && _sprite.frame <= 69))) _sprite.frame += 10;
            if (ammo > 12 && ammo <= 20 && !((_sprite.frame >= 20 && _sprite.frame <= 29) || (_sprite.frame >= 70 && _sprite.frame <= 79))) _sprite.frame += 10;
            if (ammo > 5 && ammo <= 12 && !((_sprite.frame >= 30 && _sprite.frame <= 39) || (_sprite.frame >= 80 && _sprite.frame <= 89))) _sprite.frame += 10;
            if (ammo > 0 && ammo <= 5 && !((_sprite.frame >= 40 && _sprite.frame <= 49) || (_sprite.frame >= 90 && _sprite.frame <= 99))) _sprite.frame += 10;
            base.Update();
        }
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}
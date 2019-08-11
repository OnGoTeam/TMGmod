using DuckGame;

// ReSharper disable once CheckNamespace
namespace TMGmod.src
{
    [EditorGroup("TMG|Pistol")]
    // ReSharper disable once InconsistentNaming
    public class CZ75 : Gun
    {
		private readonly SpriteMap _sprite;
        private int _fdelay;
        public CZ75(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 24;
            _ammoType = new AT9mm {range = 150f, accuracy = 0.75f, penetration = 0f};
            _type = "gun";
            _sprite = new SpriteMap(GetPath("CZ75a"), 12, 8);
            _graphic = _sprite;
            _center = new Vec2(8f, 3f);
            _collisionOffset = new Vec2(-7.5f, -3.5f);
            _collisionSize = new Vec2(15f, 9f);
            _barrelOffsetTL = new Vec2(16f, 1f);
            _fireSound = GetPath("sounds/1.wav");
            _fullAuto = false;
            _fireWait = 0.75f;
            _kickForce = 0f;
            loseAccuracy = 0.3f;
            maxAccuracyLost = 0.5f;
            _editorName = "CZ-75";
			_weight = 1f;
            _sprite.AddAnimation("haventmagaz", 0.3f, false, 1);
            _sprite.AddAnimation("havemagaz", 0.3f, false, 0);
            _sprite.SetAnimation("havemagaz");
        }

        public override void OnPressAction()
        {
            if ((ammo > 0 && _sprite.currentAnimation == "haventmagaz" || ammo > 12 && _sprite.currentAnimation == "havemagaz") && _fdelay == 0)
            {
                base.Fire();
            }
            else if (ammo == 0)
            {
                DoAmmoClick();
            }
            else
            {
                if (ammo == 12 && _sprite.currentAnimation == "havemagaz")
                {
                    SFX.Play("click");
                    if (_raised)
                        Level.Add(new Czmag(x, y + 1));
                    else if (offDir < 0)
                        Level.Add(new Czmag(x + 5, y));
                    else
                        Level.Add(new Czmag(x - 5, y));
                    _sprite.SetAnimation("haventmagaz");
                    _fdelay = 40;
                }
            }            
        }
        public override void Update()
        {
            base.Update();
            if (_fdelay > 1)
            {
                _fdelay -= 1;
            }
            else if (_fdelay == 1)
            {
                SFX.Play("click");
                _fdelay -= 1;
            }
        }
    }
}
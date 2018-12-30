using DuckGame;
using TMGmod.Core;

namespace TMGmod.NY
{
    [EditorGroup("TMG|Misc|Holiday")]
    public class GarlandGun : Gun
    {
        private SpriteMap _sprite;
        private float _sprwait;
        public GarlandGun(float xval, float yval) : base(xval, yval)
        {
            ammo = 24;
            _ammoType = new AT9mmParasha //работай, тварь
            {
                range = 1000f,
                accuracy = 1f,
                bulletSpeed = 4.5f,
                penetration = 0f
            };
            _type = "gun";
            _graphic = new Sprite(GetPath("Holiday/Garlandun"));
            _center = new Vec2(11f, 3f);
            _collisionOffset = new Vec2(-7.5f, -3.5f);
            _collisionSize = new Vec2(22f, 11f);
            _barrelOffsetTL = new Vec2(22f, 6f);
            _holdOffset = new Vec2(2f, -1f);
            _fireSound = GetPath("sounds/2.wav");
            _fullAuto = false;
            _fireWait = 0f;
            _kickForce = 0f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            _editorName = "Garlandun";
            _weight = 1f;
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _sprite = new SpriteMap(GetPath("Holiday/Garland_2"), 16, 9);
            _sprite.CenterOrigin();
        }
        public override void Update()
        {
            _sprwait -= 0.11f;
            if (_sprwait <= 0f)
            {
                var randres = Rando.Int(0, 29);
                while (randres == _sprite.frame)
                {
                    randres = Rando.Int(0, 29);
                }
                _sprite.frame = randres;
                _ammoType.sprite = _sprite;
                _sprwait += 1.0f;
            }
            base.Update();
        }
    }
}
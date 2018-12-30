using DuckGame;
using TMGmod.Core;

namespace TMGmod.NY
{
    [EditorGroup("TMG|Misc|Holiday")]
    public class GarlandGun : Gun
    {
        private SpriteMap _sprite;
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
            graphic = new Sprite(GetPath("Holiday/Garlandun"));
            center = new Vec2(11f, 3f);
            collisionOffset = new Vec2(-7.5f, -3.5f);
            collisionSize = new Vec2(22f, 11f);
            _barrelOffsetTL = new Vec2(22f, 6f);
            _holdOffset = new Vec2(2f, -1f);
            _fireSound = GetPath("sounds/2.wav");
            _fullAuto = false;
            _fireWait = 0f;
            _kickForce = 0f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            _editorName = "Garlandun";
            weight = 1f;
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
        }
        public override void Update()
        {
            _sprite = new SpriteMap(GetPath("Holiday/Garland_2"), 16, 9);
            _sprite.frame = Rando.Int(0, 29);
            _sprite.CenterOrigin();
            _ammoType.sprite = _sprite;
            base.Update();
        }
    }
}
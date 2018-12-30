using DuckGame;

namespace TMGmod.NY
{
    [EditorGroup("TMG|Misc|Holiday")]
    public class SpruceGun:Gun
    {
        public SpruceGun(float xval, float yval) : base(xval, yval)
        {
            ammo = 15;
            _ammoType = new AT9mm
            {
                sprite = new Sprite(GetPath("Holiday/Igolka")),
                range = 385f,
                accuracy = Rando.Float(0f, 0.9f),
                penetration = 1f,
                bulletSpeed = Rando.Float(0.1f, 15f),
                bulletLength = 0f,
                bulletThickness = 0.25f
            };
            _ammoType.sprite.CenterOrigin();
            _numBulletsPerFire = Rando.Int(5, 100);
            _type = "gun";
            _graphic = new Sprite(GetPath("Holiday/ShorTree"));
            _center = new Vec2(25.5f, 7.5f);
            _collisionOffset = new Vec2(-25.5f, -7.5f);
            _collisionSize = new Vec2(51f, 15f);
            _barrelOffsetTL = new Vec2(36f, 7f);
            _holdOffset = new Vec2(3f, -2f);
            _fireSound = "shotgunFire2";
            _kickForce = 3.75f;
            _fullAuto = false;
            _fireWait = 2f;
            _editorName = "Tree-12";
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
        }
        public override void Update()
        {
            _ammoType.bulletSpeed = Rando.Float(0.1f, 7f);
            base.Update();
        }
        public override void OnReleaseAction()
        {
            _ammoType.accuracy = Rando.Float(0f, 0.9f);
            _numBulletsPerFire = Rando.Int(5, 50);
            base.OnReleaseAction();
        }
    }
}
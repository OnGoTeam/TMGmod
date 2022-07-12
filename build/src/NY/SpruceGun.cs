using DuckGame;

namespace TMGmod.NY
{
    [EditorGroup("TMG|Misc|Holiday")]
    public class SpruceGun : Gun
    {
        public SpruceGun(float xval, float yval) : base(xval, yval)
        {
            ammo = 12;
            _ammoType = new ATIglu
            {
                range = 385f,
                accuracy = Rando.Float(0f, 0.9f),
                bulletSpeed = Rando.Float(0.1f, 7f)
            };
            _numBulletsPerFire = Rando.Int(25, 100);
            _type = "gun";
            _graphic = new Sprite(GetPath("Holiday/ShorTree"));
            _center = new Vec2(26f, 8f);
            _collisionOffset = new Vec2(-26f, -8f);
            _collisionSize = new Vec2(51f, 15f);
            _barrelOffsetTL = new Vec2(36f, 7f);
            _holdOffset = new Vec2(7f, -1f);
            _fireSound = "shotgunFire2";
            _kickForce = 5f;
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
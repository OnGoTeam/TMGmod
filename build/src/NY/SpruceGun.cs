using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod.NY
{
    [EditorGroup("TMG|Misc|Holiday")]
    public class SpruceGun : BaseGun
    {
        public SpruceGun(float xval, float yval) : base(xval, yval)
        {
            ammo = 12;
            _type = "gun";
            SkinFrames = 1;
            Smap = new SpriteMap(GetPath("Holiday/ShorTree"), 51, 15);
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
            ResetModifier();
            _numBulletsPerFire = Rando.Int(5, 50);
            SetAmmoType<ATIgla>();
            Randomize();
        }

        private void Randomize()
        {
            _ammoType.bulletSpeed = Rando.Float(0.1f, 7f);
            _ammoType.accuracy = Rando.Float(0f, 0.9f);
            _numBulletsPerFire = Rando.Int(5, 50);
        }

        protected override float Accuracy => _ammoType.accuracy;

        protected override void BaseOnSpent()
        {
            Randomize();
        }
    }
}

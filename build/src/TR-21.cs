using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class TR21 : BaseGun, IAmSg
    {
  
        private readonly SpriteMap _sprite;
        private readonly int _teksturka;
		
        public TR21 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 12;
            _ammoType = new ATMagnum
            {
                range = 110f,
                accuracy = 0.8f,
                penetration = 1f,
                bulletThickness = 0.2f
            };
            BaseAccuracy = 0.8f;
            _numBulletsPerFire = 9;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("TR-21p"), 22, 14);
            _graphic = _sprite;
            _teksturka = Rando.Int(0, 3);
            _sprite.frame = _teksturka;
            _center = new Vec2(11f, 6f);
            _collisionOffset = new Vec2(-11f, -6f);
            _collisionSize = new Vec2(22f, 14f);
            _barrelOffsetTL = new Vec2(22f, 4f);
            _fireSound = "shotgunFire";
            _fullAuto = true;
            _fireWait = 0.7f;
            _kickForce = 6f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.25f;
            _holdOffset = new Vec2(-5f, 2f);
            _editorName = "Crocodile 12 ammo";
			_weight = 5.5f;
        }
        public override void Draw()
        {
            _sprite.frame = _teksturka;
            base.Draw();
        }
    }
}
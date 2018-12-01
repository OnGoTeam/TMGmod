using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Shotgun")]
    public class TR21 : Gun
    {
  
     private SpriteMap _sprite;
     public int teksturka = 1;
		
        public TR21 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 12;
            _ammoType = new ATMagnum
            {
                range = 110f,
                accuracy = 0.8f,
                penetration = 1f
            };
            _numBulletsPerFire = 4;
            _ammoType.bulletThickness = 0.2f;
            _type = "gun";
            _sprite = new SpriteMap((GetPath("TR-21p")), 22, 14, false);
            graphic = (Sprite)_sprite;
            teksturka = Rando.Int(0, 3);
            _sprite.frame = teksturka;
            center = new Vec2(11f, 6f);
            collisionOffset = new Vec2(-11f, -6f);
            collisionSize = new Vec2(22f, 14f);
            _barrelOffsetTL = new Vec2(22f, 4f);
            _fireSound = "shotgunFire";
            _fullAuto = true;
            _fireWait = 0.7f;
            _kickForce = 1.6f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.25f;
            _holdOffset = new Vec2(-5f, 2f);
            _editorName = "Crocodile";
			weight = 5.5f;
        }
        public override void Draw()
        {
            _sprite.frame = teksturka;
            base.Draw();
        }
    }
}
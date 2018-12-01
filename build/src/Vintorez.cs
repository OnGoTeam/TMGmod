using DuckGame;
using TMGmod.Core;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Sniper")]
    public class Vintorez : Gun
    {
  
        private readonly SpriteMap _sprite;
        private readonly int _teksturka;
		
        public Vintorez(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 10;
            _ammoType = new AT9mmS
            {
                range = 680f,
                accuracy = 0.9f,
                penetration = 1.5f,
                bulletSpeed = 25f
            };
            _type = "gun";
			//I AM A GREEN TEXT
            //NO, I AM THE REAL ONE GREEN TEXT
            _sprite = new SpriteMap(GetPath("Vintorezptr"), 33, 11);
            graphic = _sprite;
            _teksturka = Rando.Int(0, 3);
            _sprite.frame = _teksturka;
            center = new Vec2(16.5f, 5.5f);
            collisionOffset = new Vec2(-16.5f, -5.5f);
            collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(34f, 5f);
            _holdOffset = new Vec2(3f, 0f);
            _fireSound = GetPath("sounds/Silenced1.wav");
            _fullAuto = true;
            _fireWait = 0.7f;
            _kickForce = 0.85f;
            loseAccuracy = 0.08f;
            maxAccuracyLost = 0.15f;
            _editorName = "Vintorez";
			weight = 4.7f;
		}
        public override void Draw()
        {
            _sprite.frame = _teksturka;
            base.Draw();
        }
    }
}
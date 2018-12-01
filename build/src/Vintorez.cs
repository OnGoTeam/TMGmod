using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Sniper")]
    public class Vintorez : Gun
    {
  
     private SpriteMap _sprite;
     public int teksturka = 1;
		
        public Vintorez(float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 10;
            this._ammoType = new AT9mmS
            {
                range = 680f,
                accuracy = 0.9f,
                penetration = 1.5f,
                bulletSpeed = 25f
            };
            this._type = "gun";
			//I AM A GREEN TEXT
            //NO, I AM THE REAL ONE GREEN TEXT
            this._sprite = new SpriteMap((GetPath("Vintorezptr")), 33, 11, false);
            this.graphic = (Sprite)this._sprite;
            this.teksturka = Rando.Int(0, 3);
            this._sprite.frame = teksturka;
            this.center = new Vec2(16.5f, 5.5f);
            this.collisionOffset = new Vec2(-16.5f, -5.5f);
            this.collisionSize = new Vec2(33f, 11f);
            this._barrelOffsetTL = new Vec2(34f, 5f);
            this._holdOffset = new Vec2(3f, 0f);
            this._fireSound = GetPath("sounds/Silenced1.wav");
            this._fullAuto = true;
            this._fireWait = 0.7f;
            this._kickForce = 0.85f;
            this.loseAccuracy = 0.08f;
            this.maxAccuracyLost = 0.15f;
            this._editorName = "Vintorez";
			this.weight = 4.7f;
		}
        public override void Draw()
        {
            this._sprite.frame = teksturka;
            base.Draw();
        }
    }
}
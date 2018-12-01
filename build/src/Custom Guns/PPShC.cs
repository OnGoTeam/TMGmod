using DuckGame;

namespace TMGmod.src
{

    [BaggedProperty("isInDemo", true), EditorGroup("TMG|SMG|Custom")]
    public class PPShC : Gun
    {
  
     private SpriteMap _sprite;
     public int teksturka = 1;

        public PPShC(float xval, float yval)
            : base(xval, yval)
        {
            this.ammo = 35;
            this._ammoType = new AT9mm();
            this._ammoType.range = 300f;
            this._ammoType.accuracy = 0.9f;
            this._type = "gun";
            this._sprite = new SpriteMap((GetPath("PPshlowmagptr")), 48, 11, false);
            this.graphic = (Sprite)this._sprite;
            this.teksturka = Rando.Int(0, 5);
            this._sprite.frame = teksturka;
            this.center = new Vec2(23f, 5.5f);
            this.collisionOffset = new Vec2(-23f, -4.5f);
            this.collisionSize = new Vec2(46f, 11f);
            this._barrelOffsetTL = new Vec2(47f, 4f);
            this._fireSound = "deepMachineGun2";
            this._fullAuto = true;
            this._fireWait = 0.25f;
            this._kickForce = 0.5f;
            this._holdOffset = new Vec2(7f, -1f);
            this.loseAccuracy = 0.05f;
            this.maxAccuracyLost = 0.2f;
            this._editorName = "PPSh with Low Mag";
			this.weight = 5.5f;
        }
        public override void Draw()
        {
            this._sprite.frame = teksturka;
            base.Draw();
        }
    }
}
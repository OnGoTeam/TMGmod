using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Misc|Custom Guns")]
    public class TR21C : Gun
    {
  
     private SpriteMap _sprite;
     public int teksturka = 1;
		
        public TR21C (float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 30;
            this._ammoType = new ATMagnum();
            this._ammoType.range = 110f;
            this._ammoType.accuracy = 0.8f;
            this._ammoType.penetration = 1f;
            this._numBulletsPerFire = 4;
            this._ammoType.bulletThickness = 0.2f;
            this._type = "gun";
            this._sprite = new SpriteMap((GetPath("TR-21lmg2p")), 22, 14, false);
            this.graphic = (Sprite)this._sprite;
            this.teksturka = Rando.Int(0, 3);
            this._sprite.frame = teksturka;
            this.center = new Vec2(11f, 6f);
            this.collisionOffset = new Vec2(-11f, -6f);
            this.collisionSize = new Vec2(22f, 14f);
            this._barrelOffsetTL = new Vec2(22f, 4f);
            this._fireSound = "shotgunFire";
            this._fullAuto = true;
            this._fireWait = 0.8f;
            this._kickForce = 1.6f;
            this.loseAccuracy = 0.1f;
            this.maxAccuracyLost = 0.25f;
            this._holdOffset = new Vec2(-5f, 2f);
            this._editorName = "Crocodile with Extended Mag";
			this.weight = 7f;
        }
        public override void Draw()
        {
            this._sprite.frame = teksturka;
            base.Draw();
        }
    }
}
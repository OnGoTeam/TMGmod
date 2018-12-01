using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Misc|Custom Guns")]
    public class VintorezC : Gun
    {
  
     private SpriteMap _sprite;
     public int teksturka = 1;
		
        public VintorezC(float xval, float yval)
          : base(xval, yval)
        {
            this.ammo = 20;
            this._ammoType = new AT9mmS();
            this._ammoType.range = 680f;
            this._ammoType.accuracy = 0.9f;
            this._ammoType.penetration = 1.5f;
            this._ammoType.bulletSpeed = 25f;
            this._type = "gun";
			//I AM A GREEN TEXT
            this._sprite = new SpriteMap((GetPath("Vintorezexmagptr")), 33, 12, false);
            this.graphic = (Sprite)this._sprite;
            this.teksturka = Rando.Int(0, 3);
            this._sprite.frame = teksturka;
            this.center = new Vec2(16.5f, 6f);
            this.collisionOffset = new Vec2(-16.5f, -6f);
            this.collisionSize = new Vec2(33f, 12f);
            this._barrelOffsetTL = new Vec2(34f, 5f);
            this._holdOffset = new Vec2(3f, 0f);
            this._fireSound = GetPath("sounds/Silenced1.wav");
            this._fullAuto = true;
            this._fireWait = 0.7f;
            this._kickForce = 0.85f;
            this.loseAccuracy = 0.08f;
            this.maxAccuracyLost = 0.15f;
            this._editorName = "Vintorez with Extended Mag";
			this.weight = 4.7f;
		}
        public override void Draw()
        {
            this._sprite.frame = teksturka;
            base.Draw();
        }
    }
}
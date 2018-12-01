﻿using DuckGame;

namespace TMGmod.src
{
    [EditorGroup("TMG|Sniper|Custom")]
    public class VintorezC : Gun
    {
  
     private SpriteMap _sprite;
     public int teksturka = 1;
		
        public VintorezC(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 20;
            _ammoType = new AT9mmS
            {
                range = 680f,
                accuracy = 0.9f,
                penetration = 1.5f,
                bulletSpeed = 25f
            };
            _type = "gun";
			//I AM A GREEN TEXT
            _sprite = new SpriteMap((GetPath("Vintorezexmagptr")), 33, 12, false);
            graphic = (Sprite)_sprite;
            teksturka = Rando.Int(0, 3);
            _sprite.frame = teksturka;
            center = new Vec2(16.5f, 6f);
            collisionOffset = new Vec2(-16.5f, -6f);
            collisionSize = new Vec2(33f, 12f);
            _barrelOffsetTL = new Vec2(34f, 5f);
            _holdOffset = new Vec2(3f, 0f);
            _fireSound = GetPath("sounds/Silenced1.wav");
            _fullAuto = true;
            _fireWait = 0.7f;
            _kickForce = 0.85f;
            loseAccuracy = 0.08f;
            maxAccuracyLost = 0.15f;
            _editorName = "Vintorez with Extended Mag";
			weight = 4.7f;
		}
        public override void Draw()
        {
            _sprite.frame = teksturka;
            base.Draw();
        }
    }
}
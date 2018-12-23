﻿using DuckGame;
// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    public class DaewooK1 : Gun
    {

        private readonly SpriteMap sprite;
        private bool _stock;
		
        public DaewooK1 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 40;
            _ammoType = new ATMagnum
            {
                range = 345f,
                accuracy = 0.83f,
                penetration = 1f
            };
            _type = "gun";
            sprite = new SpriteMap(GetPath("DaewooK1pattern"), 28, 11);
            graphic = sprite;
            sprite.frame = 0;
            center = new Vec2(14f, 5f);
            collisionOffset = new Vec2(-14f, -5f);
            collisionSize = new Vec2(28f, 11f);
            _barrelOffsetTL = new Vec2(28f, 3f);
            _holdOffset = new Vec2(-1f, 0f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 0.86f;
            _kickForce = 0.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.24f;
            _editorName = "Daewoo K1";
			weight = 4.5f;
        }
        public override void Update()
        {
            if (owner != null)
            {
                if (duck.inputProfile.Pressed("QUACK"))
                {
					if (_stock)
                    {
                        graphic = sprite;
                        sprite.frame -= 5;
                        loseAccuracy = 0.1f;
				        maxAccuracyLost = 0.24f;
			            weight = 4.5f;
						_stock = false;
					}
                    else
                    {
                        graphic = sprite;
                        sprite.frame += 5;
                        loseAccuracy = 0.2f;
				        maxAccuracyLost = 0.36f;
			            weight = 3f;
						_stock = true;
					}
				}
			}
		    base.Update();
		}			
	}
}
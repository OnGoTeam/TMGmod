﻿using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|LMG")]
    // ReSharper disable once InconsistentNaming
    public class MG3 : Gun, IAmLmg
    {
        private bool _bipodes;
		
		public MG3 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 80;
            _ammoType = new ATMagnum
            {
                range = 600f,
                accuracy = 0.8f,
                penetration = 1.5f
            };
            _type = "gun";
            _graphic = new Sprite(GetPath("mg3"));
            _center = new Vec2(19.5f, 6f);
            _collisionOffset = new Vec2(-19.5f, -6f);
            _collisionSize = new Vec2(39f, 9f);
            _barrelOffsetTL = new Vec2(40f, 4f);
            _fireSound = GetPath("sounds/RifleOrMG.wav");
            _fullAuto = true;
            _fireWait = 0.5f;
            _kickForce = 0.95f;
            loseAccuracy = 0.025f;
            maxAccuracyLost = 0.1f;
            _holdOffset = new Vec2(4f, 2f);
            _editorName = "MG3";
			_weight = 7f;
        }
        public override void Update()
        {
            if (duck != null && duck.height < 17f)
            {
                _kickForce = 0f;
			    loseAccuracy = 0f;
                maxAccuracyLost = 0f;
			    graphic = new Sprite(GetPath("mg3bipods"));
			    _bipodes = true;
            }
            else
            {
                _kickForce = 0.95f;
                loseAccuracy = 0.025f;
                maxAccuracyLost = 0.1f;
			    graphic = new Sprite(GetPath("mg3"));
			    _bipodes = false;
            }
            base.Update();
		    if (ammo == 0 && !_bipodes) graphic = new Sprite(GetPath("mg31"));
		    if (ammo == 0 && _bipodes) graphic = new Sprite(GetPath("mg3bipods1"));
        }
	}
}	
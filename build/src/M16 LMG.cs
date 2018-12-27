﻿using DuckGame;
using TMGmod.Core.WClasses;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|LMG")]
    // ReSharper disable once InconsistentNaming
    public class M16LMG : BaseLmg
    {
		
		public M16LMG (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 95;
            _ammoType = new ATMagnum
            {
                range = 345f,
                accuracy = 0.8f,
                penetration = 1.5f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("m4lmg"));
            center = new Vec2(19f, 6f);
            collisionOffset = new Vec2(-19f, -6f);
            collisionSize = new Vec2(38f, 11f);
            _barrelOffsetTL = new Vec2(38f, 4f);
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.825f;
            _kickForce = 0.33f;
            loseAccuracy = 0.01f;
            maxAccuracyLost = 0.1f;
            _holdOffset = new Vec2(5f, 1f);
            _editorName = "M16-LMG";
			weight = 5.75f;
            BaseAccuracy = 0.8f;
            MinAccuracy = 0.7f;
            Kforce1Lmg = 0.23f;
            Kforce2Lmg = 0.43f;
        }
          public override void Update()
        {
            if (_owner != null && _owner.height < 17f)
            {
                _kickForce = 0f;
				loseAccuracy = 0.005f;
                maxAccuracyLost = 0.6f;
            }
            else
            {
                _kickForce = 0.33f;
                loseAccuracy = 0.01f;
                maxAccuracyLost = 0.12f;
            }
            base.Update();
        }
	}
}	
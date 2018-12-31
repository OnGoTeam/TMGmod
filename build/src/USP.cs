﻿using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{
    [EditorGroup("TMG|Pistol")]
    // ReSharper disable once InconsistentNaming
    public class USP : BaseGun, IAmHg
    {
        public bool Silencer;
        public StateBinding SilencerBinding = new StateBinding(nameof(Silencer));

        public USP(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 13;
            _ammoType = new AT9mm
            {
                range = 100f,
                accuracy = 0.8f,
                penetration = 1f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("USP"));
            center = new Vec2(8f, 3f);
            collisionOffset = new Vec2(-7.5f, -3.5f);
            collisionSize = new Vec2(23f, 9f);
            _barrelOffsetTL = new Vec2(15f, 3f);
            _fireSound = GetPath("sounds/1.wav");
            _fullAuto = false;
            _fireWait = 0.75f;
            _kickForce = 0f;
            loseAccuracy = 0.05f;
            maxAccuracyLost = 0.1f;
            _editorName = "USP-S";
			weight = 1f;
        }
        public override void Update()
        {
            if (duck != null)
            {
                if (duck.inputProfile.Pressed("QUACK"))
                {
                    if (Silencer)
                    {
                        graphic = new Sprite(GetPath("USP"));
                        _fireSound = GetPath("sounds/1.wav");
                        _ammoType = new AT9mm
                        {
                            range = 100f,
                            accuracy = 0.8f
                        };
                        _barrelOffsetTL = new Vec2(15f, 3f);
                        Silencer = false;
                    }
                    else
                    {
                        graphic = new Sprite(GetPath("USPS"));
                        _fireSound = GetPath("sounds/SilencedPistol.wav");
                        _ammoType = new AT9mmS
                        {
                            range = 130f,
                            accuracy = 0.9f
                        };
                        _barrelOffsetTL = new Vec2(23f, 3f);
                        Silencer = true;
                    }
                }
			}
		    base.Update();
		}			
	}
}

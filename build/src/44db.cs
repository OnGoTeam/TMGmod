﻿using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun")]
    public class Deadly44 : BaseGun, IAmSg
    {
		public Deadly44 (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 1;
            _ammoType = new ATMagnum
            {
                range = 150f,
                accuracy = 0.2f,
                penetration = 4f,
                bulletThickness = 2f
            };
            _numBulletsPerFire = 44;
            _type = "gun";
            _graphic = new Sprite(GetPath("44db"));
            _center = new Vec2(16.5f, 5f);
            _collisionOffset = new Vec2(-16.5f, -5f);
            _collisionSize = new Vec2(33f, 10f);
            _barrelOffsetTL = new Vec2(34f, 2.5f);
            _holdOffset = new Vec2(2f, 1f);
            _fireSound = "shotgun";
            _fullAuto = false;
            _fireWait = 4f;
            _kickForce = 9f;
            loseAccuracy = 0.25f;
            maxAccuracyLost = 0.5f;
            _editorName = "DeadlyGAUGA1e";
			_weight = 4f;
        }

        public override void Reload(bool shell = true)
        {
            if (ammo > 0)
            {
                --ammo;
            }
            loaded = true;
        }
    }
}
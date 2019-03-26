using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Machinegun")]
    // ReSharper disable once InconsistentNaming
    public class MK17 : BaseAr
    {
        private float Damaged = 1;
        private readonly SpriteMap _sprite;

        public MK17(float xval, float yval)
          : base(xval, yval)
        {
            thickness = 5f;
            ammo = 20;
            _ammoType = new AT9mm
            {
                range = 370f,
                accuracy = 1f,
                penetration = 1f,
                bulletSpeed = 35f,
                bulletThickness = 0.6f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Mk17Shield"), 26, 12);
            _graphic = _sprite;
            _sprite.frame = 0;
            _graphic = _sprite;
            _center = new Vec2(13f, 6f);
            _collisionOffset = new Vec2(-13f, -6f);
            _collisionSize = new Vec2(26f, 12f);
            _barrelOffsetTL = new Vec2(26f, 5.5f);
            _holdOffset = new Vec2(5f, -1f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 0.81f;
            _kickForce = 2.35f;
		    Kforce1Ar = 2.3f;
		    Kforce2Ar = 2.6f;
            loseAccuracy = 0.3f;
            maxAccuracyLost = 1.8f;
            _editorName = "Mk17 with Shield";
			_weight = 7f;
        }
        public override bool DoHit(Bullet bullet, Vec2 hitPos)
        {
            Damaged = bullet.ammo.penetration;
            Damage(bullet.ammo);
            return Hit(bullet, hitPos);
        }
        private void Damage(AmmoType at)
        {
            thickness -= Damaged;
            if (thickness <= 0f)
            {
                _sprite.frame = 1;
            }
        }
    }
}
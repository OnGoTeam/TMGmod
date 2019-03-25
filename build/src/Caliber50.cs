using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Pistol")]
    public class BigShot : BaseGun, IAmHg
    {
        public BigShot (float xval, float yval)
          : base(xval, yval)
        {
            ammo = 7;
            _ammoType = new AT50C();
            _type = "gun";
            _graphic = new Sprite(GetPath("pistol50"));
            _center = new Vec2(13f, 5f);
            _collisionOffset = new Vec2(-13f, -5f);
            _collisionSize = new Vec2(26f, 10f);
            _barrelOffsetTL = new Vec2(26f, 2f);
            _fireSound = "magnum";
            _fullAuto = false;
            _fireWait = 1.6f;
            _kickForce = 3.76f;
            loseAccuracy = 0.5f;
            maxAccuracyLost = 1f;
            _holdOffset = new Vec2(0f, -2f);
            _editorName = "50AE Pistol";
			_weight = 2.5f;
        }
    }
}

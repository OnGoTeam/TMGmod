using DuckGame;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|MP")]
    // ReSharper disable once InconsistentNaming
    public class ANP73 : BaseGun, IAmHg
    {
        private readonly SpriteMap _sprite;
        public ANP73(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 33;
            _ammoType = new AT9mm
            {
                range = 195f,
                accuracy = 0.81f,
                penetration = 1f,
                bulletSpeed = 12f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Experimental ANP-73"), 19, 14);
            _graphic = _sprite;
            _sprite.frame = 3;
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _center = new Vec2(10f, 7f);
            _collisionOffset = new Vec2(-10f, -7f);
            _collisionSize = new Vec2(19f, 14f);
            _barrelOffsetTL = new Vec2(18f, 2f);
            _holdOffset = new Vec2(3f, 2f);
            _fireSound = GetPath("sounds/2.wav");
            _fullAuto = true;
            _fireWait = 1.5f;
            _kickForce = 1.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.4f;
            _editorName = "Experimental ANP-73";
			_weight = 2f;
        }
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if ((_sprite.frame < 4) && (_sprite.frame > 2))
                {
                    _sprite.frame = 2;
                    _fireWait = 1.2f;
                    loseAccuracy = 0.15f;
                    maxAccuracyLost = 0.5f;
                }
                else if ((_sprite.frame < 3) && (_sprite.frame > 1))
                {
                    _sprite.frame = 1;
                    _fireWait = 0.9f;
                    loseAccuracy = 0.2f;
                    maxAccuracyLost = 0.6f;
                }
                else if ((_sprite.frame < 2) && (_sprite.frame > 0))
                {
                    _sprite.frame = 0;
                    _fireWait = 0.6f;
                    loseAccuracy = 0.3f;
                    maxAccuracyLost = 0.7f;
                }
                else if ((_sprite.frame < 1) && (_sprite.frame > -1))
                {
                    _sprite.frame = 3;
                    _fireWait = 1.5f;
                    loseAccuracy = 0.1f;
                    maxAccuracyLost = 0.4f;
                }
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }
            base.Update();
		}
        public override void Reload(bool shell = true)
        {
            if (ammo != 0)
            {
                --ammo;
            }
            loaded = true;
        }
    }
}
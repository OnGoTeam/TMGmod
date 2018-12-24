using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

// ReSharper disable VirtualMemberCallInConstructor

namespace TMGmod
{

    [BaggedProperty("isInDemo", true), EditorGroup("TMG|AutoPistol")]
    public class HazeS : BaseGun
    {
        private float _heatval;

        public HazeS(float xval, float yval) :
            base(xval, yval)
        {
            ammo = 26;
            _ammoType = new AT9mmS
            {
                accuracy = 0.9f,
                range = 180f,
                penetration = 1.1f,
                combustable = true,
                bulletSpeed = 60f
            };
            _type = "gun";
            graphic = new Sprite(GetPath("haze"));
            center = new Vec2(12f, 3f);
            collisionOffset = new Vec2(-12f, -3f);
            collisionSize = new Vec2(24f, 12f);
            _barrelOffsetTL = new Vec2(25f, 2f);
            _fireSound = GetPath("sounds/SilencedPistol.wav");
            _fullAuto = true;
            _fireWait = 0.9f;
            _kickForce = 0.5f;
            _holdOffset = new Vec2(1f, 0f);
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            _editorName = "AF Haze";
            laserSight = true;
            _laserOffsetTL = new Vec2(16f, 6f);
			weight = 2f;
            _flare = new SpriteMap(GetPath("hazeFlare"), 13, 10) {center = new Vec2(0.0f, 5f)};
        }

        public override void Update()
        {
            if (_heatval > 6f) _heatval = 6f;
            _ammoType.accuracy = _heatval > 3f ? 1.2f - _heatval * 0.1f: 0.9f;
            _ammoType.bulletSpeed = 60f + 10f * _heatval;
            _heatval -= 0.05f;
            _ammoType.range = 180f;
            if (_heatval < 0f) _heatval = 0f;
            if (duck != null)
            {
                if (duck.inputProfile.Down("QUACK") && _heatval < 1f)
                {
                    _holdOffset = new Vec2(3f, -2f) * 0.2f + _holdOffset * 0.8f;
                    _ammoType.accuracy = 1f;
                    _ammoType.range = 450f;
                }
                else
                {
                    _holdOffset = new Vec2(1f, 0f) * 0.2f + _holdOffset * 0.8f;
                }
            }
            else
            {
                _holdOffset = new Vec2();
            }
            base.Update();
        }

        public override void Fire()
        {

            if (_wait <= 0f)
            {
                if (duck != null && duck.inputProfile.Down("QUACK") && _heatval < 1f)
                {
                    _heatval += 4f;
                }
                _heatval += 1f;
            }

            base.Fire();
        }
    }
}

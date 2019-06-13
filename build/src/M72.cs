using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Grenadelauncher")]
    [PublicAPI]
    public class M72 : BaseGun
    {
        private readonly SpriteMap _sprite;

        public M72(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 5;
            _ammoType = new ATGrenade
            {
                range = 2000f,
                accuracy = 0.95f,
                barrelAngleDegrees = -5f,
                bulletSpeed = 15f
            };
            BaseAccuracy = 0.95f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("M72"), 32, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(16f, 5.5f);
            _collisionOffset = new Vec2(-16f, -5.5f);
            _collisionSize = new Vec2(32f, 11f);
            _barrelOffsetTL = new Vec2(32f, 4f);
            _holdOffset = new Vec2(2f, 1f);
            _fireSound = "deepMachineGun";
            _fullAuto = false;
            _fireWait = 1f;
            _kickForce = 5f;
            loseAccuracy = 0.8f;
            maxAccuracyLost = 1f;
            _editorName = "M72 GL";
            _weight = 4.5f;
        }
        public override void Update()
        {
            switch (ammo)
            {
                case 4:
                    _sprite.frame = 1;
                    break;
                case 3:
                    _sprite.frame = 2;
                    break;
                case 2:
                    _sprite.frame = 3;
                    break;
                case 1:
                    _sprite.frame = 4;
                    break;
                case 0:
                    _sprite.frame = 5;
                    break;
            }

            base.Update();
        }
    }
}
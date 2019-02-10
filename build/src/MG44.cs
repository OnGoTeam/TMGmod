using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|LMG")]
    // ReSharper disable once InconsistentNaming
    public class MG44 : Gun, IHaveSkin, IAmLmg
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 3;
        /*
        private const float DefaultAccuracy = .75f;
        private const float MaxRaise = .6f;
        private const float EpsilonD = .2f;
        private const float EpsilonK = .1f;
        private const float EpsilonX = .8f;
        private const float EpsilonY = EpsilonK / EpsilonX;
        private const float AcclA = .045f;
        private const float AcclB = .225f;
        private float _raisestat;*/
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public MG44(float xval, float yval)
          : base(xval, yval)
        {
            ammo = 60;
            _ammoType = new ATMagnum
            {
                range = 750f,
                accuracy = 0.75f,
                penetration = 1.5f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("mg44reqpattern"), 39, 12);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(19.5f, 6f);
            _collisionOffset = new Vec2(-19.5f, -6f);
            _collisionSize = new Vec2(39f, 12f);
            _barrelOffsetTL = new Vec2(40f, 4f);
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.9f;
            _kickForce = 0.6f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            _holdOffset = new Vec2(4f, 0f);
            _editorName = "Magnium";
            _weight = 7.5f;
        }
        public override void Update()
        {
            base.Update();
            switch (ammo)
            {
                case 1:
                    if (_sprite.frame < 10) _sprite.frame += 10;
                    break;
                case 0:
                    if (_sprite.frame < 20) _sprite.frame += 10;
                    break;
            }
            /*
            if (_raisestat > MaxRaise) _raisestat = MaxRaise;
            if (_raisestat > 0f)
            {
                var δα = -EpsilonY - EpsilonK / (_raisestat - EpsilonX);

                if (offDir < 0)
                {
                    handAngle = δα;
                }
                else
                {
                    handAngle = -δα;
                }
            }
            _raisestat -= .015f;
            if (duck == null)
            {
                _raisestat = 0f;
                handAngle = 0f;
            }
            else
            {
                if (duck.crouch || duck.sliding) _raisestat -= .005f;
                if (duck.vSpeed > 0f || _raisestat > AcclB) _raisestat += 0.05f * duck.vSpeed;
                if (!(_raisestat < 0f)) return;
                _raisestat = 0f;
                handAngle = 0f;
            }
            */
        }

        /*
        public override void Fire()
        {
            var wasammo = ammo > 0;
            _ammoType.accuracy = _raisestat < AcclA ? 1f : DefaultAccuracy;
            base.Fire();
            if (!wasammo) return;
            if (_raisestat < AcclA) _raisestat = AcclB;
            var raisek = (MaxRaise - EpsilonD * _raisestat) / MaxRaise;
            _raisestat += Rando.Float(.10f * (_kickForce / weight) * raisek, .15f * (_kickForce / weight) * raisek + 0.01f);
        }
        */
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}
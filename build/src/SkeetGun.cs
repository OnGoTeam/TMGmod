using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun|Break-Action")]
    public class SkeetGun : BaseGun, IHaveAllowedSkins, IAmSg
    {
        private float _handleAngleOff;

        [UsedImplicitly] public StateBinding HandAngleOffBinding = new(nameof(HandAngleOff));

        public SkeetGun(float xval, float yval) : base(xval, yval)
        {
            _editorName = "Skeet Double";
            ammo = 2;
            SetAmmoType<ATSkeetGun>();
            _numBulletsPerFire = 10;
            Smap = new SpriteMap(GetPath("SkeetDouble"), 41, 7);
            _center = new Vec2(21f, 4f);
            _collisionOffset = new Vec2(-21f, -4f);
            _collisionSize = new Vec2(41f, 7f);
            _fireSound = "shotgunFire";
            _barrelOffsetTL = new Vec2(41f, .5f);
            _flare = FrameUtils.FlareOnePixel2();
            _fireWait = 0.5f;
            _kickForce = 6.55f;
            _holdOffset = new Vec2(8f, 2f);
#if DEBUG
            var field = typeof(Gun).GetField(
                "_barrelSmoke",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
            );
            field?.SetValue(this, FrameUtils.TakeZis());
#endif
        }

        public float HandAngleOff
        {
            get => handAngle * offDir;
            set => handAngle = value * offDir;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 4, 6, 7, 9 });

        protected override void RealFire()
        {
            _barrelOffsetTL.y = ammo switch
            {
                2 => .5f,
                1 => 2.5f,
                _ => _barrelOffsetTL.y,
            };

            base.RealFire();
        }

        public override void Update()
        {
            Hint("raise", () => barrelOffset, "UP");
            HandAngleOff = _handleAngleOff;
            base.Update();
            if (duck is null)
            {
                _handleAngleOff = 0f;
                return;
            }

            if (duck.inputProfile.Down("UP") && !_raised)
            {
                if (_handleAngleOff > -0.5f) _handleAngleOff -= 0.05f;
                return;
            }

            switch (_handleAngleOff)
            {
                case > 0f:
                    _handleAngleOff -= 0.025f;
                    break;
                case < 0f:
                    _handleAngleOff += 0.025f;
                    break;
            }

            if ((_handleAngleOff > -0.025f) & (_handleAngleOff < 0.025f)) _handleAngleOff = 0f;
        }
    }
}

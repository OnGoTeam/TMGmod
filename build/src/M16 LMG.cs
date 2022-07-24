using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.BipodsLogic;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|LMG")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class M16LMG : BaseLmg, IHaveAllowedSkins, IHaveBipods
    {
        public M16LMG(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "M16 LMG";
            ammo = 50;
            SetAmmoType<AT556NATO>(.8f);
            Smap = new SpriteMap(GetPath("M16LMG"), 38, 11);
            _center = new Vec2(19f, 6f);
            _collisionOffset = new Vec2(-19f, -6f);
            _collisionSize = new Vec2(38f, 11f);
            _barrelOffsetTL = new Vec2(38f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.83f;
            _kickForce = 2.33f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(6f, 1f);
            ShellOffset = new Vec2(-7f, -2f);
            _weight = 6f;
            MinAccuracy = 0.7f;
            KickForce1Lmg = 0.23f;
            KickForce2Lmg = 0.43f;
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 400f;
            _ammoType.penetration = 1.5f;
            base.OnInitialize();
        }

        public BitBuffer BipodsBuffer
        {
            get => this.GetBipodBuffer();
            set => this.SetBipodBuffer(value);
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        public bool Bipods
        {
            get => BipodsQ();
            set
            {
                _kickForce = value ? 0 : 2.33f;
                KickForce1Lmg = value ? 0 : 0.23f;
                KickForce2Lmg = value ? 0 : 0.43f;
                loseAccuracy = value ? 0 : 0.15f;
            }
        }

        public StateBinding BipodsBinding { get; } = new StateBinding(nameof(BipodsBuffer));
        public bool BipodsDisabled => false;
    }
}

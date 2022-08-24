using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.BipodsLogic;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Bolt-Action")]
    // ReSharper disable once InconsistentNaming
    public class MSR : BaseBolt, IHaveAllowedSkins, IHaveBipods
    {
        public MSR(float xval, float yval) : base(xval, yval)
        {
            _editorName = "MSR";
            Smap = new SpriteMap(GetPath("MSR"), 47, 12);
            _center = new Vec2(24f, 6f);
            _collisionOffset = new Vec2(-24f, -6f);
            _collisionSize = new Vec2(47f, 12f);
            _barrelOffsetTL = new Vec2(47f, 4f);
            _flare = FrameUtils.FlareOnePixel3();
            ammo = 5;
            _fireSound = GetPath("sounds/new/HeavySniper.wav");
            _kickForce = 5.5f;
            _holdOffset = new Vec2(10f, 0f);
            _weight = 4.65f;
            ShellOffset = new Vec2(-8f, -1f);
            SetAmmoType<ATBoltAction>();
        }

        protected override void OnInitialize()
        {
            _ammoType.bulletSpeed = 100f;
            _ammoType.range = 1200;
            _ammoType.penetration = 2f;
            base.OnInitialize();
        }

        [UsedImplicitly]
        public BitBuffer BipodsBuffer
        {
            get => this.GetBipodBuffer();
            set => this.SetBipodBuffer(value);
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 9 });

        public bool Bipods
        {
            get => HandleQ();
            set => _kickForce = value ? 1f : 5.5f;
        }

        public StateBinding BipodsBinding { get; } = new StateBinding(nameof(BipodsBuffer));
        public bool BipodsDisabled => false;

        protected override bool HasLaser()
        {
            return false;
        }

        protected override float MaxAngle()
        {
            return Bipods ? .067f : .1f;
        }

        protected override float MaxOffset()
        {
            return 4.0f;
        }

        protected override float ReloadSpeed()
        {
            return Bipods ? .5f : .67f;
        }
    }
}

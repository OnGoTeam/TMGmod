using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core.Modifiers.Kforce;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Burst")]
    // ReSharper disable once InconsistentNaming
    public class AN94C : BaseGun, IAmAr, IHaveAllowedSkins
    {
        [UsedImplicitly] public StateBinding SilencerBinding = new StateBinding(nameof(Silencer));

        public AN94C(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Anyx AR2 Mustang";
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("Anyx AR2 Mustang"), 33, 10);
            _center = new Vec2(16f, 5f);
            _collisionOffset = new Vec2(-15f, -5f);
            _collisionSize = new Vec2(33f, 10f);
            _barrelOffsetTL = new Vec2(28, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(3f, 2f);
            ShellOffset = new Vec2(-4f, -2f);
            ammo = 30;
            SetAmmoType<ATCZ>();
            _fireSound = GetPath("sounds/new/HighCaliber-LessImpact.wav");
            _fullAuto = false;
            _fireWait = 2f;
            _kickForce = 1.2f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.25f;
            _weight = 4f;
            Compose(
                new HSpeedKforce(this, hspeed => hspeed > .1f, kforce => kforce + .83f)
            );
            ComposeSimpleBurst(2, .4f);
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 260f;
            _ammoType.bulletSpeed = 60f;
            base.OnInitialize();
        }

        protected override string HintMessage => "silencer";

        public bool Silencer
        {
            get => _fireSound == GetPath("sounds/new/HighCaliber-LessImpact-Silenced.wav");
            set
            {
                if (value)
                {
                    _fireSound = GetPath("sounds/new/HighCaliber-LessImpact-Silenced.wav");
                    _flare = new SpriteMap(GetPath("takezis"), 4, 4);
                    SetAmmoType<ATCZS>();
                    _barrelOffsetTL = new Vec2(33f, 2f);
                    NonSkin = 1;
                }
                else
                {
                    _fireSound = GetPath("sounds/new/HighCaliber-LessImpact.wav");
                    _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
                    {
                        center = new Vec2(0.0f, 5f),
                    };
                    SetAmmoType<ATCZ>();
                    _barrelOffsetTL = new Vec2(28f, 2f);
                    NonSkin = 0;
                }
            }
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 4, 7 });

        public override void Update()
        {
            if (Quacked())
            {
                SFX.Play(Silencer ? GetPath("sounds/silencer_off.wav") : GetPath("sounds/silencer_on.wav"));
                Silencer = !Silencer;
                SFX.Play("quack", -1);
            }

            base.Update();
        }
    }
}

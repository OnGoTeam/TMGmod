using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|DMR")]
    // ReSharper disable once InconsistentNaming
    public class TC12 : BaseDmr, IHaveAllowedSkins
    {
        [UsedImplicitly] public StateBinding SilencerBinding = new StateBinding(nameof(Silencer));

        public TC12(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "TC-12";
            ammo = 11;
            SetAmmoType<ATTC12>();
            MinAccuracy = 0.45f;
            RegenAccuracyDmr = 0.007f;
            DrainAccuracyDmr = 0.15f;
            MaxDrain = .55f;
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("TC-12"), 39, 12);
            _center = new Vec2(20f, 5f);
            _collisionOffset = new Vec2(-20f, -5f);
            _collisionSize = new Vec2(39f, 12f);
            _barrelOffsetTL = new Vec2(28f, 3f);
            _flare = new SpriteMap(GetPath("FlareTC12"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(6f, 0f);
            ShellOffset = new Vec2(-7f, -1f);
            _fireSound = GetPath("sounds/new/HighCaliber.wav");
            _fullAuto = false;
            _fireWait = 1.03f;
            _kickForce = 5.3f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            laserSight = true;
            _laserOffsetTL = new Vec2(26f, 5.5f);
            _weight = 4.5f;
        }

        protected override string HintMessage => "silencer";

        public bool Silencer
        {
            get => _fireSound == GetPath("sounds/new/TC12-Silenced.wav");
            set
            {
                if (value)
                {
                    NonSkin = 1;
                    _fireSound = GetPath("sounds/new/TC12-Silenced.wav");
                    SetAmmoType<ATTC12S>();
                    _kickForce = 4.5f;
                    loseAccuracy = 0f;
                    _weight = 6.3f;
                    _barrelOffsetTL = new Vec2(39f, 3f);
                    _flare = new SpriteMap(GetPath("FlareSilencer"), 13, 10)
                    {
                        center = new Vec2(0.0f, 5f),
                    };
                }
                else
                {
                    NonSkin = 0;
                    _fireSound = GetPath("sounds/new/HighCaliber.wav");
                    SetAmmoType<ATTC12>();
                    _kickForce = 5.3f;
                    loseAccuracy = 0.1f;
                    _weight = 4.5f;
                    _barrelOffsetTL = new Vec2(28f, 3f);
                    _flare = new SpriteMap(GetPath("FlareTC12"), 13, 10)
                    {
                        center = new Vec2(0.0f, 5f),
                    };
                }
            }
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 3 });

        public override void Update()
        {
            if (Quacked())
            {
                SFX.Play(Silencer ? GetPath("sounds/silencer_off.wav") : GetPath("sounds/silencer_on.wav"));
                Silencer = !Silencer;
                UnQuack();
            }

            base.Update();
        }
    }
}

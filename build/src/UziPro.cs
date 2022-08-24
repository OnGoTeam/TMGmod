using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class UziPro : BaseSmg, IHaveAllowedSkins
    {
        [UsedImplicitly] public StateBinding SilencerBinding = new StateBinding(nameof(Silencer));

        public UziPro(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Uzi Pro";
            ammo = 24;
            SetAmmoType<ATUzi>();
            KforceDelay = 25;
            KforceDelta = 4f;
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("UziProS"), 16, 10);
            _center = new Vec2(8f, 5f);
            _collisionOffset = new Vec2(-8f, -5f);
            _collisionSize = new Vec2(16f, 10f);
            _barrelOffsetTL = new Vec2(10f, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel0"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = GetPath("sounds/new/SMG-1.wav");
            _fullAuto = true;
            _fireWait = 0.4f;
            _kickForce = 0.5f;
            loseAccuracy = 0.08f;
            maxAccuracyLost = 0.5f;
            _holdOffset = new Vec2(1f, 1f);
            ShellOffset = new Vec2(-4f, -2f);
            laserSight = true;
            _laserOffsetTL = new Vec2(8f, 5.5f);
            _weight = 2.5f;
        }

        protected override string HintMessage => "silencer";

        public bool Silencer
        {
            get => _fireSound == GetPath("sounds/new/SMG-Silenced.wav");
            set
            {
                if (value)
                {
                    NonSkin = 1;
                    SetAmmoType<ATUziS>();
                    _barrelOffsetTL = new Vec2(16f, 2f);
                    _flare = new SpriteMap(GetPath("takezis"), 4, 4);
                    _fireSound = GetPath("sounds/new/SMG-Silenced.wav");
                }
                else
                {
                    NonSkin = 0;
                    SetAmmoType<ATUzi>();
                    _barrelOffsetTL = new Vec2(10f, 2f);
                    _flare = new SpriteMap(GetPath("FlareOnePixel0"), 13, 10)
                    {
                        center = new Vec2(0.0f, 5f),
                    };
                    _fireSound = GetPath("sounds/new/UziPro.wav");
                }
            }
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 4, 6, 9 });

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

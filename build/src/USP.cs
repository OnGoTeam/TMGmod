using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Semi-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class USP : BaseGun, IAmHg, IHaveAllowedSkins
    {
        [UsedImplicitly] public StateBinding SilencerBinding = new StateBinding(nameof(Silencer));

        public USP(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 13;
            SetAmmoType<ATUSP>();
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("USP"), 23, 9);
            _center = new Vec2(8f, 3f);
            _collisionOffset = new Vec2(-7.5f, -3.5f);
            _collisionSize = new Vec2(23f, 9f);
            _barrelOffsetTL = new Vec2(14f, 3f);
            _fireSound = GetPath("sounds/1.wav");
            _fullAuto = false;
            _fireWait = 0.75f;
            _kickForce = 1f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.4f;
            ShellOffset = new Vec2(-3f, -1f);
            _editorName = "USP-S";
            _weight = 1f;
        }

        protected override string HintMessage => "silencer";

        public bool Silencer
        {
            get => _fireSound == GetPath("sounds/SilencedPistol.wav");
            set
            {
                if (value)
                {
                    NonSkin = 1;
                    _fireSound = GetPath("sounds/SilencedPistol.wav");
                    _flare = new SpriteMap(GetPath("takezis"), 4, 4);
                    SetAmmoType<ATUSPS>();
                    _barrelOffsetTL = new Vec2(23f, 2f);
                }
                else
                {
                    NonSkin = 0;
                    _flare = new SpriteMap(GetPath("FlareOnePixel0"), 13, 10)
                    {
                        center = new Vec2(0.0f, 5f),
                    };
                    _fireSound = GetPath("sounds/1.wav");
                    SetAmmoType<ATUSP>();
                    _barrelOffsetTL = new Vec2(14f, 2f);
                }
            }
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 3, 4, 7 });
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                SFX.Play(Silencer ? GetPath("sounds/silencer_off.wav") : GetPath("sounds/silencer_on.wav"));
                Silencer = !Silencer;
                SFX.Play("quack", -1);
            }

            base.Update();
        }
    }
}

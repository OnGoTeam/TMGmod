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
    public class CZC2 : BaseAr, IHaveAllowedSkins
    {
        [UsedImplicitly] public StateBinding SilencerBinding = new StateBinding(nameof(Silencer));

        public CZC2(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 23;
            _ammoType = new ATCZ2();
            MaxAccuracy = _ammoType.accuracy;
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("CZC2"), 41, 11);
            _center = new Vec2(20.5f, 5.5f);
            _collisionOffset = new Vec2(-20.5f, -5.5f);
            _collisionSize = new Vec2(41f, 11f);
            _barrelOffsetTL = new Vec2(37f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(5f, 1f);
            ShellOffset = new Vec2(-5f, -3f);
            _fireSound = "deepMachineGun2";
            _fullAuto = false;
            _fireWait = 0.9f;
            loseAccuracy = 0.12f;
            maxAccuracyLost = 0.25f;
            _editorName = "CZ-C2 SAR";
            _weight = 4.4f;
            _kickForce = 1.5f;
            KforceDelta = 1.6f;
        }

        protected override string HintMessage => "silencer";

        public bool Silencer
        {
            get => _fireSound == GetPath("sounds/Silenced2.wav");
            set
            {
                if (value)
                {
                    NonSkin = 1;
                    _fireSound = GetPath("sounds/Silenced2.wav");
                    _ammoType = new ATCZS2();
                    loseAccuracy = 0.15f;
                    maxAccuracyLost = 0.28f;
                    _barrelOffsetTL = new Vec2(41f, 3f);
                    _flare = new SpriteMap(GetPath("takezis"), 4, 4);
                }
                else
                {
                    NonSkin = 0;
                    _fireSound = "deepMachineGun2";
                    _ammoType = new ATCZ2();
                    loseAccuracy = 0.1f;
                    maxAccuracyLost = 0.3f;
                    _barrelOffsetTL = new Vec2(37f, 3f);
                    _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
                    {
                        center = new Vec2(0.0f, 5f),
                    };
                }
            }
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

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

using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class CZ805 : BaseAr, IHaveAllowedSkins
    {
        [UsedImplicitly] public StateBinding SilencerBinding = new StateBinding(nameof(Silencer));

        public CZ805(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "CZ-805 BREN";
            ammo = 30;
            SetAmmoType<ATCZ>();
            NonSkinFrames = 10;
            Smap = new SpriteMap(GetPath("CZ805Bren"), 41, 11);
            _center = new Vec2(21f, 6f);
            _collisionOffset = new Vec2(-21f, -6f);
            _collisionSize = new Vec2(41f, 11f);
            _barrelOffsetTL = new Vec2(39f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(5f, 2f);
            ShellOffset = new Vec2(-5f, -3f);
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.9f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.25f;
            _weight = 5f;
            _kickForce = 1.5f;
            KforceDelta = 1.26f;
        }

        protected override string HintMessage => "silencer";

        public bool Silencer
        {
            get => _fireSound == GetPath("sounds/new/CZ-Silenced.wav");
            set
            {
                if (value)
                {
                    NonSkin %= 5;
                    NonSkin += 5;
                    _fireSound = GetPath("sounds/new/CZ-Silenced.wav");
                    _flare = new SpriteMap(GetPath("takezis"), 4, 4);
                    SetAmmoType<ATCZS>();
                    _barrelOffsetTL = new Vec2(41f, 3f);
                    loseAccuracy = 0.15f;
                    maxAccuracyLost = 0.35f;
                }
                else
                {
                    NonSkin %= 5;
                    _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
                    {
                        center = new Vec2(0.0f, 5f),
                    };
                    _fireSound = "deepMachineGun2";
                    SetAmmoType<ATCZ>();
                    _barrelOffsetTL = new Vec2(39f, 3f);
                    loseAccuracy = 0.15f;
                    maxAccuracyLost = 0.25f;
                }
            }
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 3, 4, 5, 7 });

        public override void Update()
        {
            if (Quacked())
            {
                SFX.Play(Silencer ? GetPath("sounds/silencer_off.wav") : GetPath("sounds/silencer_on.wav"));
                Silencer = !Silencer;
                UnQuack();
            }

            if (ammo > 26) NonSkin = 5 * (NonSkin / 5) + 0;
            else if (ammo > 20) NonSkin = 5 * (NonSkin / 5) + 1;
            else if (ammo > 12) NonSkin = 5 * (NonSkin / 5) + 2;
            else if (ammo > 5) NonSkin = 5 * (NonSkin / 5) + 3;
            else NonSkin = 5 * (NonSkin / 5) + 4;
            base.Update();
        }
    }
}

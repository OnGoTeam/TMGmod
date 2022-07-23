using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class ARwA : BaseAr, IHaveAllowedSkins, MagBuddy.ISupportReload
    {
        private readonly MagBuddy _magBuddy;
        private bool _onemoreclick = true;

        [UsedImplicitly] public byte Mags = 1;

        [UsedImplicitly] public StateBinding MagsBinding = new StateBinding(nameof(Mags));

        public ARwA(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 60;
            SetAmmoType<AT556NATO>(.85f);
            NonSkinFrames = 4;
            Smap = new SpriteMap(GetPath("ARW-A"), 27, 9);
            _center = new Vec2(13f, 5f);
            _collisionOffset = new Vec2(-13f, -5f);
            _collisionSize = new Vec2(27f, 9f);
            _barrelOffsetTL = new Vec2(27f, 2f);
            _holdOffset = new Vec2(-1f, 1f);
            ShellOffset = new Vec2(0f, -2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.85f;
            loseAccuracy = 0.11f;
            maxAccuracyLost = 0.2f;
            _editorName = "ARwA";
            _weight = 5f;
            _magBuddy = new MagBuddy(this, typeof(ArwaMag));
            _kickForce = 1f;
            KforceDelta = .2f;
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 500f;
            base.OnInitialize();
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        public bool SetMag()
        {
            if (Mags <= 0) return false;
            if (_wait > 1f) return false;
            if (_onemoreclick)
            {
                SFX.Play(GetPath("sounds/tuduc.wav"));
                NonSkin = 3;
                _wait += 5f;
                return _onemoreclick = false;
            }

            _onemoreclick = true;
            Mags -= 1;
            return true;
        }

        public bool DropMag(Thing mag)
        {
            SFX.Play(GetPath("sounds/tuduc.wav"));
            switch (NonSkin)
            {
                case 0:
                    NonSkin = 1;
                    break;
                case 3:
                    NonSkin = 2;
                    break;
                default:
                    NonSkin = 1;
                    break;
            }

            _wait += 7f;
            return true;
        }

        public Vec2 SpawnPos => new Vec2(0, -1);
        private int RealAmmo => ammo - 30 * Mags;

        public override void OnPressAction()
        {
            if (RealAmmo <= 0) _magBuddy.Doload();
            base.OnPressAction();
        }

        protected override void RealFire()
        {
            ammo -= 30 * Mags; // ammo = RealAmmo
            base.RealFire();
            ammo += 30 * Mags;
        }

        public override void Update()
        {
            if (RealAmmo <= 0) _magBuddy.Disload();
            if (RealAmmo <= 0 && Mags <= 0) NonSkin = 2;
            base.Update();
        }
    }
}

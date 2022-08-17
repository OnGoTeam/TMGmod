using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|LMG")]
    // ReSharper disable once InconsistentNaming
    public class Butcher : BaseLmg, IHaveAllowedSkins, MagBuddy<Butcher>.ISupportReload
    {
        private readonly MagBuddy<Butcher> _magBuddy;
        private float _debris = 1f;
        private bool _onemoreclick = true;
        [UsedImplicitly] public byte Mags = 2;

        [UsedImplicitly] public StateBinding MagsBinding = new StateBinding(nameof(Mags));

        public Butcher(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Solaris Butcher";
            ammo = (1 + Mags) * 60;
            SetAmmoType<ATButcher>();
            NonSkinFrames = 12;
            Smap = new SpriteMap(GetPath("Solaris Butcher"), 24, 12);
            _center = new Vec2(12f, 6f);
            _collisionOffset = new Vec2(-12f, -6f);
            _collisionSize = new Vec2(24f, 12f);
            _barrelOffsetTL = new Vec2(24f, 4f);
            _flare = new SpriteMap(GetPath("FlareMG44"), 13, 10)
            {
                center = new Vec2(1.0f, 6f),
            };
            _fireSound = GetPath("sounds/new/LMG-2.wav");
            _fullAuto = true;
            _fireWait = 0.2f;
            _kickForce = 0f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.1f;
            _holdOffset = new Vec2(0f, 1f);
            ShellOffset = new Vec2(3f, -1f);
            _weight = 4f;
            _magBuddy = new MagBuddy<Butcher>(this, typeof(ArwaMag));
            KickForce1Lmg = 0.33f;
            KickForce2Lmg = 0.67f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        public bool SetMag()
        {
            if (Mags <= 0) return false;
            if (_wait > _fireWait) return false;
            if (_onemoreclick)
            {
                SFX.Play(GetPath("sounds/tuduc.wav"));
                //NonSkin = 3;
                _wait += 5f;
                _onemoreclick = false;
                return false;
            }
            Mags -= 1;
            return true;
        }

        public bool DropMag(Thing mag)
        {
            SFX.Play(GetPath("sounds/tuduc.wav"));
            //switch (NonSkin)
            //{
            //    case 0:
            //        NonSkin += 1;
            //        break;
            //    case 3:
            //        NonSkin = 2;
            //        break;
            //    default:
            //        NonSkin = 1;
            //        break;
            //}

            _wait += 7f;
            _onemoreclick = true;
            return true;
        }

        public bool Loaded { get; set; } = true;
        public StateBinding MagLoadedBinding { get; } = new StateBinding(nameof(Loaded));
        public Vec2 SpawnPos => new Vec2(0, -1);

        public override void Update()
        {
            if (ammoType.barrelAngleDegrees > 5f) _debris = -1f;
            if (ammoType.barrelAngleDegrees < -5f) _debris = 1f;
            //if (RealAmmo <= 0 && Mags <= 0) NonSkin = 2;
            base.Update();
        }

        public override void Fire()
        {
            ammo -= 60 * Mags;
            base.Fire();
            ammo += 60 * Mags;
            ammoType.barrelAngleDegrees += _debris;
        }

        public override void OnReleaseAction()
        {
            base.OnReleaseAction();
            ammoType.barrelAngleDegrees = -5f;
        }
        private int RealAmmo => ammo - 60 * Mags;
        public override void OnPressAction()
        {
            if (RealAmmo <= 0)
            {
                if (Loaded)
                    _magBuddy.Disload();
                else
                    _magBuddy.Doload();
            }
            base.OnPressAction();
        }
    }
}

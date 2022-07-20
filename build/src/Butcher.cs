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
    public class Butcher : BaseLmg, IHaveAllowedSkins, MagBuddy.ISupportReload
    {
        private const int NonSkinFrames = 12;

        private readonly MagBuddy _magBuddy;
        private readonly SpriteMap _sprite;
        private float _debris = 1f;
        private bool _onemoreclick = true;
        [UsedImplicitly] public byte Mags = 2;

        [UsedImplicitly] public StateBinding MagsBinding = new StateBinding(nameof(Mags));

        public Butcher(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 60;
            _ammoType = new ATButcher();
            IntrinsicAccuracy = true;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Solaris Butcher"), 24, 12);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(12f, 6f);
            _collisionOffset = new Vec2(-12f, -6f);
            _collisionSize = new Vec2(24f, 12f);
            _barrelOffsetTL = new Vec2(24f, 4f);
            _flare = new SpriteMap(GetPath("FlareMG44"), 13, 10)
            {
                center = new Vec2(1.0f, 6f),
            };
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.2f;
            _kickForce = 0f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.1f;
            _holdOffset = new Vec2(0f, 1f);
            ShellOffset = new Vec2(3f, -1f);
            _editorName = "Solaris Butcher";
            _weight = 4f;
            _magBuddy = new MagBuddy(this, typeof(ArwaMag));
            KickForce1Lmg = 0.33f;
            KickForce2Lmg = 0.67f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        [UsedImplicitly] public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public bool SetMag()
        {
            if (Mags <= 0) return false;
            if (_wait > 1f) return false;
            if (_onemoreclick)
            {
                SFX.Play(GetPath("sounds/tuduc.wav"));
                //_sprite.frame = 3;
                _wait += 5f;
                return _onemoreclick = false;
            }

            _onemoreclick = true;
            ammo = 60;
            Mags -= 1;
            return true;
        }

        public bool DropMag(Thing mag)
        {
            SFX.Play(GetPath("sounds/tuduc.wav"));
            //switch (_sprite.frame)
            //{
            //    case 0:
            //        _sprite.frame += 1;
            //        break;
            //    case 3:
            //        _sprite.frame = 2;
            //        break;
            //    default:
            //        _sprite.frame = 1;
            //        break;
            //}

            _wait += 7f;
            return true;
        }

        public Vec2 SpawnPos => new Vec2(0, -1);

        public override void Update()
        {
            if (ammoType.barrelAngleDegrees > 5f) _debris = -1f;
            if (ammoType.barrelAngleDegrees < -5f) _debris = 1f;
            if (ammo <= 0) _magBuddy.Disload();
            //if (ammo <= 0 && Mags <= 0) _sprite.frame = 2;
            base.Update();
        }

        public override void Fire()
        {
            base.Fire();
            ammoType.barrelAngleDegrees += _debris;
        }

        public override void OnReleaseAction()
        {
            base.OnReleaseAction();
            ammoType.barrelAngleDegrees = -5f;
        }

        public override void OnPressAction()
        {
            if (ammo <= 0) _magBuddy.Doload();
            base.OnPressAction();
        }
    }
}

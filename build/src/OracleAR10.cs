#if FEATURES_1_2_X
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|DMR")]
    // ReSharper disable once InconsistentNaming
    public class OracleAR10 : BaseDmr, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public OracleAR10(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Oracle AR-10";
            ammo = 10;
            SetAmmoType<AT556NATO>(.91f);
            MinAccuracy = .35f;
            RegenAccuracyDmr = .015f;
            DrainAccuracyDmr = .3f;
            Smap = new SpriteMap(GetPath("Oracle AR-10"), 29, 12);
            _center = new Vec2(15f, 6f);
            _collisionOffset = new Vec2(-15f, -6f);
            _collisionSize = new Vec2(29f, 12f);
            _barrelOffsetTL = new Vec2(27f, 4f);
            _flare = new SpriteMap(GetPath("FlareTC12"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(-1f, 0f);
            ShellOffset = new Vec2(1f, -1f);
            _fireSound = GetPath("sounds/new/HeavyRifle2.wav");
            _fullAuto = false;
            _fireWait = .5f;
            _kickForce = 2f;
            loseAccuracy = .15f;
            maxAccuracyLost = .15f;
            laserSight = false;
            _laserOffsetTL = new Vec2(17f, 1.5f);
            _weight = 5f;
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 333f;
            base.OnInitialize();
        }

        public override void Update()
        {
            _kickForce = BipodsQ() ? 0f : 2f;
            MaxAccuracy = BipodsQ() ? 1f : 0.91f;
            MinAccuracy = BipodsQ() ? 1f : 0.35f;
            _ammoType.range = BipodsQ() ? 700f : 320f;
            _ammoType.bulletSpeed = BipodsQ() ? 69f : 37f;
            loseAccuracy = BipodsQ() ? 0 : 0.15f;
            maxAccuracyLost = BipodsQ() ? 0 : 0.15f;
            laserSight = BipodsQ();
            base.Update();
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
    }
}
#endif
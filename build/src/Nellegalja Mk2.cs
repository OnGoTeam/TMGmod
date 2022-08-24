using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [UsedImplicitly]
    [EditorGroup("TMG|Rifle|DMR")]
    // ReSharper disable once InconsistentNaming
    public class NellegaljaMk2 : BaseDmr, IHaveAllowedSkins
    {
        public NellegaljaMk2(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Nellegalja Mk.2";
            ammo = 20;
            SetAmmoType<ATSLK8>();
            MinAccuracy = 0.8f;
            RegenAccuracyDmr = 0.02f;
            DrainAccuracyDmr = 0.45f;
            Smap = new SpriteMap(GetPath("Nellegalja Mk2"), 33, 12);
            _center = new Vec2(17f, 6f);
            _collisionOffset = new Vec2(-17f, -6f);
            _collisionSize = new Vec2(33f, 12f);
            _barrelOffsetTL = new Vec2(33f, 4f);
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _holdOffset = new Vec2(1f, 1f);
            ShellOffset = new Vec2(-1f, -2f);
            _fireSound = GetPath("sounds/new/Nellegalja.wav");
            _fullAuto = false;
            _fireWait = 0.8f;
            _kickForce = 0.7f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _weight = 5f;
            laserSight = true;
            _laserOffsetTL = new Vec2(22f, 1.5f);
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 450f;
            base.OnInitialize();
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 4, 5, 8, 9 });
    }
}

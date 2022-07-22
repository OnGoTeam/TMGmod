using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|DMR")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class SLK8 : BaseDmr, IHaveAllowedSkins
    {
        public SLK8(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 14;
            _ammoType = new ATSLK8();
            SetAccuracyAsMax();
            MinAccuracy = 0.3f;
            RegenAccuracyDmr = 0.02f;
            DrainAccuracyDmr = 0.45f;
            Smap = new SpriteMap(GetPath("SLK8"), 41, 11);
            _center = new Vec2(21f, 6f);
            _collisionOffset = new Vec2(-21f, -6f);
            _collisionSize = new Vec2(41f, 11f);
            _barrelOffsetTL = new Vec2(41f, 5f);
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _holdOffset = new Vec2(5f, 0f);
            ShellOffset = new Vec2(-7f, 0f);
            _fireSound = GetPath("sounds/RifleOrMG.wav");
            _fullAuto = false;
            _fireWait = 2f;
            _kickForce = 3.5f;
            loseAccuracy = 0.22f;
            maxAccuracyLost = 0.22f;
            _editorName = "HK SLK8";
            _weight = 7.5f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
    }
}

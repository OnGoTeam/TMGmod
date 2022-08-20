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
    public class HK417 : BaseDmr, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public HK417(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Hk 417C";
            ammo = 11;
            SetAmmoType<ATHk417>();
            MinAccuracy = 0.75f;
            RegenAccuracyDmr = 0.006f;
            DrainAccuracyDmr = 0.1f;
            
            Smap = new SpriteMap(GetPath("Hk417"), 30, 10);
            SkinValue = 4;
            _center = new Vec2(15f, 5f);
            _collisionOffset = new Vec2(-15f, -5f);
            _collisionSize = new Vec2(30f, 10f);
            _barrelOffsetTL = new Vec2(30f, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(0f, 1f);
            ShellOffset = new Vec2(0f, -2f);
            _fireSound = GetPath("sounds/new/scar.wav");
            _fullAuto = false;
            _fireWait = 0.8f;
            _kickForce = 2.1f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.15f;
            _weight = 3.5f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 4, 1, 7 });
    }
}

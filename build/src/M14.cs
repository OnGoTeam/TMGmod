using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class M14 : BaseDmr, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public M14(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "M14";
            ammo = 10;
            SetAmmoType<ATM14>();
            MinAccuracy = 0.5f;
            RegenAccuracyDmr = 0.01f;
            DrainAccuracyDmr = 0.1f;
            Smap = new SpriteMap(GetPath("M14"), 46, 11);
            _center = new Vec2(23f, 6f);
            _collisionOffset = new Vec2(-23f, -6f);
            _collisionSize = new Vec2(46f, 11f);
            _barrelOffsetTL = new Vec2(46f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = GetPath("sounds/new/MarksmanRifle-WithBoltNoise.wav");
            _fullAuto = true;
            _fireWait = 1.25f;
            _kickForce = 3.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.25f;
            _holdOffset = new Vec2(6f, 1f);
            ShellOffset = new Vec2(-5f, -2f);
            _weight = 4.5f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2 });
    }
}

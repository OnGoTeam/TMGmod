using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class M4A1 : BaseAr, IHaveAllowedSkins
    {
        public M4A1(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new AT545NATO();
            MaxAccuracy = .86f;
            Smap = new SpriteMap(GetPath("M4A1"), 30, 11);
            _center = new Vec2(15f, 6f);
            _collisionOffset = new Vec2(-15f, -6f);
            _collisionSize = new Vec2(30f, 11f);
            _barrelOffsetTL = new Vec2(30f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.75f;
            loseAccuracy = 0.07f;
            maxAccuracyLost = 0.21f;
            _holdOffset = new Vec2(2f, 1f);
            ShellOffset = new Vec2(-2f, -2f);
            _editorName = "M4A1";
            _weight = 4f;
            _kickForce = .5f;
            KforceDelta = .5f;
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 330f;
            base.OnInitialize();
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
    }
}

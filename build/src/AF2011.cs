using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Semi-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class AF2011 : BaseGun, IAmHg, IHaveAllowedSkins, I5
    {
        public AF2011(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "AF-2011";
            ammo = 10;
            SetAmmoType<ATAF2011>();
            _numBulletsPerFire = 2;
            Smap = new SpriteMap(GetPath("AF2011"), 16, 9);
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-8f, -4f);
            _collisionSize = new Vec2(16f, 9f);
            _barrelOffsetTL = new Vec2(16f, 1f);
            _fireSound = "pistolFire";
            _fireWait = 0.6f;
            _kickForce = 1.7f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.4f;
            _holdOffset = new Vec2(-1f, 2f);
            ShellOffset = new Vec2(-4f, -2f);
            _weight = 2.5f;
            Compose(new LoseAccuracy(0.05f, 0.003f, 1f));
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 3, 5 });

        protected override void PopBaseShell()
        {
            base.PopBaseShell();
            base.PopBaseShell();
        }
    }
}

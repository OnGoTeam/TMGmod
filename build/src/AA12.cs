using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class AA12 : BaseGun, IAmSg, IHaveAllowedSkins
    {
        public AA12(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 12;
            _ammoType = new AT12Gauge
            {
                range = 125f,
                accuracy = 0.6f,
                bulletThickness = 1.5f,
            };
            MaxAccuracy = 0.6f;
            _numBulletsPerFire = 12;
            Smap = new SpriteMap(GetPath("AA12"), 34, 13);
            _center = new Vec2(17f, 7f);
            _collisionOffset = new Vec2(-17f, -7f);
            _collisionSize = new Vec2(34f, 13f);
            _barrelOffsetTL = new Vec2(34f, 4f);
            _fireSound = "shotgunFire";
            _fullAuto = true;
            _fireWait = 1.2f;
            _kickForce = 6f;
            loseAccuracy = 0.35f;
            maxAccuracyLost = 0.8f;
            _holdOffset = new Vec2(1f, 1f);
            ShellOffset = new Vec2(-14f, -5f);
            _editorName = "AA-12";
            _weight = 7f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 7, 8, 9 });

        public override void Fire()
        {
            if (duck?.sliding == true) _accuracyLost = 0;
            base.Fire();
            if (duck == null) return;
            if (duck.ragdoll != null) return;
            if (!duck.sliding) return;
            if (!duck.grounded) return;
            duck.vSpeed = 0f;
        }
    }
}

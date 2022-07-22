using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.Bullets;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [UsedImplicitly]
    [EditorGroup("TMG|Handgun|Semi-Automatic")]
    public class BigShot : BaseGun, IAmHg, IHaveAllowedSkins
    {
        public BigShot(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 7;
            _ammoType = new AT50C();
            MaxAccuracy = 1f;
            MinAccuracy = 0.6f;
            Smap = new SpriteMap(GetPath("50AEPistol"), 26, 10);
            SkinValue = 1;
            _center = new Vec2(13f, 5f);
            _collisionOffset = new Vec2(-13f, -5f);
            _collisionSize = new Vec2(26f, 10f);
            _barrelOffsetTL = new Vec2(26f, 2f);
            _fireSound = "magnum";
            _fullAuto = false;
            _fireWait = 1.2f;
            _kickForce = 2.8f;
            loseAccuracy = 0.25f;
            maxAccuracyLost = 0.8f;
            _holdOffset = new Vec2(0f, 2f);
            _editorName = "50AE Pistol";
            _weight = 2.5f;
            Compose(new LoseAccuracy(0.1f, 0.02f, 1f));
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 1, 2, 5, 7 });
    }
}

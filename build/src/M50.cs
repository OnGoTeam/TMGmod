using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Semi-Automatic")]
    [UsedImplicitly]
    public class M50 : BaseGun, IAmSr, IHaveAllowedSkins
    {
        public M50(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "M50";
            ammo = 7;
            SetAmmoType<Cal50Explode>();
            Smap = new SpriteMap(GetPath("M50"), 40, 13);
            _center = new Vec2(20f, 7f);
            _collisionOffset = new Vec2(-20f, -7f);
            _collisionSize = new Vec2(40f, 13f);
            _barrelOffsetTL = new Vec2(39f, 5.5f);
            _flare = FrameUtils.FlareOnePixel3();
            _fireSound = GetPath("sounds/new/M50v2.wav");
            _fullAuto = false;
            _fireWait = 3.75f;
            _kickForce = 8f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(5f, 0f);
            ShellOffset = new Vec2(0f, 0f);
            laserSight = true;
            _laserOffsetTL = new Vec2(30f, 8.5f);
            _weight = 6.75f;
            Compose(new SpeedAccuracy(this, 1f, 1f, 1f));
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        public override void Update()
        {
            _kickForce = HandleQ() ? 1f : 8f;
            loseAccuracy = HandleQ() ? 0f : 0.1f;
            maxAccuracyLost = HandleQ() ? 0f : 0.3f;
            base.Update();
        }
    }

#if DEBUG
    [EditorGroup("TMG|DEBUG")]
    [UsedImplicitly]
    public class Super50: M50
    {
        public Super50(float xval, float yval) : base(xval, yval)
        {
            ammo = 35;
            material = new MaterialGlitch(this);
            ComposeSimpleBurst(5, 1f);
        }
    }
#endif
}

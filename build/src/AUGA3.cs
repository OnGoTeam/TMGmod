using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class AUGA3 : BaseAr, IHaveAllowedSkins
    {
        [UsedImplicitly] public StateBinding HandAngleOffBinding = new StateBinding(nameof(HandAngleOff));

        public AUGA3(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "AUG A3";
            ammo = 30;
            SetAmmoType<ATAUGA3>();

            Smap = new SpriteMap(GetPath("AUGA3"), 30, 12);
            _center = new Vec2(15f, 6f);
            _collisionOffset = new Vec2(-15f, -6f);
            _collisionSize = new Vec2(30f, 12f);
            _barrelOffsetTL = new Vec2(30f, 4.5f);
            _flare = FrameUtils.FlareOnePixel2();
            _holdOffset = new Vec2(-2f, 1f);
            ShellOffset = new Vec2(-10f, -1f);
            _fireSound = GetPath("sounds/new/AutomaticRifle-2.wav");
            _fullAuto = true;
            _fireWait = 0.8f;
            loseAccuracy = 0.08f;
            maxAccuracyLost = 0.2f;
            _weight = 5f;
            _kickForce = .07f;
            laserSight = false;
            _laserOffsetTL = new Vec2(15f, 0.5f);
            KforceDelta = .63f;
        }

        public float HandAngleOff
        {
            get => handAngle * offDir;
            set => handAngle = value * offDir;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 8 });

        public override void Update()
        {
            laserSight = BipodsQ();
            base.Update();
        }
    }
}

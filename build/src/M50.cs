using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.BipodsLogic;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Semi-Automatic")]
    [UsedImplicitly]
    public class M50 : BaseGun, IAmSr, IHaveAllowedSkins, IHaveBipods
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
            _barrelOffsetTL = new Vec2(39f, 5f);
            _flare = new SpriteMap(GetPath("FlareOnePixel3"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
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

        public BitBuffer BipodsBuffer
        {
            get => this.GetBipodBuffer();
            set => this.SetBipodBuffer(value);
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        public bool Bipods
        {
            get => HandleQ();
            set
            {
                _kickForce = value ? 1f : 8f;
                loseAccuracy = value ? 0f : 0.1f;
                maxAccuracyLost = value ? 0f : 0.3f;
            }
        }

        public StateBinding BipodsBinding { get; } = new StateBinding(nameof(BipodsBuffer));
        public bool BipodsDisabled => false;
    }

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
}

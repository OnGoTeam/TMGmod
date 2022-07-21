using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.BipodsLogic;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.Modifiers.Firing;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Semi-Automatic")]
    [UsedImplicitly]
    public class M50 : BaseGun, IAmSr, IHaveAllowedSkins, IHaveBipods
    {
        private const int NonSkinFrames = 1;
        private readonly SpriteMap _sprite;

        public M50(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 7;
            _ammoType = new Cal50Explode
            {
                range = 1100f,
                accuracy = 1f,
                penetration = 1f,
                bulletThickness = 2.5f,
            };
            
            _sprite = new SpriteMap(GetPath("M50"), 40, 13);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(20f, 7f);
            _collisionOffset = new Vec2(-20f, -7f);
            _collisionSize = new Vec2(40f, 13f);
            _barrelOffsetTL = new Vec2(40f, 5f);
            _flare = new SpriteMap(GetPath("FlareOnePixel3"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = GetPath("sounds/HeavySniper.wav");
            _fullAuto = false;
            _fireWait = 3.75f;
            _kickForce = 8f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(6f, -1f);
            ShellOffset = new Vec2(0f, 0f);
            laserSight = true;
            _laserOffsetTL = new Vec2(31f, 9f);
            _editorName = "M50";
            _weight = 6.75f;
            Compose(new SpeedAccuracy(this, 1f, 1f, 1f));
        }

        public BitBuffer BipodsBuffer
        {
            get => this.GetBipodBuffer();
            set => this.SetBipodBuffer(value);
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

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
            Compose(new Burst(this, true, 5, 1f,  null));
        }
    }
}

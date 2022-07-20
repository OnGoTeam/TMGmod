using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Semi-Automatic")]
    public class BersaMagnum : BaseGun, IAmHg, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        private readonly SpriteMap _sprite;

        public BersaMagnum(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 7;
            _ammoType = new ATBersaMagnum();
            _type = "gun";
            _sprite = new SpriteMap(GetPath("BersaMagnum"), 13, 8);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(7f, 4f);
            _collisionOffset = new Vec2(-7f, -4f);
            _collisionSize = new Vec2(13f, 8f);
            _barrelOffsetTL = new Vec2(13f, 1.5f);
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _fireSound = "magnum";
            _fullAuto = false;
            _fireWait = 1.2f;
            _kickForce = 2.1f;
            loseAccuracy = 0.3f;
            maxAccuracyLost = 0.7f;
            _holdOffset = new Vec2(0f, 2f);
            ShellOffset = new Vec2(0f, 0f);
            _editorName = "Bersa Magnum";
            _laserOffsetTL = new Vec2(12f, 0f);
            laserSight = true;
            _weight = 2.4f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}

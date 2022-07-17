using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    public class Type89 : BaseAr, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 7 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public Type89(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 26;
            _ammoType = new ATType89();
            IntrinsicAccuracy = true;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Type 89"), 30, 12);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(15f, 6f);
            _collisionOffset = new Vec2(-15f, -6f);
            _collisionSize = new Vec2(30f, 12f);
            _barrelOffsetTL = new Vec2(30f, 4f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.71f;
            _kickForce = 1.1f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.25f;
            _holdOffset = new Vec2(1f, 0f);
            ShellOffset = new Vec2(-1f, -1f);
            _editorName = "Type 89";
            _weight = 4.6f;
            KickForceFastAr = 1.5f;
            KickForceSlowAr = 1.1f;
        }

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
    }
}

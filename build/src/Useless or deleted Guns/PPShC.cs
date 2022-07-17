#if DEBUG
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod.Useless_or_deleted_Guns
{
    [BaggedProperty("isInDemo", true)]
    [EditorGroup("TMG|DEBUG")]

    // ReSharper disable once InconsistentNaming
    public class PPShC : BaseGun, IHaveAllowedSkins, IAmSmg, I5
    {
        private const int NonSkinFrames = 1;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 5, 6, 7 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public PPShC(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 35;
            _ammoType = new AT9mmParabellum
            {
                range = 300f,
                accuracy = 0.9f,
                penetration = 0.4f,
            };
            MaxAccuracy = 0.9f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("PPShC"), 48, 16);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(23f, 5.5f);
            _collisionOffset = new Vec2(-23f, -4.5f);
            _collisionSize = new Vec2(46f, 11f);
            _barrelOffsetTL = new Vec2(47f, 4f);
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.25f;
            _kickForce = 0.5f;
            _holdOffset = new Vec2(7f, -1f);
            loseAccuracy = 0.05f;
            maxAccuracyLost = 0.2f;
            _editorName = "PPSh";
            _weight = 5.5f;
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
#endif

using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Modifiers;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class SVU : BaseDmr, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public SVU(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 12;
            _ammoType = new ATSVU();
            MaxAccuracy = _ammoType.accuracy;
            MinAccuracy = 0.2f;
            RegenAccuracyDmr = 0.017f;
            DrainAccuracyDmr = 0.2f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("SVU"), 37, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _flare = new SpriteMap(GetPath("FlareSilencer"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _center = new Vec2(18f, 6f);
            _collisionOffset = new Vec2(-18f, -6f);
            _collisionSize = new Vec2(37f, 11f);
            _barrelOffsetTL = new Vec2(37f, 5.5f);
            _fireSound = GetPath("sounds/Rifle.wav");
            _fullAuto = true;
            _fireWait = 1.2f;
            _kickForce = 2.8f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.1f;
            _holdOffset = new Vec2(0f, 0f);
            ShellOffset = new Vec2(-10f, 0f);
            _editorName = "SVU";
            _weight = 5.7f;
            BaseActiveModifier = ComposedModifier.Compose(
                DefaultModifier(),
                new SpeedAccuracy(this, 0f, 1f, 0f)
            );
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

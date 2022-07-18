using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class Foucus : BaseAr, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 1;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 7 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        [UsedImplicitly] public int Legacy;

        public Foucus(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 20;
            _ammoType = new ATFOUCUS();
            IntrinsicAccuracy = true;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Foucus"), 37, 13);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(19f, 7f);
            _collisionOffset = new Vec2(-19f, -7f);
            _collisionSize = new Vec2(37f, 13f);
            _barrelOffsetTL = new Vec2(37f, 3f);
            _flare = new SpriteMap(GetPath("FlareFoucus"), 13, 10)
            {
                center = new Vec2(3f, 5f),
            };
            _holdOffset = new Vec2(3f, 2f);
            ShellOffset = new Vec2(-3f, -2f);
            _fireSound = GetPath("sounds/Silenced1.wav");
            _fullAuto = true;
            _fireWait = 0.65f;
            KickForceSlowAr = 2f;
            KickForceFastAr = 3.5f;
            loseAccuracy = 0.275f;
            maxAccuracyLost = 0.275f;
            _editorName = "Foucus";
            _weight = 8f;
        }

        [UsedImplicitly] public StateBinding LegacyBinding { get; } = new StateBinding(nameof(Legacy));

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void Fire()
        {
            base.Fire();
            if (ammo < 1) return;
            if (duck == null) return;
            if (duck.ragdoll != null) return;
            if ((Legacy == 0) & (duck.vSpeed > -1f) & (duck.vSpeed < 1f)) duck.vSpeed += Rando.Float(-0.7f, -0.2f);
            else duck.vSpeed += Rando.Float(0.2f, 0.6f);
            Legacy = Rando.ChooseInt(0, 1);
        }
    }
}

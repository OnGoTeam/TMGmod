using DuckGame;
using JetBrains.Annotations;
using System.Collections.Generic;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Fully-Automatic")]
    public class Vintorez : BaseAr, ISpeedAccuracy, IHaveSkin, IHaveBipods
    {

        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        [UsedImplicitly]
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        public float RandomaticKickforce;
        [UsedImplicitly]
        public StateBinding RandomaticKickforceBinding { get; } = new StateBinding(nameof(RandomaticKickforce));
        // ReSharper disable once InconsistentNaming
        [UsedImplicitly]
        private readonly EditorProperty<int> skin;
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 1, 7 });
        public Vintorez(float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 16;
            _ammoType = new ATVintorez();
            MinAccuracy = 0f;
            BaseAccuracy = 0.9f;
            KickForceSlowAr = 0.4f;
            KickForceFastAr = 0.85f;
            MuAccuracySr = 1f;
            LambdaAccuracySr = 0.5f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Vintorez"), 33, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(16f, 6f);
            _collisionOffset = new Vec2(-16f, -6f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 5f);
            _holdOffset = new Vec2(2f, 0f);
            ShellOffset = new Vec2(0f, 0f);
            _fireSound = GetPath("sounds/Silenced1.wav");
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _fullAuto = true;
            _fireWait = 0.7f;
            _kickForce = 2.85f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.2f;
            _editorName = "Vintorez";
            _weight = 4.7f;
        }
        public override void Update()
        {
            base.Update();
            Bipods = Bipods;
            RandomaticKickforce = Rando.Float(1.1f, 1.7f);
        }
        [UsedImplicitly]
        public bool Bipods
        {
            get => HandleQ();
            set
            {
                _kickForce = value ? RandomaticKickforce : 2.85f;
                loseAccuracy = value ? 0f : 0.1f;
                maxAccuracyLost = value ? 0f : 0.2f;
                LambdaAccuracySr = value ? 0f : 0.5f;
            }
        }
        [UsedImplicitly]
        public BitBuffer BipodsBuffer
        {
            get
            {
                var b = new BitBuffer();
                b.Write(Bipods);
                return b;
            }
            set => Bipods = value.ReadBool();
        }
        public StateBinding BipodsBinding { get; } = new StateBinding(nameof(BipodsBuffer));
        public bool BipodsDisabled => false;

        [UsedImplicitly]
        public float MuAccuracySr { get; }
        public float LambdaAccuracySr { get; private set; }
        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic))
            {
                bublic = Rando.Int(0, 9);
            }
            _sprite.frame = bublic;
        }
        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}
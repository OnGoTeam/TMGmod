using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Modifiers.Kforce;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Combined")]
    // ReSharper disable once InconsistentNaming
    public class MP5 : BaseBurst, IFirstPrecise, IHaveAllowedSkins, IAmSmg
    {
        private const int NonSkinFrames = 2;
        public virtual ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 3, 4, 6, 7 });
        protected SpriteMap Texture;
        protected float IncreasedAccuracy;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        [UsedImplicitly] public StateBinding NonAutoBinding = new StateBinding(nameof(NonAuto));

        public MP5(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 30;
            _ammoType = new ATMP5();
            IncreasedAccuracy = .9f;
            LowerAccuracyFp = 0.7f;
            MaxAccuracy = LowerAccuracyFp;
            _type = "gun";
            Texture = new SpriteMap(GetPath("MP5"), 27, 12);
            _graphic = Texture;
            Texture.frame = 0;
            _center = new Vec2(13f, 6f);
            _collisionOffset = new Vec2(-13f, -6f);
            _collisionSize = new Vec2(27f, 12f);
            _barrelOffsetTL = new Vec2(27f, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = "deepMachineGun";
            _fullAuto = false;
            _fireWait = 0.5f;
            _kickForce = 0.5f;
            _holdOffset = new Vec2(-1f, 2f);
            ShellOffset = new Vec2(2f, -4f);
            _editorName = "MP5A3";
            _weight = 3f;
            MaxDelayFp = 10;
            DeltaWait = 0.45f;
            BurstNum = 1;
            Compose(new FirstKforce(20, kforce => kforce + 1.2f));
        }

        [UsedImplicitly]
        public bool NonAuto
        {
            get => BurstNum == 1;
            set
            {
                BurstNum = value ? 1 : 3;
                _fireWait = value ? 0.5f : 1.8f;
                FrameId = FrameId % 10 + (value ? 0 : 10);
                MaxAccuracy = value ? LowerAccuracyFp : IncreasedAccuracy;
            }
        }

        public int CurrentDelayFp { get; set; }
        public int MaxDelayFp { get; }
        public float LowerAccuracyFp { get; protected set; }
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        [UsedImplicitly]
        public int FrameId
        {
            get => Texture.frame;
            set => Texture.frame = value % (10 * NonSkinFrames);
        }

        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                NonAuto = !NonAuto;
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }

            base.Update();
        }
    }
}

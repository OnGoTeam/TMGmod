using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.Modifiers.Firing;
using TMGmod.Core.Modifiers.Kforce;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Combined")]
    // ReSharper disable once InconsistentNaming
    public class MP5 : BaseGun, IHaveAllowedSkins, IAmSmg
    {
        private const int NonSkinFrames = 2;
        protected float IncreasedAccuracy;

        protected SpriteMap Texture;

        public MP5(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 30;
            _ammoType = new ATMP5();
            IncreasedAccuracy = .9f;
            DecreasedAccuracy = 0.7f;
            MaxAccuracy = DecreasedAccuracy;
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
            Compose(
                new FirstKforce(20, kforce => kforce + 1.2f),
                new FirstAccuracy(10, accuracy => DecreasedAccuracy),
                new Burst(
                    this,
                    false,
                    burst =>
                    {
                        _fireWait = burst ? 1.8f : 0.5f;
                        FrameId = FrameId % 10 + (burst ? 10 : 0);
                        MaxAccuracy = burst ? IncreasedAccuracy : DecreasedAccuracy;
                    }
                )
                {
                    Num = 3,
                    Wait = .45f,
                    SwitchOnQuack = true,
                }
            );
        }

        protected float DecreasedAccuracy { get; set; }
        public virtual ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 3, 4, 6, 7 });
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        [UsedImplicitly]
        public int FrameId
        {
            get => Texture.frame;
            set => Texture.frame = value % (10 * NonSkinFrames);
        }
    }
}

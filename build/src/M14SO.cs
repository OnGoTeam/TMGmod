#if FEATURES_1_2_X
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class M14SO : BaseDmr, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public M14SO(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "M14 Sawed-Off";
            ammo = 18;
            SetAmmoType<ATM14>(.9f);
            MinAccuracy = 0.3f;
            RegenAccuracyDmr = 0.01f;
            DrainAccuracyDmr = 0.12f;
            NonSkinFrames = 3;
            Smap = new SpriteMap(GetPath("Sawed-Off M14"), 31, 11);
            _center = new Vec2(16f, 6f);
            _collisionOffset = new Vec2(-16f, -6f);
            _collisionSize = new Vec2(31f, 11f);
            _barrelOffsetTL = new Vec2(31f, 3.5f);
            _flare = FrameUtils.FlareOnePixel2();
            _fireSound = GetPath("sounds/new/MarksmanRifle-WithBoltNoise.wav");
            _fullAuto = true;
            _fireWait = 1.25f;
            _kickForce = 2.5f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            _holdOffset = new Vec2(-1f, 1f);
            ShellOffset = new Vec2(1f, -2f);
            _weight = 2.5f;
            Compose(
                new WithStock(
                    this,
                    true,
                    GetPath("sounds/tuduc"),
                    GetPath("sounds/tuduc"),
                    1f / 17f,
                    state =>
                    {
                        _fireWait = state.Deployed ? 1.25f : 1f;
                        loseAccuracy = state.Deployed ? 0f : 0.2f;
                        maxAccuracyLost = state.Deployed ? 0f : 0.25f;
                        _weight = state.Deployed ? 2.5f : 2f;
                        NonSkin = state.Deployed ? 0 : state.Folded ? 2 : 1;
                    }
                ).Switching()
            );
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 444f;
            base.OnInitialize();
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
    }
}
#endif

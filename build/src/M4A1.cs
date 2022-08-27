using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
#if DEBUG
using TMGmod.Core.Modifiers.Syncing;
using TMGmod.Core.Modifiers.Updating;
#endif
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class M4A1 : BaseAr, IHaveAllowedSkins
    {
        public M4A1(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "M4A1";
            ammo = 30;
            SetAmmoType<AT545NATO>(.86f);
            Smap = new SpriteMap(GetPath("M4A1"), 30, 11);
            _center = new Vec2(15f, 6f);
            _collisionOffset = new Vec2(-15f, -6f);
            _collisionSize = new Vec2(30f, 11f);
            _barrelOffsetTL = new Vec2(30f, 3.5f);
            _flare = FrameUtils.FlareOnePixel1();
            _fireSound = "deepMachineGun";
            _fullAuto = true;
            _fireWait = 0.75f;
            loseAccuracy = 0.07f;
            maxAccuracyLost = 0.21f;
            _holdOffset = new Vec2(2f, 1f);
            ShellOffset = new Vec2(-3f, -2f);
            _weight = 4f;
            _kickForce = .5f;
            KforceDelta = .5f;
#if DEBUG
            var acceleratedProxy = new ValueProxy<bool>(false);
            var acceleratedProperty = new SynchronizedProperty<bool>(
                () => acceleratedProxy.Value,
                (old, value) =>
                {
                    if (value != old)
                        SFX.Play(GetPath("sounds/tuduc.wav"), pitch: value ? .1f : -.1f);
                    _fireWait = value ? .55f : .75f;
                    acceleratedProxy.Value = value;
                }
                );
            var animating = Anime.Simple(
                frames => acceleratedProperty.Value = frames > 0,
                () => acceleratedProperty.Value = false
            );
            Compose(
                animating,
                new Combo(
                    this,
                    "faster fire (1)",
                    () => animating.AsInactive()?.Set(60),
                    "DOWN", "DOWN", "DOWN"
                ),
                new Combo(
                    this,
                    "faster fire",
                    () => animating.AsInactive()?.Set(60),
                    "UP", "UP", "UP"
                )
            );
#endif
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 330f;
            base.OnInitialize();
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 7 });
    }
}

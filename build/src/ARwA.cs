using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Firing;
using TMGmod.Core.Modifiers.Syncing;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class ARwA : BaseAr, IHaveAllowedSkins
    {
        private readonly SynchronizedValue<bool> _magInserted = new SynchronizedValue<bool>(true);
        [UsedImplicitly]
        public ARwA(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "ARwA";
            SetAmmoType<AT556NATO>(.85f);
            ammo = 60;
            NonSkinFrames = 4;
            Smap = new SpriteMap(GetPath("ARW-A"), 27, 9);
            _center = new Vec2(13f, 5f);
            _collisionOffset = new Vec2(-13f, -5f);
            _collisionSize = new Vec2(27f, 9f);
            _barrelOffsetTL = new Vec2(27f, 2.5f);
            _holdOffset = new Vec2(-1f, 1f);
            ShellOffset = new Vec2(1f, -2f);
            _flare = FrameUtils.FlareOnePixel1();
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.85f;
            loseAccuracy = 0.11f;
            maxAccuracyLost = 0.2f;
            _weight = 5f;
            _kickForce = 1f;
            KforceDelta = .2f;
            var magOffset = new Vec2(3f, 3f);
            Compose(
                _magInserted,
                new Reloading(
                    this,
                    30,
                    magOffset,
                    (
                        load, magsBefore
                    ) =>
                    {
                        loaded = false;
                        if (_magInserted.Value)
                        {
                            SFX.Play(GetPath("sounds/tuduc.wav"));
                            var magpos = Offset(magOffset);
                            Level.Add(
                                new ArwaMag(magpos.x, magpos.y) { graphic = { flipH = offDir < 0 } }
                            );
                            NonSkin = magsBefore > 0 ? 1 : 2;
                            _wait += 5f;
                            _magInserted.Value = false;
                            return;
                        }

                        load(
                            magsAfter =>
                            {
                                SFX.Play(GetPath("sounds/tuduc.wav"));
                                NonSkin = magsAfter > 0 ? 0 : 3;
                                _wait += _fireWait;
                                _magInserted.Value = true;
                            },
                            () => loaded = true
                        );
                    }
                )
            );
        }

        public override void OnReleaseAction()
        {
            if (_magInserted.Value)
                loaded = true;
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 300f;
            base.OnInitialize();
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        protected override float BaseKforce => NonSkin < 2 ? _kickForce : _kickForce * 1.5f;
    }
}

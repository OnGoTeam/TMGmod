using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Modifiers.Firing;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class ARwA : BaseAr, IHaveAllowedSkins
    {
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
            _barrelOffsetTL = new Vec2(27f, 2f);
            _holdOffset = new Vec2(-1f, 1f);
            ShellOffset = new Vec2(1f, -2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _fireSound = "deepMachineGun2";
            _fullAuto = true;
            _fireWait = 0.85f;
            loseAccuracy = 0.11f;
            maxAccuracyLost = 0.2f;
            _weight = 5f;
            _kickForce = 1f;
            KforceDelta = .2f;
            var magInserted = new SynchronizedValue<bool>(true);
            Compose(
                magInserted,
                new Reloading(
                    this,
                    30,
                    (
                        load, magsBefore
                    ) =>
                    {
                        loaded = false;
                        if (magInserted.Value)
                        {
                            SFX.Play(GetPath("sounds/tuduc.wav"));
                            var magpos = Offset(new Vec2(5f, 0f));
                            Level.Add(
                                new ArwaMag(magpos.x, magpos.y) { graphic = { flipH = offDir < 0 } }
                            );
                            NonSkin = magsBefore > 0 ? 1 : 2;
                            _wait += 5f;
                            magInserted.Value = false;
                            return;
                        }

                        load(
                            magsAfter =>
                            {
                                SFX.Play(GetPath("sounds/tuduc.wav"));
                                NonSkin = magsAfter > 0 ? 0 : 3;
                                _wait += _fireWait;
                                magInserted.Value = true;
                            },
                            () => loaded = true
                        );
                    }
                )
            );
        }

        public override void OnReleaseAction()
        {
            loaded = true;
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 500f;
            base.OnInitialize();
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
    }
}

using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Modifiers.Firing;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Semi-Automatic")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class CZ75 : BaseGun, IAmHg, IHaveAllowedSkins
    {
        public CZ75(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "CZ-75";
            ammo = 24;
            SetAmmoType<ATCZ75>();
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("CZ75"), 12, 8);
            _center = new Vec2(6f, 4f);
            _collisionOffset = new Vec2(-6f, -4f);
            _collisionSize = new Vec2(12f, 8f);
            _barrelOffsetTL = new Vec2(12f, 1f);
            _flare = new SpriteMap(GetPath("FlareOnePixel0"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(-1f, 2f);
            ShellOffset = new Vec2(-3f, -1f);
            _fireSound = GetPath("sounds/new/SMG-1.wav");
            _fireWait = 0.75f;
            _kickForce = 0.9f;
            loseAccuracy = 0.3f;
            maxAccuracyLost = 0.5f;
            _weight = 1f;
            var ldd = new SynchronizedValue<bool>(true);
            Compose(
                ldd,
                new Reloading(
                    this,
                    12,
                    1,
                    (
                        load, _
                    ) =>
                    {
                        if (ldd.Value)
                        {
                            DoAmmoClick();
                            var magpos = Offset(new Vec2(-5f, 0f));
                            Level.Add(
                                new Czmag(magpos.x, magpos.y) { graphic = { flipH = offDir < 0 } }
                            );
                            _wait += 5f;
                            ldd.Value = false;
                            return;
                        }

                        load(
                            mags =>
                            {
                                DoAmmoClick();
                                if (mags <= 0)
                                    NonSkin = 1;
                                _wait += _fireWait;
                                ldd.Value = true;
                            }
                        );
                    }
                )
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 4, 5 });


        protected override float BaseKforce => NonSkin == 0 ? _kickForce : _kickForce * 216f;
    }
}

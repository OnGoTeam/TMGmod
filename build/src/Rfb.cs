using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Syncing;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    public class Rfb : BaseAr, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public Rfb(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "RFB";
            ammo = 20;
            SetAmmoType<ATRfb>(.9f);
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("RFB"), 33, 11);
            _center = new Vec2(17f, 5f);
            _collisionOffset = new Vec2(-17f, -5f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 3.5f);
            _flare = FrameUtils.FlareOnePixel2();
            _holdOffset = new Vec2(0f, 1f);
            ShellOffset = new Vec2(10f, -2f);
            _fireSound = GetPath("sounds/new/DaewooK1.wav");
            _fullAuto = false;
            _fireWait = 0.46f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _weight = 5.5f;
            _kickForce = 0.07f;
            KforceDelta = 0.63f;
            var fullautoProperty = new SynchronizedProperty<bool>(
                () => _fullAuto,
                (old, value) =>
                {
                    if (value != old)
                        SFX.Play(GetPath("sounds/tuduc.wav"));
                    _fullAuto = value;
                    NonSkin = value ? 1 : 0;
                    _fireWait = value ? .79f : .46f;
                    maxAccuracyLost = value ? .45f : .3f;
                    MaxAccuracy = value ? .8f : .9f;
                }
            );
            Compose(
                fullautoProperty,
                new Quacking(this, true, true, fullautoProperty.Flip, "full-auto", () => new Vec2(5.5f, -2.5f))
            );
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 380f;
            base.OnInitialize();
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 7 });
    }
}

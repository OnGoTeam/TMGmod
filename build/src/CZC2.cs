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
    [EditorGroup("TMG|Rifle|DMR")]
    // ReSharper disable once InconsistentNaming
    public class CZC2 : BaseAr, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public CZC2(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "CZ-C2 SAR";
            ammo = 23;
            SetAmmoType<ATCZ2>();
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("CZC2"), 41, 11);
            _center = new Vec2(21f, 6f);
            _collisionOffset = new Vec2(-21f, -6f);
            _collisionSize = new Vec2(41f, 11f);
            _barrelOffsetTL = new Vec2(37f, 3.5f);
            _flare = FrameUtils.FlareOnePixel1();
            _holdOffset = new Vec2(5f, 2f);
            ShellOffset = new Vec2(-5f, -3f);
            _fireSound = "deepMachineGun2";
            _fullAuto = false;
            _fireWait = 0.9f;
            loseAccuracy = 0.12f;
            maxAccuracyLost = 0.25f;
            _weight = 4.4f;
            _kickForce = 1.5f;
            KforceDelta = 1.6f;
            var silencerProperty = new SynchronizedProperty<bool>(
                () => _fireSound == GetPath("sounds/new/CZ-Silenced.wav"),
                (old, value) =>
                {
                    if (value != old)
                        FrameUtils.SwitchedSilencer(old);
                    NonSkin = value ? 1 : 0;
                    _fireSound = value ? GetPath("sounds/new/CZ-Silenced.wav") : "deepMachineGun2";
                    if (value)
                        SetAmmoType<ATCZS2>();
                    else
                        SetAmmoType<ATCZ2>();
                    loseAccuracy = value ? .15f : .1f;
                    maxAccuracyLost = value ? .28f : .3f;
                    _barrelOffsetTL = value ? new Vec2(41f, 3.5f) : new Vec2(37f, 3.5f);
                    _flare = value ? FrameUtils.TakeZis() : FrameUtils.FlareOnePixel1();
                }
            );
            Compose(
                silencerProperty,
                new Quacking(this, true, true, silencerProperty.Flip, "silencer", () => barrelOffset)
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
    }
}

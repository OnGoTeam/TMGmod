using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Fully-Automatic")]
    public class SpectreM4 : BaseSmg, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public SpectreM4(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Spectre M4";
            ammo = 30;
            SetAmmoType<ATSpectreM4>();
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("SpectreM4"), 19, 10);
            _center = new Vec2(10f, 5f);
            _collisionOffset = new Vec2(-10f, -5f);
            _collisionSize = new Vec2(19f, 10f);
            _barrelOffsetTL = new Vec2(13f, 2f);
            _fireSound = GetPath("sounds/new/LightCaliber-Pistol.wav");
            _flare = FrameUtils.SmallFlare();
            _fullAuto = true;
            _fireWait = 0.31f;
            _kickForce = 0.8f;
            KforceDelta = 2.7f;
            KforceDelay = 50;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.34f;
            _holdOffset = new Vec2(3f, 3f);
            ShellOffset = new Vec2(-3f, -3f);
            _weight = 3.3f;
            ComposeSilencer(
                () => _fireSound == GetPath("sounds/SilencedPistol.wav"),
                value =>
                {
                    NonSkin = value ? 1 : 0;
                    if (value)
                        SetAmmoType<ATSpectreM4S>();
                    else
                        SetAmmoType<ATSpectreM4>();
                    _barrelOffsetTL = value ? new Vec2(16f, 2f) : new Vec2(13f, 2f);
                    loseAccuracy = value ? .07f : .1f;
                    maxAccuracyLost = value ? .3f : .34f;
                    _weight = value ? 3.8f : 3.3f;
                    _fireSound = value
                        ? GetPath("sounds/SilencedPistol.wav")
                        : GetPath("sounds/new/LightCaliber-Pistol.wav");
                    _flare = value ? FrameUtils.TakeZis() : FrameUtils.SmallFlare();
                }
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 6 });
    }
}

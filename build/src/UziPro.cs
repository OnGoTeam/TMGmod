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
    [EditorGroup("TMG|SMG|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class UziPro : BaseSmg, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public UziPro(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Uzi Pro";
            ammo = 24;
            SetAmmoType<ATUzi>();
            KforceDelay = 25;
            KforceDelta = 4f;
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("UziProS"), 16, 10);
            _center = new Vec2(8f, 5f);
            _collisionOffset = new Vec2(-8f, -5f);
            _collisionSize = new Vec2(16f, 10f);
            _barrelOffsetTL = new Vec2(10f, 2.5f);
            _flare = FrameUtils.FlareOnePixel0();
            _fireSound = GetPath("sounds/new/SMG-1.wav");
            _fullAuto = true;
            _fireWait = 0.4f;
            _kickForce = 0.5f;
            loseAccuracy = 0.08f;
            maxAccuracyLost = 0.5f;
            _holdOffset = new Vec2(1f, 1f);
            ShellOffset = new Vec2(-4f, -2f);
            laserSight = true;
            _laserOffsetTL = new Vec2(8f, 5.5f);
            _weight = 2.5f;
            var silencerProperty = new SynchronizedProperty<bool>(
                () => _fireSound == GetPath("sounds/new/SMG-Silenced.wav"),
                (old, value) =>
                {
                    if (value != old)
                        FrameUtils.SwitchedSilencer(old);
                    NonSkin = value ? 1 : 0;
                    if (value)
                        SetAmmoType<ATUziS>();
                    else
                        SetAmmoType<ATUzi>();
                    _barrelOffsetTL = value ? new Vec2(16f, 2.5f) : new Vec2(10f, 2.5f);
                    _flare = value ? FrameUtils.TakeZis() : FrameUtils.FlareOnePixel0();
                    _fireSound = value ? GetPath("sounds/new/SMG-Silenced.wav") : GetPath("sounds/new/UziPro.wav");
                }
            );
            Compose(
                silencerProperty,
                new Quacking(this, true, true, silencerProperty.Flip, "silencer", () => barrelOffset)
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 2, 4, 6, 9 });
    }
}

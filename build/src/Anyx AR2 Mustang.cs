﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Kforce;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Burst")]
    // ReSharper disable once InconsistentNaming
    public class AN94C : BaseGun, IAmAr, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public AN94C(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Anyx AR2 Mustang";
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("Anyx AR2 Mustang"), 33, 10);
            _center = new Vec2(16f, 5f);
            _collisionOffset = new Vec2(-15f, -5f);
            _collisionSize = new Vec2(33f, 10f);
            _barrelOffsetTL = new Vec2(28, 2.5f);
            _flare = FrameUtils.FlareOnePixel1();
            _holdOffset = new Vec2(3f, 2f);
            ShellOffset = new Vec2(-4f, -2f);
            ammo = 30;
            SetAmmoType<ATCZ>();
            _fireSound = GetPath("sounds/new/HighCaliber-LessImpact.wav");
            _fullAuto = false;
            _fireWait = 2f;
            _kickForce = 1.2f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.25f;
            _weight = 4f;
            ComposeSimpleBurst(2, .4f);
            Compose(new HSpeedKforce(this, hspeed => hspeed > .1f, kforce => kforce + .83f));
            ComposeSilencer(
                () => _fireSound == GetPath("sounds/new/HighCaliber-LessImpact-Silenced.wav"),
                value =>
                {
                    _fireSound = value
                        ? GetPath("sounds/new/HighCaliber-LessImpact-Silenced.wav")
                        : GetPath("sounds/new/HighCaliber-LessImpact.wav");
                    _flare = value ? FrameUtils.TakeZis() : FrameUtils.FlareOnePixel1();
                    if (value)
                        SetAmmoType<ATCZS>();
                    else
                        SetAmmoType<ATCZ>();
                    _barrelOffsetTL = value ? new Vec2(33f, 2f) : new Vec2(28f, 2f);
                    NonSkin = value ? 1 : 0;
                }
            );
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 260f;
            _ammoType.bulletSpeed = 60f;
            base.OnInitialize();
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 4, 7 });
    }
}

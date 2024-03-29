﻿using System.Collections.Generic;
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
    // ReSharper disable once InconsistentNaming
    public class AUGA1 : BaseAr, IHaveAllowedSkins, I5
    {
        [UsedImplicitly]
        public AUGA1(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "AUG A1";
            ammo = 42;
            SetAmmoType<ATAUGA1>();
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("AUGA1"), 30, 12);
            SkinValue = 8;
            _center = new Vec2(15f, 6f);
            _collisionOffset = new Vec2(-15f, -6f);
            _collisionSize = new Vec2(30f, 12f);
            _barrelOffsetTL = new Vec2(30f, 4.5f);
            _flare = FrameUtils.FlareOnePixel1();
            _holdOffset = new Vec2(-2f, 1f);
            ShellOffset = new Vec2(-10f, -1f);
            _fireSound = GetPath("sounds/new/AutomaticRifle-2.wav");
            _fullAuto = true;
            _fireWait = 0.8f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.2f;
            _weight = 5.5f;
            _kickForce = .07f;
            KforceDelta = .63f;
            var gripProperty = new SynchronizedProperty<bool>(
                () => maxAccuracyLost < 0.15f,
                (old, value) =>
                {
                    if (value != old)
                        SFX.Play(GetPath("sounds/tuduc.wav"));
                    NonSkin = value ? 1 : 0;
                    maxAccuracyLost = value ? .1f : .2f;
                    MaxAccuracy = value ? .97f : .80f;
                }
            );
            Compose(
                gripProperty,
                new Quacking(this, true, true, gripProperty.Flip, "foregrip", () => new Vec2(6.5f, 0f))
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 2, 4, 5, 6, 8 });
    }
}

﻿using DuckGame;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Syncing;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [UsedImplicitly]
    [EditorGroup("TMG|SMG|Other")]
    // ReSharper disable once InconsistentNaming
    public class ANP73 : BaseGun, IAmHg, IHaveAllowedSkins
    {
        public ANP73(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Experimental ANP-73";
            ammo = 33;
            SetAmmoType<ATANP73>();
            NonSkinFrames = 4;
            Smap = new SpriteMap(GetPath("Experimental ANP-73"), 19, 14);
            _flare = FrameUtils.TakeZis();
            _center = new Vec2(10f, 7f);
            _collisionOffset = new Vec2(-10f, -7f);
            _collisionSize = new Vec2(19f, 14f);
            _barrelOffsetTL = new Vec2(19f, 3f);
            _holdOffset = new Vec2(2f, 3f);
            ShellOffset = new Vec2(-1f, -3f);
            _fireSound = GetPath("sounds/2.wav");
            _fullAuto = true;
            _fireWait = 1.5f;
            _kickForce = 1.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.4f;
            _weight = 2f;
            var modeProperty = new SynchronizedProperty<int>(
                () => NonSkin % 4,
                (old, value) =>
                {
                    if (value != old)
                        SFX.Play(GetPath("sounds/tuduc.wav"));
                    NonSkin = value;
                    _fireWait = new[] { 1.5f, 1.2f, .9f, .6f }[value];
                    loseAccuracy = new[] { .15f, .15f, .2f, .3f }[value];
                    maxAccuracyLost = new[] { .3f, .5f, .6f, .7f }[value];
                },
                value => value.Modulo(4)
            );
            Compose(
                modeProperty,
                new Quacking(this, true, true, modeProperty.Increment, "mode", () => new Vec2(-3.5f, -3.5f))
            );
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });
    }
}

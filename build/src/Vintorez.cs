﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Accuracy;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Sniper|Fully-Automatic")]
    public class Vintorez : BaseAr, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public Vintorez(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Vintorez";
            ammo = 16;
            SetAmmoType<ATVintorez>();
            MinAccuracy = 0f;
            _kickForce = 2.85f;
            KforceDelta = 0.45f;
            Smap = new SpriteMap(GetPath("Vintorez"), 33, 11);
            _center = new Vec2(16f, 6f);
            _collisionOffset = new Vec2(-16f, -6f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 5f);
            _holdOffset = new Vec2(2f, 0f);
            ShellOffset = new Vec2(0f, 0f);
            _fireSound = GetPath("sounds/new/HighCaliber-LessImpact-Silenced.wav");
            _flare = FrameUtils.TakeZis();
            _fullAuto = true;
            _fireWait = 0.7f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            _weight = 4.7f;
            Compose(new SpeedAccuracy(this, 1f, 1f, 0.5f));
        }

        protected override float BaseKforce => HandleQ() ? Rando.Float(0.5f, 1f) : 2.85f;

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 7 });
    }
}

﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    //[yee] switch
    [EditorGroup("TMG|SMG|Fully-Automatic")]
    [UsedImplicitly]
    // ReSharper disable once InconsistentNaming
    public class MP7 : BaseGun, IAmSmg, IHaveAllowedSkins
    {
        private float _handleAngleOff;

        [UsedImplicitly] public StateBinding HandAngleOffBinding = new(nameof(HandAngleOff));

        public MP7(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "HK MP7";
            ammo = 35;
            SetAmmoType<ATMP7>();
            Smap = new SpriteMap(GetPath("MP7"), 20, 10);
            _flare = FrameUtils.TakeZis();
            _center = new Vec2(12f, 4f);
            _collisionOffset = new Vec2(-12f, -4f);
            _collisionSize = new Vec2(20f, 10f);
            _barrelOffsetTL = new Vec2(20f, 3f);
            _fireSound = GetPath("sounds/new/SMG-Silenced.wav");
            _fullAuto = true;
            _fireWait = 0.5f;
            _kickForce = 1.5f;
            _holdOffset = new Vec2(3f, 1f);
            ShellOffset = new Vec2(-7f, -1f);
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.5f;
            _weight = 3f;
        }

        [UsedImplicitly]
        public float HandAngleOff
        {
            get => handAngle * offDir;
            set => handAngle = value * offDir;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 7 });

        public override void Update()
        {
            Hint("change angle", () => barrelOffset, "QUACK", "UP");
            HandAngleOff = _handleAngleOff;
            base.Update();
            if (duck is null)
            {
                _handleAngleOff = 0f;
                return;
            }

            if (duck?.inputProfile.Down("UP") == true && !_raised)
            {
                if (_handleAngleOff > -0.7f) _handleAngleOff -= 0.05f;

                return;
            }

            if (duck?.inputProfile.Down("QUACK") == true && !_raised && !duck.sliding)
            {
                if (_handleAngleOff < 0.7f) _handleAngleOff += 0.05f;

                return;
            }

            switch (_handleAngleOff)
            {
                case > 0f:
                    _handleAngleOff -= 0.1f;
                    break;
                case < 0f:
                    _handleAngleOff += 0.1f;
                    break;
            }
            if ((_handleAngleOff > -0.1f) & (_handleAngleOff < 0.1f)) _handleAngleOff = 0f;
        }
    }
}

﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.Modifiers.Kforce;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod.Custom_Guns
{
    [EditorGroup("TMG|Rifle|Burst|Custom")]
    // ReSharper disable once InconsistentNaming
    public class AN94C : BaseBurst, IAmAr, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 2;

        // ReSharper disable once MemberCanBePrivate.Global
        private readonly SpriteMap _sprite;

        [UsedImplicitly] public StateBinding StockBinding = new StateBinding(nameof(Laserrod));

        public AN94C(float xval, float yval)
            : base(xval, yval)
        {
            _sprite = new SpriteMap(GetPath("AN94C"), 33, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(16f, 5f);
            _collisionOffset = new Vec2(-15f, -5f);
            _collisionSize = new Vec2(33f, 9f);
            _barrelOffsetTL = new Vec2(33f, 2f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(3f, 2f);
            ShellOffset = new Vec2(0f, -3f);
            ammo = 30;
            _ammoType = new AT545NATO { range = 260f, bulletSpeed = 60f, accuracy = 0.87f };
            _fireSound = "deepMachineGun2";
            _fullAuto = false;
            _fireWait = 2f;
            _kickForce = 0.07f;
            loseAccuracy = 0.15f;
            maxAccuracyLost = 0.1f;
            _editorName = "AN94 with Wooden Stock";
            _weight = 5.5f;
            laserSight = false;
            _laserOffsetTL = new Vec2(30f, 2.5f);
            DeltaWait = 0.07f;
            BurstNum = 2;
            Compose(new HSpeedKforce(this, hspeed => hspeed > .1f, kforce => kforce + .83f));
        }

        [UsedImplicitly]
        public bool Laserrod
        {
            get => _sprite.frame < 10;
            set
            {
                if (value)
                {
                    loseAccuracy = 0.15f;
                    _fireWait = 1.5f;
                    maxAccuracyLost = 0.45f;
                    _sprite.frame %= 10;
                    laserSight = false;
                }
                else
                {
                    loseAccuracy = 0.1f;
                    _fireWait = 2.5f;
                    maxAccuracyLost = 0.1f;
                    _sprite.frame %= 10;
                    _sprite.frame += 10;
                    laserSight = true;
                }
            }
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 6, 7 });

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));


        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                Laserrod = !Laserrod;
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }

            base.Update();
        }
    }
}

﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.AmmoTypes;
using TMGmod.Core;
using TMGmod.Core.Modifiers.Updating;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class AKALFA : BaseAr, IHaveAllowedSkins
    {
        [UsedImplicitly]
        public AKALFA(float xval, float yval)
            : base(xval, yval)
        {
            _editorName = "Alfa";
            ammo = 20;
            SetAmmoType<AT545NATO>();
            NonSkinFrames = 3;
            Smap = new SpriteMap(GetPath("ALFA"), 38, 9);
            _center = new Vec2(19f, 5f);
            _collisionOffset = new Vec2(-19f, -5f);
            _collisionSize = new Vec2(38f, 9f);
            _barrelOffsetTL = new Vec2(38f, 2.5f);
            _holdOffset = new Vec2(4f, 1f);
            ShellOffset = new Vec2(-3f, -2f);
            _flare = FrameUtils.FlareOnePixel1();
            _fireSound = GetPath("sounds/new/ALFA.wav");
            _fullAuto = true;
            _fireWait = 0.75f;
            _kickForce = 0.05f;
            KforceDelta = 0.70f;
            loseAccuracy = 0f;
            maxAccuracyLost = 0.25f;
            _weight = 5.5f;
            Compose(
                new WithStock(
                    this,
                    true,
                    GetPath("sounds/tuduc"),
                    GetPath("sounds/tuduc"),
                    1f / 10f,
                    state =>
                    {
                        _kickForce = state.Deployed ? .65f : 1.2f;
                        loseAccuracy = state.Deployed ? 0f : 0.1f;
                        _weight = state.Deployed ? 5.5f : 3.5f;
                        NonSkin = state.Deployed ? 0 : state.Folded ? 2 : 1;
                    }
                ).Switching()
            );
        }

        protected override void OnInitialize()
        {
            _ammoType.range = 330f;
            _ammoType.bulletSpeed = 60f;
            _ammoType.bulletThickness = .87f;
            base.OnInitialize();
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 4, 5 });
    }
}

﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [UsedImplicitly]
    [EditorGroup("TMG|Shotgun|Break-Action")]
    public class Deadly44C : BaseGun, IAmSg, IHaveAllowedSkins
    {
        public Deadly44C(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 2;
            SetAmmoType<AT44DB>(.1f);
            _numBulletsPerFire = 44;
            NonSkinFrames = 2;
            Smap = new SpriteMap(GetPath("44dbTWICE"), 33, 10);
            _center = new Vec2(16.5f, 5f);
            _collisionOffset = new Vec2(-16.5f, -5f);
            _collisionSize = new Vec2(33f, 10f);
            _barrelOffsetTL = new Vec2(33f, 2f);
            _holdOffset = new Vec2(2f, 1f);
            ShellOffset = new Vec2(-6f, -7f);
            _fireSound = "shotgun";
            _manualLoad = true;
            _fireWait = 4f;
            _kickForce = 9f;
            loseAccuracy = 0.25f;
            maxAccuracyLost = 0.5f;
            _editorName = "You Scared Ded Twice";
            _weight = 4.25f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0 });

        public override void OnPressAction()
        {
            switch (loaded)
            {
                case false when ammo > 0:
                    Reload();
                    break;
                case true:
                    Fire();
                    break;
            }
        }

        public override void Reload(bool shell = true)
        {
            if (ammo > 1)
            {
                SFX.Play(GetPath("sounds/tuduc.wav"));
                FrameId = FrameId % 10 + 10;
            }

            base.Reload(ammo > 1);
        }
    }
}

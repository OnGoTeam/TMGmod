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
    [EditorGroup("TMG|Handgun|Semi-Automatic")]
    public class Bersa45 : BaseGun, IAmHg, IHaveAllowedSkins
    {
        public Bersa45(float xval, float yval)
            : base(xval, yval)
        {
            ammo = 11;
            SetAmmoType<ATBersa45>();
            
            Smap = new SpriteMap(GetPath("Bersa45"), 12, 8);
            _center = new Vec2(6f, 4f);
            _collisionOffset = new Vec2(-6f, -4f);
            _collisionSize = new Vec2(12f, 8f);
            _barrelOffsetTL = new Vec2(12f, 1.5f);
            _flare = new SpriteMap(GetPath("takezis"), 4, 4);
            _fireSound = GetPath("sounds/1.wav");
            _fullAuto = false;
            _fireWait = 0.3f;
            _kickForce = 1f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.9f;
            _holdOffset = new Vec2(-1f, 2f);
            ShellOffset = new Vec2(0f, 0f);
            _editorName = "Bersa 45";
            _laserOffsetTL = new Vec2(9f, 4f);
            laserSight = true;
            _weight = 1f;
        }

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 1, 3, 4, 6, 8 });
    }
}

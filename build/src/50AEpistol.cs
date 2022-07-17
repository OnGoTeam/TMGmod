﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Handgun|Semi-Automatic")]
    public class BigShot : BaseGun, IAmHg, IHaveAllowedSkins, ILoseAccuracy
    {
        private const int NonSkinFrames = 1;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 1, 2, 5, 7 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public BigShot(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(1, this, -1f, 9f, 0.5f);
            ammo = 7;
            _ammoType = new AT50C();
            MaxAccuracy = 1f;
            MinAccuracy = 0.6f;
            RegenAccuracyDmr = 0.05f;
            DrainAccuracyDmr = 0.1f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("50AEPistol"), 26, 10);
            _graphic = _sprite;
            _sprite.frame = 1;
            _center = new Vec2(13f, 5f);
            _collisionOffset = new Vec2(-13f, -5f);
            _collisionSize = new Vec2(26f, 10f);
            _barrelOffsetTL = new Vec2(26f, 2f);
            _fireSound = "magnum";
            _fullAuto = false;
            _fireWait = 1.2f;
            _kickForce = 2.8f;
            loseAccuracy = 0.25f;
            maxAccuracyLost = 0.8f;
            _holdOffset = new Vec2(0f, 2f);
            _editorName = "50AE Pistol";
            _weight = 2.5f;
        }

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public float RegenAccuracyDmr { get; }
        public float DrainAccuracyDmr { get; }
    }
}

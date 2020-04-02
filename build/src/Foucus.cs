﻿using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;
using TMGmod.Core.AmmoTypes;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class Foucus : BaseAr, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        private int Legacy = 0;
        public StateBinding LegacyBinding { get; } = new StateBinding(nameof(Legacy));
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 7 });

        public Foucus(float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 20;
            _ammoType = new AT556NATOS
            {
                range = 405f,
                accuracy = 0.85f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Foucus"), 37, 13);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(19f, 7f);
            _collisionOffset = new Vec2(-19f, -7f);
            _collisionSize = new Vec2(37f, 13f);
            _barrelOffsetTL = new Vec2(34f, 3f);
            _flare = new SpriteMap(GetPath("FlareFoucus"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _holdOffset = new Vec2(3f, 2f);
            ShellOffset = new Vec2(-3f, -2f);
            _fireSound = GetPath("sounds/Silenced1.wav");
            _fullAuto = true;
            _fireWait = 0.7f;
            Kforce1Ar = 2f;
            Kforce2Ar = 5f;
            loseAccuracy = 0.275f;
            maxAccuracyLost = 0.275f;
            _editorName = "Foucus";
			_weight = 8f;
        }
        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic))
            {
                bublic = Rando.Int(0, 9);
            }
            _sprite.frame = bublic;
        }

        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }
        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
        public override void Fire()
        {
            base.Fire();
            if (ammo < 1) return;
            if (duck == null) return;
            if (duck.ragdoll != null) return;
            if ((Legacy == 0) & (duck.vSpeed > -1f) & (duck.vSpeed < 1f)) duck.vSpeed += Rando.Float(-0.7f, -0.2f);
            else duck.vSpeed += Rando.Float(0.2f, 0.6f);
            Legacy = Rando.ChooseInt(0, 1);
        }
    }
}
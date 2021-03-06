﻿#if DEBUG
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|DMR")]
    // ReSharper disable once InconsistentNaming
    public class DR300 : BaseGun, IAmAr, IHaveSkin
    {
        private int _postrounds = Rando.ChooseInt(20, 30);
        private const int Postframe = 8;
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 3;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once ConvertToAutoProperty
        [UsedImplicitly]
        public EditorProperty<int> Rounds { get; }

        private static readonly List<int> Allowedlst = new List<int>(new[] { 3, 8 });
        public DR300(float xval, float yval)
          : base(xval, yval)
        {
            Rounds = new EditorProperty<int>(0, this, 0, 2, 1);
            skin = new EditorProperty<int>(8, this, -1f, 9f, 0.5f);
            ammo = _postrounds;
            _ammoType = new AT9mm
            {
                range = 667f,
                accuracy = 0.98f,
                penetration = 1f,
                bulletSpeed = 44f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("deleteco/Future/DR300.png"), 37, 11);
            _graphic = _sprite;
            _sprite.frame = Postframe;
            _center = new Vec2(18f, 6f);
            _collisionOffset = new Vec2(-18f, -6f);
            _collisionSize = new Vec2(37f, 11f);
            _barrelOffsetTL = new Vec2(37f, 2f);
            _fireSound = GetPath("sounds/1.wav");
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _fullAuto = false;
            _fireWait = 0.3f;
            _kickForce = 3.7f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _holdOffset = new Vec2(2f, 3f);
            ShellOffset = new Vec2(0f, 0f);
            _editorName = "DR-300";
            laserSight = false;
			_weight = 3.5f;
        }
        public override void Update()
        {
            if ((_postrounds == 20) & !((_sprite.frame > 9) & (_sprite.frame < 20))) _sprite.frame = 10 + _sprite.frame % 10;
            if ((_postrounds == 30) & (_sprite.frame < 19)) _sprite.frame = 20 + _sprite.frame % 10;
            base.Update();
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
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (Rounds.value)
            {
                case 0:
                    _postrounds = Rando.ChooseInt(20, 30);
                    break;
                case 1:
                    _postrounds = 20;
                    break;
                case 2:
                    _postrounds = 30;
                    break;
            }
            ammo = _postrounds;
            UpdateSkin();
            _sprite.frame = Rounds.value * 10 + _sprite.frame % 10;
            base.EditorPropertyChanged(property);
        }
    }
}
#endif
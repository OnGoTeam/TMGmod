using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.WClasses;
using TMGmod.Core;
using TMGmod.Core.AmmoTypes;

namespace TMGmod
{
    [BaggedProperty("isInDemo", true), EditorGroup("TMG|Handgun|Fully-Automatic")]
    public class HazeS : BaseGun, IAmHg, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 7 });
        private float _heatval;
        [UsedImplicitly]
        public float Heatval
        {
            get => _heatval;
            set
            {
                _heatval = value;
                _ammoType.accuracy = _heatval > 3f ? 1.28f - _heatval * 0.16f : 0.8f;
                _ammoType.bulletSpeed = 60f + 10f * _heatval;
                _ammoType.range = 180f;
                Sighted = _sighted;
            }
        }
        [UsedImplicitly]
        public StateBinding HeatvalBinding = new StateBinding(nameof(Heatval));

        private bool _sighted;

        [UsedImplicitly]
        public bool Sighted
        {
            get => _sighted;
            set
            {
                _sighted = value;
                if (!value) return;
                //else
                _ammoType.accuracy = 1f;
                _ammoType.range = 450f;
            }
        }

        public HazeS(float xval, float yval) :
            base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 26;
            _ammoType = new AT9mmS
            {
                accuracy = 0.9f,
                range = 150f,
                combustable = true,
                bulletSpeed = 60f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("HazeS"), 24, 12);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(12f, 3f);
            _collisionOffset = new Vec2(-12f, -3f);
            _collisionSize = new Vec2(24f, 12f);
            _barrelOffsetTL = new Vec2(24f, 2f);
            _fireSound = GetPath("sounds/SilencedPistol.wav");
            _fullAuto = true;
            _fireWait = 0.9f;
            _kickForce = 0.5f;
            _holdOffset = new Vec2(1f, 0f);
            loseAccuracy = 0f;
            maxAccuracyLost = 0f;
            _editorName = "AF Haze";
            laserSight = true;
            _laserOffsetTL = new Vec2(16f, 6f);
			_weight = 2f;
            _flare = new SpriteMap(GetPath("FlareHazeS"), 13, 10) {center = new Vec2(0.0f, 5f)};
        }

        public override void Update()
        {
            base.Update();
            if (_heatval > 8f)
            {
                _heatval = 8f;
                for (var i = 0; i < 4; i++)
                {
                    Level.Add(SmallSmoke.New(x, y));
                }
            }
            _heatval -= 0.1f;
            if (_heatval < 0f) _heatval = 0f;
            Heatval = _heatval;
            if (duck != null)
            {
                if (duck.inputProfile.Down("QUACK") && _heatval < 2f && Math.Abs(duck.hSpeed) < 2.845f)
                {
                    _holdOffset = new Vec2(3f, -2f) * 0.2f + _holdOffset * 0.8f;
                    Sighted = true;
                }
                else
                {
                    _sighted = false;
                    _holdOffset = new Vec2(1f, 0f) * 0.2f + _holdOffset * 0.8f;
                }
            }
            else
            {
                _sighted = false;
                _holdOffset = new Vec2(1f,0f);
            }

            CurrHone = HoldOffsetNoExtra;
        }

        public override void Fire()
        {
            if (ammo > 0)
            {
                Heatval = Heatval;
                if (_wait <= 0f)
                {
                    if (Sighted && _heatval < 1f)
                    {
                        _heatval += 4f;
                    }

                    _heatval += 1f;
                }
            }
            base.Fire();
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
    }
}
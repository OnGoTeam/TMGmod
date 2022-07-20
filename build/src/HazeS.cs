using System;
using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses;
using TMGmod.Core.WClasses.ClassMarkers;

namespace TMGmod
{
    [BaggedProperty("isInDemo", true)]
    [EditorGroup("TMG|Handgun|Fully-Automatic")]
    public class HazeS : BaseGun, IAmHg, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 2;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 7 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        private float _heatval;

        private bool _sighted;

        [UsedImplicitly] public StateBinding HeatvalBinding = new StateBinding(nameof(Heatval));

        public HazeS(float xval, float yval) :
            base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 26;
            _ammoType = new ATHazeS();
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
            _editorName = "AF Haze";
            laserSight = true;
            _laserOffsetTL = new Vec2(16f, 6f);
            _weight = 2f;
            _flare = new SpriteMap(GetPath("FlareHazeS"), 13, 10) { center = new Vec2(0.0f, 5f) };
        }

        [UsedImplicitly]
        public float Heatval
        {
            get => _heatval;
            set
            {
                _heatval = value;
                _ammoType.bulletSpeed = 60f + 10f * _heatval;
                _ammoType.range = 180f;
                Sighted = _sighted;
            }
        }

        protected override float Accuracy => Sighted ? 1f : _heatval > 3f ? 1.28f - _heatval * 0.16f : 0.8f;

        [UsedImplicitly]
        public bool Sighted
        {
            get => _sighted;
            set
            {
                _sighted = value;
                if (value) _ammoType.range = 450f;
            }
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

        public override void Update()
        {
            base.Update();
            if (_heatval > 8f)
            {
                _heatval = 8f;
                for (var i = 0; i < 4; i++) Level.Add(SmallSmoke.New(x, y));
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
                _holdOffset = new Vec2(1f, 0f);
            }

            CurrHone = HoldOffsetNoExtra;
        }

        protected override bool CanFire()
        {
            return Sighted || duck?.inputProfile.Down("QUACK") != true;
        }

        protected override void OnFire()
        {
            Heatval = Heatval;
            if (Sighted && _heatval < 1f) _heatval += 4f;
            _heatval += 1f;
        }
    }
}

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
        private float _heatval;

        private bool _sighted;

        [UsedImplicitly] public StateBinding HeatvalBinding = new StateBinding(nameof(Heatval));

        public HazeS(float xval, float yval) :
            base(xval, yval)
        {
            _editorName = "AF Haze";
            ammo = 26;
            SetAmmoType<ATHazeS>();
            Smap = new SpriteMap(GetPath("HazeS"), 24, 12);
            _center = new Vec2(12f, 3f);
            _collisionOffset = new Vec2(-12f, -3f);
            _collisionSize = new Vec2(24f, 12f);
            _barrelOffsetTL = new Vec2(24f, 2f);
            _fireSound = GetPath("sounds/SilencedPistol.wav");
            _fullAuto = true;
            _fireWait = .9f;
            _kickForce = .5f;
            _holdOffset = new Vec2(2f, 0f);
            laserSight = true;
            _laserOffsetTL = new Vec2(16f, 6f);
            ShellOffset = new Vec2(-4f, 0f);
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
                _fireWait = _heatval > 3f ? .6f : .9f;
                Sighted = _sighted;
            }
        }

        protected override float BaseAccuracy => Sighted ? 1f : _heatval > 3f ? 1.28f - _heatval * 0.16f : 0.8f;

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

        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 7 });

        protected override void BaseOnUpdate()
        {
            if (_heatval > 8f)
            {
                _heatval = 8f;
                for (var i = 0; i < 4; i++) Level.Add(SmallSmoke.New(x, y));
            }

            _heatval -= 0.07f;
            if (_heatval < 0f) _heatval = 0f;
            Heatval = _heatval;
            if (duck != null)
            {
                if (duck.inputProfile.Down("QUACK") && _heatval < 2f && Math.Abs(duck.hSpeed) < 2.845f)
                {
                    CurrHone = new Vec2(3f, -2f) * 0.2f + CurrHone * 0.8f;
                    Sighted = true;
                }
                else
                {
                    _sighted = false;
                    CurrHone = new Vec2(1f, 0f) * 0.2f + CurrHone * 0.8f;
                }
            }
            else
            {
                _sighted = false;
                CurrHone = new Vec2(1f, 0f);
            }
        }

        protected override bool CanFire()
        {
            return Sighted || duck?.inputProfile.Down("QUACK") != true;
        }

        protected override void BaseOnSpent()
        {
            switch (Sighted)
            {
                case true when _heatval < 1f:
                    _heatval += 4f;
                    break;
                case true when _heatval < 1.3f:
                    _heatval += .3f;
                    break;
                case true when _heatval > 1.7f:
                    _heatval += .7f;
                    break;
                case true:
                    _heatval += _heatval - 1f;
                    break;
                default:
                    _heatval += 1.5f;
                    break;
            }

            Heatval = Heatval;
        }
    }
}

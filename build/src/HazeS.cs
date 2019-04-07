using System.Collections.Generic;
using DuckGame;
using TMGmod.Core.WClasses;
using TMGmod.Core;

namespace TMGmod
{

    [BaggedProperty("isInDemo", true), EditorGroup("TMG|AutoPistol")]
    public class HazeS : BaseGun, IAmHg, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding = new StateBinding(nameof(FrameId));
        public readonly EditorProperty<int> Skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 7 });
        private float _heatval;
        public float Heatval
        {
            get => _heatval;
            set
            {
                _heatval = value;
                _ammoType.accuracy = _heatval > 3f ? 1.2f - _heatval * 0.1f : 0.9f;
                _ammoType.bulletSpeed = 60f + 10f * _heatval;
                _ammoType.range = 180f;
                Sighted = _sighted;
            }
        }

        private bool _sighted;

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
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 26;
            _ammoType = new AT9mmS
            {
                accuracy = 0.9f,
                range = 150f,
                combustable = true,
                bulletSpeed = 60f
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("HazeSpattern"), 24, 12);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(12f, 3f);
            _collisionOffset = new Vec2(-12f, -3f);
            _collisionSize = new Vec2(24f, 12f);
            _barrelOffsetTL = new Vec2(25f, 2f);
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
            _flare = new SpriteMap(GetPath("hazeFlare"), 13, 10) {center = new Vec2(0.0f, 5f)};
        }

        public override void Update()
        {
            if (_heatval > 6f) _heatval = 6f;
            Heatval = _heatval;
            _heatval -= 0.05f;
            if (_heatval < 0f) _heatval = 0f;
            if (duck != null)
            {
                if (duck.inputProfile.Down("QUACK") && _heatval < 1f)
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
                _holdOffset = new Vec2();
            }
            base.Update();
        }

        public override void Fire()
        {
            if (_wait <= 0f)
            {
                if (duck != null && duck.inputProfile.Down("QUACK") && _heatval < 1f)
                {
                    _heatval += 4f;
                }
                _heatval += 1f;
            }

            base.Fire();
        }
        private void UpdateSkin()
        {
            var fid = Skin.value;
            while (!Allowedlst.Contains(fid))
            {
                fid = Rando.Int(0, 9);
            }
            _sprite.frame = fid;
        }

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
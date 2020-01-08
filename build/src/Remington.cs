using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun|Pump-Action")]
    public class Remington : BasePumpAction, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 3;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0 });
        public Remington(float xval, float yval) : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 6;
	        _ammoType = new AT9mm
	        {
	            range = 120f,
	            accuracy = 0.67f,
	            penetration = 1f,
	            bulletSpeed = 23f,
	            bulletThickness = 1.5f
	        };
            _numBulletsPerFire = 5;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Fabarm FP-6"), 33, 10);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(17f, 5f);
		    _collisionOffset = new Vec2(-17f, -5f);
		    _collisionSize = new Vec2(33f, 10f);
		    _barrelOffsetTL = new Vec2(33f, 1f);
            _flare = new SpriteMap(GetPath("FlareBase2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _holdOffset = new Vec2(1f, 2f);
            _fireSound = "shotgunFire";
            _kickForce = 3f;
            loseAccuracy = 0.2f;
            maxAccuracyLost = 0.8f;
		    _manualLoad = true;
            _fireWait = 3f;
            _editorName = "Fabarm FP-6";
            LoaderSprite = new SpriteMap(GetPath("Fabarm FP-6Pump"), 9, 4)
            {
                    center = new Vec2(5f, 2f)
            };
            LoaderVec2 = new Vec2(9f, -1f);
            Loaddx = 3f;
            LoadSpeed = 15;
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
            set
            {
                _sprite.frame = value % (10 * NonSkinFrames);
                LoaderSprite.frame = (value % 10 + 10) % 10;
            }
        }

        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}
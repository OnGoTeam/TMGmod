using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;
using TMGmod.Core.AmmoTypes;

namespace TMGmod
{
    [EditorGroup("TMG|Shotgun|Pump-Action")]
    public class Remington : BasePumpAction, IHaveSkin
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 1;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 2, 4 });
        public Remington(float xval, float yval) : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 6;
	        _ammoType = new AT12Gauge
            {
	            range = 110f,
	            accuracy = 0.67f,
	            bulletThickness = 1.5f
	        };
            _numBulletsPerFire = 5;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("Fabarm FP-6"), 33, 9);
            _graphic = _sprite;
            LoaderSprite = new SpriteMap(GetPath("Fabarm FP-6Pump"), 9, 4)
            {
                center = new Vec2(5f, 2f)
            };
            FrameId = 0;
            _center = new Vec2(17f, 5f);
		    _collisionOffset = new Vec2(-17f, -5f);
		    _collisionSize = new Vec2(33f, 9f);
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
            ShellOffset = new Vec2(0f, -3f);
            LoaderVec2 = new Vec2(9f, -1f);
            Loaddx = 3f;
            LoadSpeed = 15;
            _weight = 3.2f;
        }
        private void UpdateSkin()
        {
            var bublic = Skin.value;
            while (!Allowedlst.Contains(bublic))
            {
                bublic = Rando.Int(0, 9);
            }
            FrameId = bublic;
        }
        [UsedImplicitly]
        public int FrameId
        {
            get => _sprite.frame;
            set
            {
                Ssmfid(_sprite, value, 10 * NonSkinFrames);
                Ssmfid(LoaderSprite, value, 10);
            }
        }

        public override void EditorPropertyChanged(object property)
        {
            UpdateSkin();
            base.EditorPropertyChanged(property);
        }
    }
}
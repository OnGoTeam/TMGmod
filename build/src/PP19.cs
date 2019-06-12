using JetBrains.Annotations;
using System.Collections.Generic;
using DuckGame;
using TMGmod.Core;
using TMGmod.Core.WClasses;
using System;

namespace TMGmod
{
    [EditorGroup("TMG|SMG|Fully-Automatic")]
    [PublicAPI]
    public class PP19 : BaseGun, IAmSmg, IHaveSkin
    {
        public bool Stock;
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        public EditorProperty<int> Skin { get; }
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0 });
        public PP19(float xval, float yval)
          : base(xval, yval)
        {
            Skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 64;
            _ammoType = new AT9mm
            {
                range = 115f,
                accuracy = 0.6f,
                penetration = 1f
            };
            BaseAccuracy = 0.6f;
            _type = "gun";
            _sprite = new SpriteMap(GetPath("PP19Bizonpattern"), 28, 9);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(14f, 4.5f);
            _collisionOffset = new Vec2(-14f, -4.5f);
            _collisionSize = new Vec2(28f, 9f);
            _barrelOffsetTL = new Vec2(28f, 3f);
            _fireSound = GetPath("sounds/2.wav");
            _fullAuto = true;
            _fireWait = 0.75f;
            _kickForce = 1.5f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.35f;
            _holdOffset = new Vec2(2f, -1f);
            handOffset = new Vec2(2f, 0f);
            _editorName = "PP-19 Bizon";
			_weight = 1.5f;
        }
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (Stock)
                {
                    FrameId -= 10;
                    loseAccuracy = 0.1f;
                    maxAccuracyLost = 0.35f;
                    _weight = 1.5f;
                    Stock = false;
                }
                else
                {
                    FrameId += 10;
                    loseAccuracy = 0.14f;
                    maxAccuracyLost = 0.7f;
                    _weight = 1f;
                    Stock = true;
                }
                SFX.Play(GetPath("sounds/tuduc.wav"));
            }
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
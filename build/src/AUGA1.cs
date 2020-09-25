using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core;
using TMGmod.Core.WClasses;
using TMGmod.Core.AmmoTypes;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Fully-Automatic")]
    // ReSharper disable once InconsistentNaming
    public class AUGA1 : BaseAr, IHaveSkin, I5
    {
        private readonly SpriteMap _sprite;
        private const int NonSkinFrames = 2;
        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));
        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;
        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;
        private static readonly List<int> Allowedlst = new List<int>(new[] { 0, 1, 2, 4, 5, 6, 8 });
        [UsedImplicitly]
        public bool Grip
        {
            get => _sprite.frame > 10;
            set
            {
                if (!value)
                {
                    _sprite.frame %= 10;
                    loseAccuracy = 0.15f;
                    maxAccuracyLost = 0.35f;
                    _ammoType.accuracy = 0.8f;
                }
                else
                {
                    _sprite.frame %= 10;
                    _sprite.frame += 10;
                    loseAccuracy = 0.1f;
                    maxAccuracyLost = 0.2f;
                    _ammoType.accuracy = 0.97f;
                }
            }
        }
        [UsedImplicitly]
        public StateBinding GripBinding = new StateBinding(nameof(Grip));

        public AUGA1 (float xval, float yval)
          : base(xval, yval)
        {
            skin = new EditorProperty<int>(8, this, -1f, 9f, 0.5f);
            ammo = 42;
            _ammoType = new ATAUGA1();
            _type = "gun";
            _sprite = new SpriteMap(GetPath("AUGA1"), 30, 12);
            _graphic = _sprite;
            _sprite.frame = 8;
            _center = new Vec2(15f, 6f);
            _collisionOffset = new Vec2(-15f, -6f);
            _collisionSize = new Vec2(30f, 12f);
            _barrelOffsetTL = new Vec2(30f, 4f);
            _flare = new SpriteMap(GetPath("FlareOnePixel1"), 13, 10)
            {
                center = new Vec2(0.0f, 5f)
            };
            _holdOffset = new Vec2(-2f, 1f);
            ShellOffset = new Vec2(-10f, -2f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = true;
            _fireWait = 0.8f;
            _kickForce = 1.7f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.2f;
            _editorName = "AUG A1";
			_weight = 5.5f;
            Kforce2Ar = 0.7f;
        }
        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                Grip = !Grip;
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
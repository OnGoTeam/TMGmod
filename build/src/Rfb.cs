using System.Collections.Generic;
using DuckGame;
using JetBrains.Annotations;
using TMGmod.Core.AmmoTypes;
using TMGmod.Core.SkinLogic;
using TMGmod.Core.WClasses.ClassImplementations;

namespace TMGmod
{
    [EditorGroup("TMG|Rifle|Combined")]
    public class Rfb : BaseAr, IHaveAllowedSkins
    {
        private const int NonSkinFrames = 2;
        public ICollection<int> AllowedSkins { get; } = new List<int>(new[] { 0, 7 });
        private readonly SpriteMap _sprite;

        [UsedImplicitly]
        // ReSharper disable once InconsistentNaming
        private readonly EditorProperty<int> skin;

        public Rfb(float xval, float yval)
            : base(xval, yval)
        {
            skin = new EditorProperty<int>(0, this, -1f, 9f, 0.5f);
            ammo = 20;
            _ammoType = new AT545NATO
            {
                range = 380f,
                accuracy = 0.9f,
            };
            _type = "gun";
            _sprite = new SpriteMap(GetPath("RFB"), 33, 11);
            _graphic = _sprite;
            _sprite.frame = 0;
            _center = new Vec2(17f, 5f);
            _collisionOffset = new Vec2(-17f, -5f);
            _collisionSize = new Vec2(33f, 11f);
            _barrelOffsetTL = new Vec2(33f, 3f);
            _flare = new SpriteMap(GetPath("FlareOnePixel2"), 13, 10)
            {
                center = new Vec2(0.0f, 5f),
            };
            _holdOffset = new Vec2(0f, 1f);
            ShellOffset = new Vec2(9f, -3f);
            _fireSound = GetPath("sounds/scar.wav");
            _fullAuto = false;
            _fireWait = 0.46f;
            loseAccuracy = 0.1f;
            maxAccuracyLost = 0.3f;
            _editorName = "RFB";
            _weight = 5.5f;
            _kickForce = 0.07f;
            KforceDelta = 0.63f;
        }

        public StateBinding FrameIdBinding { get; } = new StateBinding(nameof(FrameId));

        // ReSharper disable once ConvertToAutoProperty
        public EditorProperty<int> Skin => skin;

        public int FrameId
        {
            get => _sprite.frame;
            set => _sprite.frame = value % (10 * NonSkinFrames);
        }

        public override void Update()
        {
            if (duck?.inputProfile.Pressed("QUACK") == true)
            {
                if (_fullAuto)
                {
                    _fullAuto = false;
                    _sprite.frame -= 10;
                    _fireWait = 0.46f;
                    maxAccuracyLost = 0.3f;
                    _ammoType.accuracy = 0.9f;
                }
                else
                {
                    _fullAuto = true;
                    _sprite.frame += 10;
                    _fireWait = 0.79f;
                    maxAccuracyLost = 0.45f;
                    _ammoType.accuracy = 0.6f;
                }

                SFX.Play(GetPath("sounds/tuduc.wav"));
            }

            base.Update();
        }
    }
}
